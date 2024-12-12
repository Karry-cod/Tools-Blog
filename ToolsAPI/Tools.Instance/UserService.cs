using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.DataBase.MYSQL;
using Tools.Interface;
using Tools.Model;
using System.Net.Mail;
using System.Net;
using Tools.Model.Enums;
using Tools.Common;
using Tools.Model.Response;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using Tools.Common.Collection;
using Newtonsoft.Json;
using Tools.Helper;

namespace Tools.Instance
{
    public class UserService: IUserService
    {
        private Blob_ToolsContext _context;

        public UserService(Blob_ToolsContext context) {
            _context = context;
        }

        #region 蓝牙功能
        /// <summary>
        /// 给用户绑定蓝牙
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult> BindBlueToothByUser()
        {
            // 判断用户是否登录
            if (LocalUserCommon.LocalUser == null) {
                return await ApiResult.Fail("请先进行登录！");
            }

            // 获取当前设备蓝牙信息
            var ratio = BluetoothRadio.PrimaryRadio;
            // 检查本地蓝牙适配器是否已启用
            if (BluetoothRadio.IsSupported)
            {
                if (ratio == null)
                {
                    return await ApiResult.Fail("蓝牙适配器未启用");
                }
            }
            else
            {
                return await ApiResult.Fail("该设备不支持蓝牙服务");
            }

            // 先判断用户是否已经绑定5个蓝牙设备了，上限只能5个蓝牙设备
            var list = await _context.BlueToothUsers.Where(d => d.Uid == LocalUserCommon.LocalUser.UId).ToListAsync();
            if (list.Count == 5) {
                return await ApiResult.Fail("能绑定的蓝牙设备数上限5个");
            }

            // 判断该设备是否已经被其他用户绑定过
            var blueToothUser = await _context.BlueToothUsers.Where(d => d.BlueToothName == ratio.Name && d.BlueToothAddress == ratio.LocalAddress.ToString()).FirstOrDefaultAsync();
            if (blueToothUser != null)
            {
                if (blueToothUser.Uid == LocalUserCommon.LocalUser.UId)
                {
                    return await ApiResult.Ok("该已为您绑定当前设备蓝牙");
                }
                else {
                    return await ApiResult.Fail("当前设备已被其他用户绑定，请联系原用户进行删除");
                }
            }

            var blueTooth_User = new BlueToothUser() { 
                BtuId = Config.GetGuid(),
                BlueToothAddress = ratio.LocalAddress.ToString(),
                BlueToothName = ratio.Name,
                BlueToothType = ratio.ClassOfDevice.MajorDevice.ToString(),
                Uid = LocalUserCommon.LocalUser.UId,
                CreatedTime = DateTime.Now
            };
            await _context.BlueToothUsers.AddAsync(blueTooth_User);
            int count = await _context.SaveChangesAsync();
            return await (count > 0 ? ApiResult.Ok("绑定成功") : ApiResult.Fail("绑定失败，请联系开发人员检查一下？"));
        }

        /// <summary>
        /// 获取周围的蓝牙设备
        /// </summary>
        /// <param name="isFindUser">是否只查找用户</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult> GetBlueTooths(bool isFindUser = false)
        {
            // 检查本地蓝牙适配器是否已启用
            if (BluetoothRadio.IsSupported)
            {
                BluetoothRadio radio = BluetoothRadio.PrimaryRadio; // 获取当前设备的信息
                if (radio == null)
                {
                    return await ApiResult.Fail("蓝牙适配器未启用");
                }
            }
            else
            {
                return await ApiResult.Fail("该设备不支持蓝牙服务");
            }

            var bluetoothClient = new BluetoothClient();
            var discoverDevices = bluetoothClient.DiscoverDevices();
            // 存储获取到的蓝牙设备集
            BlueToothCollection.DiscoverDevices = discoverDevices;

            if (isFindUser) {
                var bluetooths = discoverDevices.Select(d => d.DeviceName + "-" + d.DeviceAddress.ToString()).ToList();
                var bluetoothUsers = await _context.BlueToothUsers.Where(d => bluetooths.Contains(d.BlueToothName + "-" + d.BlueToothAddress)).ToListAsync();
                if (bluetoothUsers.Count > 0) {
                    var users = await _context.Users.ToListAsync();
                    var result = new List<BlueTooth_UsersRes>();
                    bluetoothUsers.ForEach(d => {
                        var user = users.Where(u => u.UId == d.Uid).FirstOrDefault();
                        result.Add(new BlueTooth_UsersRes() { 
                            BtuId = d.BtuId,
                            Uid = d.Uid,
                            Uname = user.UName,
                            BlueToothAddress = d.BlueToothAddress,
                            BlueToothType = d.BlueToothType,
                            BlueToothName = d.BlueToothName,
                            CreatedTime = d.CreatedTime?.ToString("yyyy-MM-dd HH:mm:ss")
                        });
                    });
                    return await ApiResult.Ok(data: result);
                }
                else {
                    return await ApiResult.Ok();
                }
            }

            var resultList = discoverDevices.Select(d => new BlueTooth_UsersRes() { 
                BlueToothAddress = d.DeviceAddress.ToString(),
                BlueToothName= d.DeviceName,
                BlueToothType = d.ClassOfDevice.MajorDevice.ToString()
            }).ToList();
            return await ApiResult.Ok(data: resultList);
        }

        /// <summary>
        /// 获取当前用户已绑定的蓝牙设备信息列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> GetMyBlueTooths() {
            if (!LocalUserCommon.IsLogin()) {
                return await ApiResult.Fail("请先登录再进行此操作！");
            };
            var blueTooths = await _context.BlueToothUsers.Where(d => d.Uid == LocalUserCommon.LocalUser.UId).ToListAsync();
            return await ApiResult.Ok(data: blueTooths);
        }

        /// <summary>
        /// 解除绑定的蓝牙设备
        /// </summary>
        /// <param name="btId">蓝牙绑定id</param>
        /// <returns></returns>
        public async Task<ApiResult> CancelBindBlueTooth(string btId) {
            var blueTooth_User = await _context.BlueToothUsers.Where(d => d.BtuId == btId && d.Uid == LocalUserCommon.LocalUser.UId).FirstOrDefaultAsync();
            if (blueTooth_User == null) {
                return await ApiResult.Fail("该数据不存在，数据异常");
            }
            _context.BlueToothUsers.Remove(blueTooth_User);
            int count = await _context.SaveChangesAsync();
            return await (count > 0 ? ApiResult.Ok() : ApiResult.Fail("删除失败，请联系开发人员进行查看"));
        }
        #endregion

        /// <summary>
        /// 提交创建请求
        /// </summary>
        /// <param name="target">请求账户</param>
        /// <param name="type">请求类型</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult> ConfirmCreate(string target, string type, string? password, string code, string ip)
        {
            // 1. 判断是否已存在
            var user = await _context.Users.Where(d => d.UEmail == target || d.UPhone == target).Select(d => new UserRes
            {
                UId = d.UId,
                UName = d.UName,
                CreatedTime = d.CreatedTime,
                UAccount = d.UAccount,
                UAvatar = d.UAvatar,
                UEmail = d.UEmail,
                UIntroduce = d.UIntroduce,
                UPassword = d.UPassword,
                UPhone = d.UPhone,
                UIP = d.UIp
            }).FirstOrDefaultAsync();
            if (user != null)
            {
                return await ApiResult.Ok($"{EncryptUtil.Encrypt(JsonConvert.SerializeObject(user))}");
            }

            // 2. 判断验证码是否正确
            var userMessage = await _context.UserMessages.Where(d => d.MainInfo == code && d.Target == target && d.Type == type).OrderByDescending(d => d.CreatedTime).FirstOrDefaultAsync();
            if (userMessage != null && (DateTime.Now - userMessage.CreatedTime).Value <= TimeSpan.FromMinutes(3))
            {

            }
            else {
                return await ApiResult.Fail("验证码不存在或已失效，请重新发送验证码");
            }

            var newUser = new User() { 
                UId = Guid.NewGuid().ToString().Replace("-", ""),
                UAccount = Enum_UserMessageType.SetAccountByTarget(target, type),
                UPassword = string.IsNullOrEmpty(password) ? DateTime.Now.ToString("MMddHHmmss") : password,
                UEmail = type == "Email" ? target : "",
                UPhone = type == "Phone" ? target : "",
                UAvatar = "https://img1.baidu.com/it/u=165764012,751844309&fm=253&fmt=auto&app=138&f=JPEG?w=500&h=500",
                UIntroduce = "此人懒得逆天，别太离谱，啥都没留下",
                UName = Enum_UserMessageType.SetAccountByTarget(target, type),
                CreatedTime = DateTime.Now,
                USex = 1,
                UIp = ip
            };

            if (newUser.UAccount.Contains("Error")) {
                return await ApiResult.Fail(newUser.UAccount);
            }

            // 获取ip所在地
            var ipAddress = AMAPHelper.GetAddressByIP(ip);
            newUser.UIpAddress = ipAddress.province + " · " + ipAddress.city;
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return await ApiResult.Ok($"{EncryptUtil.Encrypt(JsonConvert.SerializeObject(newUser))}", data: newUser);
        }

        /// <summary>
        /// 创建账号 - 邮箱
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult> CreateByEmail(string email)
        {
            try {
                // 判断邮件地址格式是否合法
                if (Enum_UserMessageType.SetAccountByTarget(email, "Email").Contains("Error")) {
                    return await ApiResult.Fail("该邮箱地址不合法，请重新输入");
                }

                var mainInfo = new Random().Next(1000, 9999);
                string message = $"   欢迎您创建微客账号，短信验证码为：<span style='color:red;font-size:1.5rem'>{mainInfo}</span>";
                
                // 创建邮件消息
                var mailMessage = new MailMessage(SMTPCollection.QQ.EmailAddress, email)
                {
                    Subject = "微客",
                    Body = message,
                    IsBodyHtml = true
                };

                // 配置SMTP客户端
                using (var smtpClient = new SmtpClient(SMTPCollection.QQ.Host))
                {
                    smtpClient.Port = SMTPCollection.QQ.Port; // SMTP端口
                    smtpClient.UseDefaultCredentials = false; // 不使用默认凭据
                    smtpClient.Credentials = new System.Net.NetworkCredential(SMTPCollection.QQ.Account, SMTPCollection.QQ.Pwd); // SMTP服务器凭据
                    smtpClient.EnableSsl = true; // 启用SSL

                    // 发送邮件
                    smtpClient.Send(mailMessage);
                }

                var userMessage = new UserMessage() { 
                    UmId = Guid.NewGuid().ToString().Replace("-", ""),
                    MainInfo = mainInfo.ToString(),
                    Target = email,
                    Type = Enum_UserMessageType.Email,
                    Message = message,
                    CreatedTime = DateTime.Now
                };
                await _context.UserMessages.AddAsync(userMessage);
                await _context.SaveChangesAsync();

                return await ApiResult.Ok("发送成功！");
            } catch (Exception ex) {
                return await ApiResult.Fail(ex.Message);
            }
            
        }

        /// <summary>
        /// 创建账号 - 短信
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult> CreateByPhone(string phone)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> Login(string account, string password, string ip) {
            var newUser = await _context.Users.Where(d => d.UAccount == account && d.UPassword == password).FirstOrDefaultAsync();

            if (newUser == null) {
                return await ApiResult.Fail("查无此用户，请检查账号或密码是否正确");
            }

            // 获取ip所在地
            var ipAddress = AMAPHelper.GetAddressByIP(ip);
            newUser.UIp = ip;
            newUser.UIpAddress = ipAddress.province + " · " + ipAddress.city;
            _context.Update(newUser);
            await _context.SaveChangesAsync();

            return await ApiResult.Ok($"{EncryptUtil.Encrypt(JsonConvert.SerializeObject(newUser))}", newUser);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="selectUserName">用户名或账号</param>
        /// <returns></returns>
        public async Task<ApiResult> GetUsers(string selectUserName) {
            var users = await _context.Users.Where(d => d.UName.Contains(selectUserName) || d.UAccount == selectUserName).ToListAsync();
            return await ApiResult.Ok(data:users);
        }
    }
}

using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using Tools.Common;
using Tools.Common.Collection;
using Tools.Interface;
using Tools.Model;
using Tools.Model.Response;

namespace Tools.API.Controllers
{
    /// <summary>
    /// 用户模块
    /// </summary>
    public class UsersController : BaseControllerr, IUserService
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 提交创建申请
        /// </summary>
        /// <param name="target">账号</param>
        /// <param name="type">请求类型</param>
        /// <param name="password">密码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<ApiResult> ConfirmCreate(string target, string type, string? password, string code, string ip)
        {
            return await _userService.ConfirmCreate(target, type, password, code, ip);
        }

        /// <summary>
        /// 创建账号 - 邮箱
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<ApiResult> CreateByEmail(string email)
        {
            return await _userService.CreateByEmail(email);
        }

        /// <summary>
        /// 创建账号 - 手机号
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<ApiResult> CreateByPhone(string phone)
        {
            return await ApiResult.Ok("测试成功");
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> Login(string account, string password, string ip)
        {
            return await _userService.Login(account, password, ip);
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> GetLocalUser() {
            if (LocalUserCommon.LocalUser != null) {
                return await ApiResult.Ok(data: LocalUserCommon.LocalUser);
            }
            return await ApiResult.Fail("凭证已过期，请重新登录！");
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="selectUserName">用户名或账号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> GetUsers(string selectUserName = "")
        { 
            return await _userService.GetUsers(selectUserName);
        }

        #region 蓝牙功能
            /// <summary>
            /// 获取周围的蓝牙列表
            /// </summary>
            /// <param name="isFindUser">是否只查询用户</param>
            /// <returns></returns>
            [HttpGet]
        public async Task<ApiResult> GetBlueTooths(bool isFindUser = false)
        {
            return await _userService.GetBlueTooths(isFindUser);
        }

        /// <summary>
        /// 连接蓝牙
        /// </summary>
        /// <param name="blueToothName">蓝牙名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> ConnectBlueTooth(string blueToothName)
        {
            try
            {
                var blueToothDriver = BlueToothCollection.DiscoverDevices.FirstOrDefault(d => d.DeviceName == blueToothName);
                if (blueToothDriver == null)
                {
                    return await ApiResult.Fail("该设备不存在");
                }
                if (blueToothDriver.Connected)
                {
                    return await ApiResult.Fail("该设备已连接蓝牙");
                }


                // 创建连接
                BluetoothRadio radio = BluetoothRadio.PrimaryRadio;
                //Guid serviceUuid = Guid.Parse(guid); // 例如，使用标准的RFCOMM服务UUID
                BluetoothClient client = new BluetoothClient();
                //BluetoothAddress address = BluetoothAddress.Parse("94:08:C7:89:AC:5B");
                //var peer = new BluetoothEndPoint(address, BluetoothService.SerialPort);
                //client.Connect(peer);
                await Task.Run(() => client.Connect(blueToothDriver.DeviceAddress, BluetoothService.SerialPort));

                // 连接后，可以使用client.GetStream()来读写数据
                NetworkStream stream = client.GetStream();
                return await ApiResult.Ok(data: stream);
            }
            catch (Exception ex)
            {

                //Process.Start(@"ms-settings:bluetooth");
                return await ApiResult.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 断开蓝牙连接
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> CloseBlueToothConnect()
        {
            BluetoothClient client = new BluetoothClient();
            if (client.Connected)
            {
                client.Close();
                return await ApiResult.Ok("成功断开连接");
            }
            return await ApiResult.Fail("蓝牙此时无连接");
        }

        /// <summary>
        /// 为用户绑定设备蓝牙
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<ApiResult> BindBlueToothByUser()
        {
            return await _userService.BindBlueToothByUser();
        }

        /// <summary>
        /// 获取当前用户已绑定的蓝牙设备信息集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> GetMyBlueTooths()
        {
            return await _userService.GetMyBlueTooths();
        }

        /// <summary>
        /// 获取当前设备的蓝牙信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> GetCurrentBlueTooth() {
            // 检查本地蓝牙适配器是否已启用
            if (BluetoothRadio.IsSupported)
            {
                BluetoothRadio radio = BluetoothRadio.PrimaryRadio; // 获取当前设备的信息
                if (radio == null)
                {
                    return await ApiResult.Fail("蓝牙适配器未启用");
                }
                return await ApiResult.Ok(data: new BlueToothRadioRes() { 
                    Name = radio.Name,
                    Address = radio.LocalAddress.ToString(),
                    DriviceName = radio.ClassOfDevice.MajorDevice.ToString(),
                    Mode = radio.Mode.ToString()
                });
            }
            else
            {
                return await ApiResult.Fail("该设备不支持蓝牙服务");
            }
        }

        /// <summary>
        /// 解除绑定的蓝牙设备
        /// </summary>
        /// <param name="btId">蓝牙绑定id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> CancelBindBlueTooth(string btId)
        {
            return await _userService.CancelBindBlueTooth(btId);
        }
        #endregion
    }
}

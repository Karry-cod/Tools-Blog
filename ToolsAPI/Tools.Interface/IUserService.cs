using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Model;

namespace Tools.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ApiResult> Login(string account, string password, string ip);

        /// <summary>
        /// 创建账号 - 短信
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> CreateByPhone(string phone);

        /// <summary>
        /// 创建账号 - 邮箱
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> CreateByEmail(string email);

        /// <summary>
        /// 提交创建请求
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> ConfirmCreate(string target, string type, string password, string code, string ip);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> GetUsers(string selectUserName);

        #region 蓝牙功能
        /// <summary>
        /// 给用户绑定蓝牙
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> BindBlueToothByUser();

        /// <summary>
        /// 获取周围的蓝牙设备
        /// </summary>
        /// <param name="isFindUser">是否只查找用户</param>
        /// <returns></returns>
        Task<ApiResult> GetBlueTooths(bool isFindUser = false);

        /// <summary>
        /// 获取当前用户已绑定的蓝牙信息列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> GetMyBlueTooths();

        /// <summary>
        /// 解除绑定的蓝牙设备
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> CancelBindBlueTooth(string btId);
        #endregion
    }
}

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tools.Model.Response;

namespace Tools.Common
{
    public class LocalUserCommon
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public static UserRes LocalUser { get; set; }

        /// <summary>
        /// 判断当前用户是否登录
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin() {
            if (LocalUserCommon.LocalUser == null) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public static UserRes? GetCurrentUser(string cookie) {
            if (string.IsNullOrEmpty(cookie)) {
                return null;
            }
            return JsonConvert.DeserializeObject<UserRes>(EncryptUtil.Decrypt(cookie));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools.Model.Enums
{
    public class Enum_UserMessageType
    {
        public static string Email = "Email";

        public static string Phone = "Phone";

        /// <summary>
        /// 返回类型名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTypeName(string type) { 
            return type switch { 
                "Email" => "邮箱",
                "Phone" => "手机号"
            };
        }

        /// <summary>
        /// 根据请求账户设置用户名
        /// </summary>
        /// <returns></returns>
        public static string SetAccountByTarget(string target, string type) {
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"; // 用于匹配是否属于邮件地址格式

            return type switch { 
                "Email" => Regex.IsMatch(target, pattern) ? target.Split("@")[0] : "Error：邮件格式错误，请重新输入",
                "Phone" => target
            };
        }
    }
}

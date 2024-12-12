using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Common.Collection
{
    public class SMTPCollection
    {
        public class Email {
            /// <summary>
            /// 邮箱账号
            /// </summary>
            public string Account { get; set; } = "3603703202";
            
            /// <summary>
            /// SMTP凭证
            /// </summary>
            public string Pwd { get; set; } = "dadfpiicpprvchhj";

            /// <summary>
            /// 邮箱地址
            /// </summary>
            public string EmailAddress { get; set; } = "3603703202@qq.com";

            /// <summary>
            /// SMTP端口
            /// </summary>
            public int Port { get; set; } = 587;

            /// <summary>
            /// SMTP请求地址
            /// </summary>
            public string Host { get; set; } = "smtp.qq.com";

        }

        /// <summary>
        /// QQ
        /// </summary>
        public static Email QQ = new Email() {
            Account = "3603703202",
            Pwd = "dadfpiicpprvchhj",
            Port = 587,
            Host = "smtp.qq.com",
            EmailAddress = "3603703202@qq.com"
        };

        /// <summary>
        /// 网易云
        /// </summary>
        public static Email WYY = new Email() {
            Account = "18711586257",
            Pwd = "MFVWBHPIUQXPRMJX",
            EmailAddress = "18711586257@163.com",
            Host = "smtp.163.com",
            Port = 465  // 或 994, 如果未启用SSL加密，则是：25
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using NPinyin;

namespace Tools.Common
{
    public class Config
    {
        /// <summary>
        /// 获取guid
        /// </summary>
        /// <param name="isAll">是否保留整个字符串</param>
        /// <returns></returns>
        public static string GetGuid(bool isAll = false) {
            if (isAll)
            {
                return Guid.NewGuid().ToString();
            }
            else {
                return Guid.NewGuid().ToString().Replace("-", "");
            }
        }

        /// <summary>
        /// 拼音转文字 - 只能单个字进行转换
        /// </summary>
        /// <returns></returns>
        public static string PYToText(string py) {
            var text = Pinyin.GetChineseText(py);
            return text;
        }
    }
}

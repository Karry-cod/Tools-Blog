using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Common
{
    public class LogFileCommon
    {
        /// <summary>
        /// 创建存放日志的目录
        /// </summary>
        /// <returns></returns>
        private static string CreateLogFolder() {
            // 先创建存放日志的文件夹
            if (!Directory.Exists("FileUpload/Logs"))
            {
                Directory.CreateDirectory("FileUpload/Logs");
            }
            string folderUrl = $"FileUpload/Logs/{DateTime.Now.ToString("yyyy-MM")}";
            if (!Directory.Exists(folderUrl))
            {
                Directory.CreateDirectory(folderUrl);
            }
            return folderUrl + "/";
        }

        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="reason">错误原因</param>
        /// <param name="details">详细信息</param>
        public static async Task LogError(string reason, string details) {
            var fileUrl = CreateLogFolder() + DateTime.Now.ToString("MM-dd") + "-Error.txt";
            if (!File.Exists(fileUrl)) {
                File.Create(fileUrl);
            }
            string content = $@"

>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
异常原因：{reason}；
{details}";
            using (StreamWriter sw = new StreamWriter(fileUrl, true))
            {
                sw.WriteLine(content);
            }
        }
    }
}

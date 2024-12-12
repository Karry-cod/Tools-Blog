using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Helper
{
    /// <summary>
    /// 文件管理
    /// </summary>
    public class FileHelper
    {

        /// <summary>
        /// 批量下载文件
        /// </summary>
        /// <param name="files">文件集</param>
        /// <param name="tempUrl">临时存放路径</param>
        /// <param name="evnPath">环境真实路径</param>
        /// <returns></returns>
        public static async Task<List<string>> DownloadFiles(List<IFormFile> files, string tempUrl, string evnPath) {
            var paths = new List<string>();

            if (Directory.Exists(tempUrl))
            {
            }
            else
            {
                Directory.CreateDirectory(tempUrl);
            }

            files.ForEach(async file =>
            {
                var fileName = file.FileName;

                //拼接完整的文件存储路径
                var filePath = Path.Combine(tempUrl, fileName);

                using (var fs = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(fs);
                    paths.Add(evnPath + tempUrl + "/" + fileName);
                }
            });

            // 强制同步
            await Task.Run(() => { });

            return paths;
        }

        /// <summary>
        /// 清空文件夹
        /// </summary>
        public static void ClearFolder(string folderPath) {
            // 检查文件夹是否存在
            if (Directory.Exists(folderPath))
            {
                // 获取文件夹中的所有文件和子文件夹
                string[] files = Directory.GetFiles(folderPath);
                string[] directories = Directory.GetDirectories(folderPath);

                // 删除文件
                foreach (var file in files)
                {
                    File.Delete(file);
                }

                // 递归清空子文件夹
                foreach (var directory in directories)
                {
                    ClearFolder(directory);
                }
            }
            else
            {
                throw new DirectoryNotFoundException($"The folder '{folderPath}' does not exist.");
            }
        }


        /// <summary>
        /// 创建文件夹
        /// </summary>
        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }
        }
    }
}

using Aliyun.OSS;
using Aliyun.OSS.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Helper
{
    public class OSSHelper
    {
        // 阿里云OSS相关参数
        private static readonly string endpoint = "oss-cn-shanghai.aliyuncs.com"; // 外网
        //private const string endpoint = "oss-cn-shanghai-internal.aliyuncs.com"; // 内网
        private static readonly string accessKeyId = "LTAI5tMsyC4HCTSsEugRwdAP";
        private static readonly string accessKeySecret = "R3fZXzIZ2YTXsMS4xA1e28vuC4wjpu";
        private static readonly string bucket = "blobtools";
        private static readonly string imgUrl = "https://blobtools.oss-cn-shanghai.aliyuncs.com";

        /// <summary>
        /// 上传文件（文件流形式）
        /// </summary>
        /// <param name="files"></param>
        /// <param name="types">文件格式要求</param>
        /// <param name="isTimestamp">是否以当前时间命名文件名</param>
        /// <param name="folder">存放文件的文件夹路径</param>
        /// <returns></returns>
        public static List<string> Upload_Stream(List<IFormFile> files, string[]? types, bool isTimestamp = false, string folder = "")
        {
            #region 格式校验
            var mark = false;
            if (types != null)
            {
                files.ForEach(d => {
                    var ext = Path.GetExtension(d.FileName).ToLowerInvariant();
                    if (string.IsNullOrEmpty(ext) || !types.Contains(ext)) mark = true;
                });
            }
            if (mark) return new List<string>();
            #endregion

            var write_client = new OssClient(endpoint, accessKeyId, accessKeySecret); // 用于oss操作对象
            var read_client = new OssClient(endpoint, accessKeyId, accessKeySecret); // 用于oss读取对象
            var imgUrls = new List<string>();
            files.ForEach(d => {
                var fileName = Path.GetFileName(d.FileName);
                if (isTimestamp) fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                using (var stream = d.OpenReadStream())
                {
                    write_client.PutObject(bucket, folder + fileName, stream);
                }
                DateTime expiration = DateTime.Now.AddYears(20); // 有效期限
                var url = read_client.GeneratePresignedUri(bucket, folder + "/" + fileName, expiration);
                string urlstring = imgUrl + url.AbsolutePath;
                imgUrls.Add(urlstring);
            });
            return imgUrls;
        }

        /// <summary>
        /// 上传文件（文件流形式）
        /// </summary>
        /// <param name="files"></param>
        /// <param name="types">文件格式要求</param>
        /// <param name="isTimestamp">是否以当前时间命名文件名</param>
        /// <param name="folder">存放文件的文件夹路径</param>
        /// <returns></returns>
        public static List<string> Upload_Path(List<string> paths, string[]? types = null, bool isTimestamp = false, string folder = "") {
            #region 格式校验
            var mark = false;
            if (types != null)
            {
                paths.ForEach(d => {
                    var ext = Path.GetExtension(d).ToLowerInvariant();
                    if (string.IsNullOrEmpty(ext) || !types.Contains(ext)) mark = true;
                });
            }
            if (mark) return new List<string>();
            #endregion

            var write_client = new OssClient(endpoint, accessKeyId, accessKeySecret); // 用于oss操作对象
            var read_client = new OssClient(endpoint, accessKeyId, accessKeySecret); // 用于oss读取对象
            var imgUrls = new List<string>();
            paths.ForEach(d => {
                var fileName = Path.GetFileName(d);
                if (isTimestamp) fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                using (var stream = new FileStream(d, FileMode.Open, FileAccess.Read))
                {
                    write_client.PutObject(bucket, folder + fileName, stream);
                }
                DateTime expiration = DateTime.Now.AddYears(20); // 有效期限
                var url = read_client.GeneratePresignedUri(bucket, folder + fileName, expiration);
                string urlstring = imgUrl + url.AbsolutePath;
                imgUrls.Add(urlstring);
            });
            return imgUrls;
        }

        /// <summary>
        /// 删除指定存储空间下的特定文件
        /// </summary>
        /// <param name="folder">存储文件夹的名称</param>
        /// <param name="key">文件的名称</param>        
        public static bool DeleteObject(string key, string folder)
        {
            try
            {
                var write_client = new OssClient(endpoint, accessKeyId, accessKeySecret);
                var listResult = write_client.ListObjects(bucket + folder);
                foreach (var summary in listResult.ObjectSummaries)
                {
                    if (key == summary.Key)
                        break;
                }
                write_client.DeleteObject(bucket + folder, key);
                return true;
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                 ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 拷贝文件重命名  拷贝的文件必须小于1G(当前在同一个存储空间下)
        /// </summary>
        /// <param name="sourceBucket">原文件所在存储空间的名称</param>
        /// <param name="sourceKey">原文件的名称</param>
        /// <param name="targetBucket">目标文件所在存储空间的名称 可以与原文件所在的存储空间是同一个  也可以是另一个</param>
        /// <param name="targetKey">目标文件的名称</param>       
        private static string CopyObect(string sourceKey, string targetKey, string sourceBucket, string targetBucket)
        {

            try
            {
                //sourceKey = "test/test.txt";
                //targetKey = "test/2018.txt";
                sourceBucket = bucket + sourceBucket;
                targetBucket = bucket + targetBucket;
                var metadata = new ObjectMetadata();
                var write_client = new OssClient(endpoint, accessKeyId, accessKeySecret);
                metadata.AddHeader(Aliyun.OSS.Util.HttpHeaders.ContentType, "text/html");
                var req = new CopyObjectRequest(sourceBucket, sourceKey, targetBucket, targetKey)
                {
                    NewObjectMetadata = metadata
                };
                write_client.CopyObject(req);
                DeleteObject(sourceKey, sourceBucket);
                return JsonConvert.SerializeObject(new { status = 200, rel = true, data = "Copy object succeeded" });
            }
            catch (OssException ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    status = 201,
                    error = string.Format("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId)
                });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { status = 201, error = string.Format("Copy object failed. {0}", ex.Message) });
            }
        }
    }
}

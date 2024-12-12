using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Model
{
    public enum ResultCode
    {
        ok = 1,
        fail = 0,
        error = -500
    }

    public class ApiResult
    {
        public ResultCode? code { get; set; }
        public string? message { get; set; }
        public object? data { get; set; }
        /// <summary>
        /// 如果该返回结果是从redis中取出的，则需将此字段改为：true
        /// </summary>
        public bool isRedis { get; set; } = false;
        /// <summary>
        /// 如果该接口的作用是查询，则将此字段改为：true
        /// </summary>
        public bool isQuery { get; set; } = false;

        /// <summary>
        /// 返回结果：成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<ApiResult> Ok(string? message = "success", object? data = null, bool isRedis = false, bool isQuery = false)
        {
            await Task.Run(() => { 
            
            });

            return new ApiResult()
            {
                code = ResultCode.ok,
                message = message,
                data = data,
                isRedis = isRedis,
                isQuery = isQuery
            };
        }

        /// <summary>
        /// 返回结果：失败
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<ApiResult> Fail(string? message = "fail", object? data = null)
        {
            await Task.Run(() => {

            });

            return new ApiResult()
            {
                code = ResultCode.fail,
                message = message,
                data = data
            };
        }

        /// <summary>
        /// 返回结果：异常
        /// </summary>
        /// <returns></returns>
        public static async Task<ApiResult> Error(string? message = "exception", object? data = null)
        {
            await Task.Run(() => {

            });

            return new ApiResult()
            {
                code = ResultCode.error,
                message = message,
                data = data
            };
        }
    }
}

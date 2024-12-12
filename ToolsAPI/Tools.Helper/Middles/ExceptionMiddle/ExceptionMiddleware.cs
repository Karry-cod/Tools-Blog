using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Model;

namespace Tools.Helper.Middles.ExceptionMiddle
{
    public class ExceptionMiddleware
    {
        protected RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 中间件入口
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //处理返回结果
                var response = context.Response;
                var originalBodyStream = response.Body;
                using (var responseBody = new MemoryStream())
                {
                    response.Body = responseBody;
                    // 修改返回内容
                    ApiResult result = new ApiResult();
                    var res = JsonConvert.SerializeObject(await ApiResult.Error(ex.Message));
                    var buffer = Encoding.UTF8.GetBytes(res);
                    context.Response.Headers.Add("Content-Type", "application/json"); // 添加标头，声明此返回结果为json格式
                    await response.Body.WriteAsync(buffer, 0, buffer.Length);
                    response.Body.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                    await LogFileCommon.LogError(ex.Message, ex.StackTrace);
                }
            }
        }
    }
}

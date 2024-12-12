using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Common;

namespace Tools.Helper.Filters
{
    /// <summary>
    /// 行为过滤器
    /// </summary>
    public class ResultFilter : BaseFilterAttribute
    {
        /// <summary>
        /// 在请求进入管道之前，对上下文进行操作
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cookie = context.HttpContext.Request.Headers["wk_cookie"];
            LocalUserCommon.LocalUser = LocalUserCommon.GetCurrentUser(cookie);
            return base.OnActionExecutionAsync(context, next);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Model;

namespace Tools.Interface
{
    public interface IAMAPService
    {
        /// <summary>
        /// 根据关键字获取地点信息
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <returns></returns>
        Task<ApiResult> GetPOI2(string keywords);
    }
}

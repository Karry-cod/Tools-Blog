using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Helper;
using Tools.Interface;
using Tools.Model;

namespace Tools.Instance
{
    public class AMAPService : IAMAPService
    {
        /// <summary>
        /// 根据关键字获取地点信息
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <returns></returns>
        public async Task<ApiResult> GetPOI2(string keywords)
        {
            var result = AMAPHelper.GetPOI2(keywords);
            return await ApiResult.Ok(data: result);
        }
    }
}

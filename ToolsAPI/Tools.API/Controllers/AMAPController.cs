using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tools.Interface;
using Tools.Model;

namespace Tools.API.Controllers
{
    /// <summary>
    /// 高德地图
    /// </summary>
    public class AMAPController : BaseControllerr, IAMAPService
    {
        private IAMAPService _amapService;

        public AMAPController(IAMAPService amapService) {
            _amapService = amapService;
        }

        /// <summary>
        /// 根据关键字获取地点信息
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> GetPOI2(string keywords)
        {
            return await _amapService.GetPOI2(keywords);
        }
    }
}

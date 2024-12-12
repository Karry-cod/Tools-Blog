using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Common;
using Tools.Model.Response.AMAP;

namespace Tools.Helper
{
    /// <summary>
    /// 高德地图
    /// </summary>
    public class AMAPHelper
    {
        private static string api_key = "70cd0d5450719a60d929e1d3ac6ee47e";

        /// <summary>
        /// 根据ip获取定位信息（只限中国）
        /// </summary>
        private static string host_GetAddressByIP = $"https://restapi.amap.com/v3/ip?key={api_key}&ip=";

        /// <summary>
        /// 根据关键字搜索地点信息
        /// </summary>
        private static string host_GetPOI2 = $"https://restapi.amap.com/v5/place/text?key={api_key}&keywords=";

        /// <summary>
        /// 根据ip获取定位信息
        /// </summary>
        public static Address GetAddressByIP(string ip) {
            var result = Http.Get(host_GetAddressByIP + ip);
            var address = new Address();
            if (result != null) { 
                address = JsonConvert.DeserializeObject<Address>(result);
                if (address.status != "1") {
                    address = null;
                }
            }
            return address;
        }

        /// <summary>
        /// 根据关键字搜索地点信息 - POI 2: https://lbs.amap.com/api/webservice/guide/api-advanced/newpoisearch
        /// </summary>
        /// <returns></returns>
        public static POIRes GetPOI2(string keywords) {
            var result = Http.Get(host_GetPOI2 + keywords);
            var PoiRes = new POIRes();
            if (result != null) {
                PoiRes = JsonConvert.DeserializeObject<POIRes>(result);
            }
            return PoiRes;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Common;

namespace Tools.Helper
{
    public class IpifyHelper
    {
        // IPIFY, 接口api， 官网地址：https://geo.ipify.org/docs
        private static string api_key = "at_BuNKeR3B8WU6826AVg6SayGZpKOb3";

        /// <summary>
        /// API - Country + City
        /// </summary>
        private static string host_CountryCity = $"https://geo.ipify.org/api/v2/country,city?apiKey={api_key}&ipAddress=";

        private static string host_Country = $"https://geo.ipify.org/api/v2/country?apiKey={api_key}&ipAddress=";

        private static string host_CountryCityVPN = $"https://geo.ipify.org/api/v2/country,city,vpn?apiKey={api_key}&ipAddress=";

        /// <summary>
        /// 获取IP所在地，包含City + Country
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocation_CountryCity(string ip) { 
            var result = Http.Get(host_CountryCity + ip);
            var location = new IPAddress();
            if (result != null)
            {
                location = JsonConvert.DeserializeObject<IPAddress>(result);
            }
            return location;
        }

        /// <summary>
        /// 获取IP所在地，包含Country
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocation_Country(string ip)
        {
            var result = Http.Get(host_Country + ip);
            var location = new IPAddress();
            if (result != null)
            {
                location = JsonConvert.DeserializeObject<IPAddress>(result);
            }
            return location;
        }

        /// <summary>
        /// 获取IP所在地，包含City + Country + VPN
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocation_CountryCityVPN(string ip)
        {
            var result = Http.Get(host_CountryCityVPN + ip);
            var location = new IPAddress();
            if (result != null)
            {
                location = JsonConvert.DeserializeObject<IPAddress>(result);
            }
            return location;
        }

        public class Location
        {
            /// <summary>
            /// 
            /// </summary>
            public string country { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string region { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string city { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public double lat { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public double lng { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string postalCode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string timezone { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int geonameId { get; set; }

        }



        public class As
        {
            /// <summary>
            /// 
            /// </summary>
            public int asn { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string route { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string domain { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string type { get; set; }

        }



        public class Proxy
        {
            /// <summary>
            /// 
            /// </summary>
            public string proxy { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string vpn { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string tor { get; set; }

        }



        public class IPAddress
        {
            /// <summary>
            /// 
            /// </summary>
            public string ip { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public Location location { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> domains { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public As aas { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string isp { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public Proxy proxy { get; set; }

        }
    }
}

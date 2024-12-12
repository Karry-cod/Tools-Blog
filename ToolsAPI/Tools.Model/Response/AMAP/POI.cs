using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Model.Response.AMAP
{
    public class POIRes
    {
        public string status { get; set; }
        public string info { get; set; }
        public string infocode { get; set; }
        public string count { get; set; }
        public List<POI> pois { get; set; }
    }

    public class POI {
        public string name { get; set; }
        public string id { get; set; }
        public string location { get; set; }
        public string type { get; set; }
        public string typecode { get; set; }
        public string pname { get; set; }
        public string cityname { get; set; }
        public string adname { get; set; }
        public string address { get; set; }
        public string pcode { get; set; }
        public string adcode { get; set; }
        public string citycode { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Model.Response
{
    public class BlueTooth_UsersRes
    {
        public string BtuId { get; set; } = null!;
        public string? BlueToothAddress { get; set; }
        public string? BlueToothName { get; set; }
        public string? BlueToothType { get; set; }
        public string? Uid { get; set; }
        public string? Uname { get; set; }
        public string? Uavatar { get; set; }
        public string? CreatedTime { get; set; }
    }
}

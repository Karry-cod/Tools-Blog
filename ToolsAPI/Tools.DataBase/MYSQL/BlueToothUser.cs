using System;
using System.Collections.Generic;

namespace Tools.DataBase.MYSQL
{
    public partial class BlueToothUser
    {
        public string BtuId { get; set; } = null!;
        public string? BlueToothAddress { get; set; }
        public string? BlueToothName { get; set; }
        public string? BlueToothType { get; set; }
        public string? Uid { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}

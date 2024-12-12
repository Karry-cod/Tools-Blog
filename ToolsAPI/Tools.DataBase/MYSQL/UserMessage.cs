using System;
using System.Collections.Generic;

namespace Tools.DataBase.MYSQL
{
    public partial class UserMessage
    {
        public string UmId { get; set; } = null!;
        public string? Type { get; set; }
        public string? Message { get; set; }
        public string? MainInfo { get; set; }
        public string? Target { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}

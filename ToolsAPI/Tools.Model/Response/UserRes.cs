using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Model.Response
{
    public class UserRes
    {
        public string UId { get; set; } = null!;
        public string? UName { get; set; }
        public string? UAccount { get; set; }
        public string? UPassword { get; set; }
        public string? UEmail { get; set; }
        public string? UAvatar { get; set; }
        public string? UIntroduce { get; set; }
        public string? UPhone { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? USex { get; set; }
        public string? UIP { get; set; }
        public string? UIpAddress { get; set; }
    }
}

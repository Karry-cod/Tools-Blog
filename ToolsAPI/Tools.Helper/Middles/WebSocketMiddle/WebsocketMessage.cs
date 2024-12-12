using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Helper.Middles.WebSocketMiddle
{
    public class WebsocketMessage
    {
        public string? SendClientId { get; set; }
        public string? messageId { get; set; }
        public string? action { get; set; }
        public string? roomNo { get; set; }
        public string? nick { get; set; }
        public string? msg { get; set; }
        public string? uid { get; set; }
        public string? uAvatar { get; set; }
        public string? createdTime { get; set; }
    }
}

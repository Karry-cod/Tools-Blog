using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Helper.Middles.WebSocketMiddle
{
    public class WebsocketClient
    {
        public string? Id { get; set; }
        public string? RoomNo { get; set; }
        public WebSocket? WebSocket { get; set; }

        /// <summary>
        /// 向客户端推送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="websocketClient"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(string msg, WebsocketClient websocketClient)
        {
            CancellationToken cancellation = default(CancellationToken);
            var buf = Encoding.UTF8.GetBytes(msg);
            var segment = new ArraySegment<byte>(buf);
            await websocketClient.WebSocket.SendAsync(segment, WebSocketMessageType.Text, true, cancellation);
        }
    }
}

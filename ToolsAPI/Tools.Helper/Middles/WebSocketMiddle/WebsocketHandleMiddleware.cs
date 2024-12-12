using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Helper.Middles.WebSocketMiddle
{
    public class WebSocketHandleMiddleware: IMiddleware
    {
        //private readonly RequestDelegate next;

        //public WebSocketHandleMiddleware(RequestDelegate _next)
        //{
        //    next = _next ?? throw new ArgumentNullException(nameof(next));
        //}

        /// <summary>
        /// 每个自定义中间件都需要有一个Invoke来拦截请求并处理,RequestDelegate是必须的
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path == "/ws")
            {
                //仅当网页执行new WebSocket("ws://localhost:5000/ws")时，后台会执行此逻辑
                if (context.WebSockets.IsWebSocketRequest)
                {
                    //后台成功接收到连接请求并建立连接后，前台的webSocket.onopen = function (event){}才执行
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    string clientId = Guid.NewGuid().ToString();
                    var wsClient = new WebsocketClient
                    {
                        Id = clientId,
                        WebSocket = webSocket
                    };
                    try
                    {
                        await Handle(wsClient);
                    }
                    catch (Exception ex)
                    {
                        await context.Response.WriteAsync("closed," + ex.Message);
                    }
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
            else
            {
                await next(context);
            }
        }

        /// <summary>
        /// 采用长轮询监听客户端发送给服务端的消息
        /// </summary>
        /// <param name="websocketClient"></param>
        /// <returns></returns>
        private async Task Handle(WebsocketClient websocketClient)
        {

            WebSocketClientCollection.Add(websocketClient);

            WebSocketReceiveResult clientData = null;
            do
            {
                var buffer = new byte[1024 * 1];
                //客户端与服务器成功建立连接后，服务器会循环异步接收客户端发送的消息，收到消息后就会执行Handle(WebsocketClient websocketClient)中的do{}while;直到客户端断开连接
                //不同的客户端向服务器发送消息后台执行do{}while;时，websocketClient实参是不同的，它与客户端一一对应
                //同一个客户端向服务器多次发送消息后台执行do{}while;时，websocketClient实参是相同的
                clientData = await websocketClient.WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                // 接收消息
                if (clientData.MessageType == WebSocketMessageType.Text && !clientData.CloseStatus.HasValue)
                {
                    var msgString = Encoding.UTF8.GetString(buffer);
                    var message = JsonConvert.DeserializeObject<WebsocketMessage>(msgString);
                    message.SendClientId = websocketClient.Id;
                    HandleMessage(message, websocketClient);
                }
            } while (!clientData.CloseStatus.HasValue);
            //关掉使用WebSocket连接的网页/调用webSocket.close()后，与之对应的后台会跳出循环
            WebSocketClientCollection.Remove(websocketClient);
        }

        /// <summary>
        /// 给客户端发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="websocketClient"></param>
        private void HandleMessage(WebsocketMessage message, WebsocketClient websocketClient)
        {
            var client = WebSocketClientCollection.Get(message.SendClientId);
            message.messageId = Guid.NewGuid().ToString().Replace("-", "");
            message.createdTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            switch (message.action)
            {
                case "join":
                    client.RoomNo = message.roomNo;
                    client.SendMessageAsync($"{message.nick} join room {client.RoomNo} success .", websocketClient);
                    break;
                case "send_to_room":
                    if (string.IsNullOrEmpty(client.RoomNo))
                    {
                        break;
                    }
                    var clients = WebSocketClientCollection.GetClientsByRoomNo(client.RoomNo);
                    clients.ForEach(c =>
                    {
                        c.SendMessageAsync(JsonConvert.SerializeObject(message), c);
                    });
                    break;
                case "leave":
                    #region 通过把连接的RoomNo置空模拟关闭连接
                    var roomNo = client.RoomNo;
                    client.RoomNo = "";
                    #endregion

                    client.SendMessageAsync($"{message.nick} leave room {roomNo} success .", websocketClient);

                    #region 后台关闭连接
                    client.WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    WebSocketClientCollection.Remove(client);
                    #endregion
                    break;
                default:
                    break;
            }
        }
    }
}

import type { IWebSocket } from "../interfaces/IWebSocket" // 接口Interface
import store from "../store";


export default class WebSocketHandle {
    private webSocket: WebSocket;
    private nick: string;
    private roomNo: string;
    private uid: string;

    constructor(webSocket: WebSocket, nick: string, roomNo: string, uid: string) {
        this.webSocket = webSocket;
        this.nick = nick;
        this.roomNo = roomNo;
        this.uid = uid;
    }

    // 打开连接
    open = () => {
        this.webSocket.onopen = (e) => {
            console.log("Connection opened...", this);
            this.sendMsg({
                action: "join",
                msg: "我加入进来了！",
                nick: this.nick,
                roomNo: this.roomNo,
                uid: this.uid,
                uAvatar: store.state.localUser.uAvatar ?? ""
            });
        };
    };

    // 发送消息
    sendMsg = (message: IWebSocket) => {
        console.log("Send message:", message);
        this.webSocket.send(JSON.stringify({
            action: message.action ? message.action : "send_to_room",
            msg: message.msg,
            nick: message.nick,
            roomNo: message.roomNo,
            uid: message.uid,
            uAvatar: message.uAvatar
        }));
    };

    // 接收信息
    receiveMsg = (data: any) => {
        this.webSocket.onmessage = function (event) {
            console.log("Received message: " + event.data);
            if (event.data.toJohn() != "false") {
                data.push(event.data.toJohn());
            }
        };
        return data;
    };

    // 断开连接
    closeConnect = () => {
        console.log("Connection breaking...");
        this.webSocket.send(JSON.stringify({
            action: "leave",
            msg: "close",
            nick: this.nick,
            roomNo: this.roomNo
        }));
    }
}
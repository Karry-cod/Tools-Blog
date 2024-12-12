import type { IUser } from "./IUser"

// 聊天
export interface ICommunicate{
    commId: string,
    user: IUser,
    content: string
}

// 聊天室
export interface ICommRoom {
    roomId: string,
    roomNo: string,
    roomName: string,
    roomAvatar: string,
    createdTime: string,
    lastCommTime: string
}

// 聊天消息
export interface ICommMessage {
    SendClientId: string,
    action: string,
    roomNo: string,
    nick: string,
    msg: string,
    uid: string,
    uAvatar: string,
    createdTime: string,
    messageId: string
}
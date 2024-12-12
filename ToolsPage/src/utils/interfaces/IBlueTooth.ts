// 蓝牙设备
export interface IBlueToothRadio {
    name: string,
    address: string,
    mode: string,
    driviceName: string
}

// 用户绑定蓝牙信息
export interface IBlueTooth_Users{
    btuId:string,
    blueToothAddress: string,
    blueToothName: string,
    uid: string,
    uname: string,
    uavatar: string,
    createdTime: string
}

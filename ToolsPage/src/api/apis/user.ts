import type { IApiResult } from "@/utils/interfaces/IApiResult";
import Request from "../index"
import type { IUser, IUserService } from "@/utils/interfaces/IUser"

class UserService implements IUserService {
    constructor(){};

    /**
     * 用户登录
     * @param accoutn 用户名
     * @param password 密码
     * @returns 
     */
    async Login(params: any): Promise<IApiResult>{
        return await Request.get("Users/Login", params);
    }

    /**
     * 给电子邮箱发送验证码
     * @param params 
     * @returns 
     */
    async CreateByEmail(params: any): Promise<IApiResult> {
        return await Request.get("Users/CreateByEmail", params);
    }

    /**
     * 给手机号码发送验证码
     * @param params 
     * @returns 
     */
    async CreateByPhone(params: any): Promise<IApiResult> {
        return await Request.get("Users/CreateByPhone", params);
    }

    /**
     * 提交登录申请
     * @param target 请求账户
     * @param type 请求类型
     * @param password 密码
     * @param code 验证码
     */
    async ConfirmCreate(params: any): Promise<IApiResult> {
        return await Request.get("Users/ConfirmCreate", params);
    }

    /**
     * 获取当前用户信息
     * @param params 
     * @returns 
     */
    async GetLocalUser(params?: any): Promise<IApiResult> {
        return await Request.get("Users/GetLocalUser", params);
    }

     /**
     * 获取用户列表
     * @param params 
     * @returns 
     */
    async GetUsers(params?: any): Promise<IApiResult> {
        return await Request.get("Users/GetUsers", params);
    }

    //#region 蓝牙功能

    /**
     * 获取当前用户已绑定的蓝牙设备信息列表
     */
    async GetMyBlueTooths(params?: any): Promise<IApiResult> {
        return await Request.get("Users/GetMyBlueTooths", params);
    }

    /**
     * 获取当前蓝牙设备信息
     */
    async GetCurrentBlueTooth(params?: any): Promise<IApiResult> {
        return await Request.get("Users/GetCurrentBlueTooth", params);
    }

    /**
     * 绑定当前蓝牙设备
     */
    async BindBlueToothByUser(params?: any): Promise<IApiResult> {
        return await Request.get("Users/BindBlueToothByUser", params);
    }

    /**
     * 解除绑定的蓝牙设备
     * @param btId 用户与蓝牙绑定的关联id
     */
    async CancelBindBlueTooth(params: any): Promise<IApiResult> {
        return await Request.get("Users/CancelBindBlueTooth", params);
    }

    /**
     * 获取附近可检测的蓝牙设备列表
     * @param params 
     * @returns 
     */
    async GetBlueTooths(params?: any): Promise<IApiResult> {
        return await Request.get("Users/GetBlueTooths", params);
    }
    //#endregion
}

export default new UserService();
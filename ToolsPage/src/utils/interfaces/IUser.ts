import type { IApiResult } from "./IApiResult";

export interface IUser{
    uId: string,
    uName?: string,
    uAccount?: string,
    uPassword?: string,
    uEmail?: string,
    uAvatar?: string,
    uIntroduce?: string,
    uPhone?: string,
    createdTime?: string,
    uSex?: number,
    uIpAddress?: string
}

export interface IUserService{
    /**
     * 用户登录
     */
    Login(account: string, password: string): Promise<IApiResult>;

    /**
     * 给邮箱发送验证码
     */
    CreateByEmail(email: string): Promise<IApiResult>;

    /**
     * 给手机号码发送验证码
     */
    CreateByPhone(phone: string): Promise<IApiResult>;

    ConfirmCreate(target: string, type: string, password: string, code: string): Promise<IApiResult>;
}
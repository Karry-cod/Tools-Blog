import type { IApiResult } from "@/utils/interfaces/IApiResult";
import request from "@/utils/request";

class Request {
    async get(url: string, params?: any): Promise<IApiResult>{
        var res: IApiResult = await request({
            method: "get",
            params,
            url
        });
        return res.data;
    };
    async post(url: string, data?: any): Promise<IApiResult>{
        var res: IApiResult = await request({
            method: "post",
            data,
            url
        });
        return res.data;
    }
};

export default new Request();
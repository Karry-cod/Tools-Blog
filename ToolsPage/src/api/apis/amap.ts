import type { IApiResult } from "@/utils/interfaces/IApiResult";
import Request from "../index"

/**
 * 高德地图
 */
class AmapService {
    /**
     * 通过关键字获取地点信息
     * @param params 
     * @returns 
     */
    async GetPOI2(params: any): Promise<IApiResult>{
        return await Request.get("AMAP/GetPOI2", params);
    }

}

export default new AmapService();
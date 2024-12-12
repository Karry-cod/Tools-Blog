import Request from "../index"

class Tools {
    /**
     * 内容转二维码 - PNG
     * @param data 需要转换的二维码内容
     */
    QrCode(params: any){
        return Request.get("Tools/CreateQrCode", params);
    }
};

export default new Tools();
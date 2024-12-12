export interface IApiResult {
    code: number;
    message: string;
    data: any;
}

export interface IAxiosResult {
    data: IApiResult;
    status: number;
    statusText: string;
    headers: any;
    config: any;
    request?: any;
}
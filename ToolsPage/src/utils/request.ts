import axios from "axios";
import nprogress from "nprogress";
import { ElMessage } from "element-plus";
import router from "@/router";
import Cookie from "@/utils/cookie"

const service = axios.create({
    // 服务器路径
    baseURL: import.meta.env.DEV ? "http://localhost:5103/api/" : "http://karry.org.cn:5000/api/",
    // 本地路径
    // baseURL: "http://localhost:5103/api/", // url = base url + request url
    // withCredentials: true, // send cookies when cross-domain requests
    timeout: 10000, // request timeout
});

// #region 请求拦截
service.interceptors.request.use(
    (config: any) => {
        nprogress.start();
        if(Cookie.get("wk_cookie")){
            config.headers.wk_cookie = Cookie.get("wk_cookie");
        }
        // if (getToken()) {
        //     config.headers.Authorization = "Bearer " + getToken();
        // }
        return config;
    },
    (error) => {
        console.log(error); // for debug
        return Promise.reject(error);
    }
);
//#endregion


// #region 响应拦截
service.interceptors.response.use(
    (response) => {
        const res = response.data;
        nprogress.done();
        if (response.status !== 200) {
            ElMessage({
                message: res.message,
                type: "error",
            });
            return Promise.reject(new Error(res.message || "Error"));
        } else {
            return Promise.resolve(response);
        }
    },
    (error) => {
        console.log(error);
        if (error.response.status != undefined) {
            if (error.response.status == 401) {
                ElMessage({
                    message: "您的登录已过期或失效，请再次前往登录!!!",
                    type: "error",
                });
                router.push("/");
            } else {
                ElMessage({
                    message: error.message,
                    type: "error",
                });
            }
        }
        return Promise.reject(error);
    }
);
//#endregion

export default service;
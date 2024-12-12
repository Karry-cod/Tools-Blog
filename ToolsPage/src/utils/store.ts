import { reactive } from "vue"
import type { IUser } from "./interfaces/IUser";
import { ElMessage } from "element-plus";

const store = {
    state: reactive({
        editor: {
            tinyHtml: ""
        },
        config: <any>{}, // 配置文件内容
        localUser: <IUser>{},
        isShowLogin: false, // 是否显示登录窗口
        vistPath: import.meta.env.DEV ? "http://127.0.0.1:5173/" : "http://karry.org.cn:5001/" // 根据所在环境访问不同环境的接口
    }),
    methods:{
        /**
         * 整数转金钱格式
         * @param num 整数 
         * @returns 
         */
        toThousands(num: number) {
            return (num || 0).toString().replace(/(\d)(?=(?:\d{3})+$)/g, '$1,');
        },
        /**
         * 金钱转金钱格式 - 带有货币符号和属性的货币格式
         * @param num 金钱
         * @returns 
         */
        formatToIntlStyle(price: number, type: string = 'zh-CN', style: string = 'currency', currency: string = 'CNY'){
            const formatter = new Intl.NumberFormat(type, {
                style,
                currency, // 货币符号
                minimumFractionDigits: 2, // 最少保留小数位数
                maximumFractionDigits: 2, // 最多保留小数位数
            });
            return formatter.format(price);
        },
        /**
         * 金钱转金钱格式 - 不带格式
         * @param num 金钱
         * @returns 
         */
        formatToIntl(price: number){
            const formatter = new Intl.NumberFormat('zh-CN', {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2,
            });
            return formatter.format(price);
        },
        /**
         * 获取本机真实IP地址
         */
        async getRealIP(){
            let ip = "";
            await fetch("https://api.ipify.org/?format=json")
                .then(response => response.json())
                .then(data => {
                    ip = data.ip;
                })
                .catch(error => ElMessage.error(error.message));
            return ip;
        },
        /**
         * 获取用户性别
         */
        getUSex(sex: number){
            if(sex === 1){
                return "男";
            }else{
                return "女";
            }
        },
        /**
         * 将关键信息标红
         */
        redMainText(initText: string, mainText: string){
            return initText.replace(mainText, `<span style='color: red'>${mainText}</span>`);
        },
        /**
         * 获取图片的静态资源地址
         */
        getImgStaticUrl(url: string){
            return new URL(url, import.meta.url).href;
        }
    }
};

export default store;
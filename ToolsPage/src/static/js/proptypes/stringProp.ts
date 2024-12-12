// 声明自定义方法
declare interface String{
    isNull(): boolean;
    parseInt(): number;
    toJohn(): any;
    toFullTime(): string;
}

/**
 * 判断字符串是否为空
 * @returns Boolean，为空：true，反之：false
 */
String.prototype.isNull = function () : boolean {
    if (this === "" || this === null || this === undefined) {
        return true;
    }
    return false;
}

/**
 * 将字符串转换成number，失败返回0
 * @returns Number，成功：number，反之：0
 */
String.prototype.parseInt = function () : number {
    try{
        return parseInt(this.toString());
    }catch{
        return 0;
    }
}

/**
 * 将字符串转JSON，失败则返回原字符串
 * @returns any, 成功：object, 反之： 原字符串
 */
String.prototype.toJohn = function () : any {
    let str: string = this.toString();
    try{
        str = JSON.parse(str);
    }catch{
        str = "false";
    }finally{
        return str;
    }
}

/**
 * 将不规则时间字符串转成yyyy-MM:dd mm:HH:ss格式
 */
String.prototype.toFullTime = function () : any {
    return this.toString().replace("T", " ");
}
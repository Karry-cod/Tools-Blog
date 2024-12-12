import { ElNotification } from 'element-plus'

/**
 * 通知栏
 */
export default class Notification {
    private title; // 标题
    private message; // 消息内容

    constructor(title: string, message: string) {
        this.title = title;
        this.message = message;
    }

    // #region 实例方法
    /**
     * 警告
     * @param title 标题
     * @param message 消息内容
     */
    Warning(title: string = this.title, message: string = this.message): void {
        ElNotification({
            title,
            message,
            type: 'warning',
        })
    }

    /**
     * 成功
     * @param title 标题
     * @param message 消息内容
     */
    Success(title: string = this.title, message: string = this.message): void {
        ElNotification({
            title,
            message,
            type: 'success',
        })
    }

    /**
     * 错误
     * @param title 标题
     * @param message 消息内容
     */
    Error(title: string = this.title, message: string = this.message): void {
        ElNotification({
            title,
            message,
            type: 'error',
        })
    }

    /**
     * 消息
     * @param title 标题
     * @param message 消息内容
     */
    Info(title: string = this.title, message: string = this.message): void {
        ElNotification({
            title,
            message,
            type: 'info',
        })
    }
    // #endregion

    // #region 静态方法
    /**
     * 警告
     * @param title 标题
     * @param message 消息内容
     */
    static Warning(title: string = "通知", message: string = "消息"): void {
        ElNotification({
            title,
            message,
            type: 'warning',
        })
    }

    /**
     * 成功
     * @param title 标题
     * @param message 消息内容
     */
    static Success(title: string = "通知", message: string = "消息"): void {
        ElNotification({
            title,
            message,
            type: 'success',
        })
    }

    /**
     * 错误
     * @param title 标题
     * @param message 消息内容
     */
    static Error(title: string = "通知", message: string = "消息"): void {
        ElNotification({
            title,
            message,
            type: 'error',
        })
    }

    /**
     * 消息
     * @param title 标题
     * @param message 消息内容
     */
    static Info(title: string = "通知", message: string = "消息"): void {
        ElNotification({
            title,
            message,
            type: 'info',
        })
    }
    // #endregion 
}

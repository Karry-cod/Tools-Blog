class Cookie {
    /**
     * 获取cookie值
     * @param key 键
     * @returns 
     */
    get(key: string){
        return localStorage.getItem(key);
    }

    /**
     * 设置cookie值
     * @param key 键
     * @param value 值
     */
    set(key: string, value: any){
        localStorage.setItem(key, value);
    }

    /**
     * 移除cookie值
     * @param key 键
     */
    remove(key: string){
        localStorage.removeItem(key);
    }
}

export default new Cookie();
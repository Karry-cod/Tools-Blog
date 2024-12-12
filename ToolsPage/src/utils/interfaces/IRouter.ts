export declare interface IMeta {
    title: String;
}

export declare interface IRouter {
    path: String;
    name: String;
    component: any;
    meta: IMeta;
};
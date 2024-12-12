export declare interface IMainMenu {
    id?: string | number;
    isActive?: boolean;
    iconUrl?: string;
    title?: string;
    childMenus?: Array<IChildMenu>
}

export declare interface IChildMenu {
    id?: string | number;
    path?: string;
    title?: string;
    isActive?: boolean;
}
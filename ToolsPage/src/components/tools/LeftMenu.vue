<template>
    <div class="box">
        <!-- 主菜单 -->
        <div class="mainMenuBox">
            <div class="menuIcon" v-for="item, index in menus" :class="item.isActive ? 'active' : ''"
                @click="changeMainMenu(index)" :key="item.id">
                <img :src="item.iconUrl" class="iconImg" :alt="item.title" />
            </div>
        </div>
        <!-- 子菜单 -->
        <div class="childMenuBox">
            <div v-for="item, index in childMenus" class="childMenus" :class="item.isActive ? 'active' : ''"
                @click="changeChildMenu(index)">
                {{ item.title }}
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, reactive, toRefs } from "vue"
import router from "@/router"
import type { IMainMenu, IChildMenu } from "@/utils/interfaces/ILeftMenu.ts"

export default defineComponent({
    setup() {
        let data = reactive({
            menus: <Array<IMainMenu>>[
                {
                    id: 1,
                    isActive: true,
                    title: "HTML",
                    iconUrl: "https://www.runoob.com/wp-content/uploads/2013/07/pic_html5.gif",
                    childMenus: [{
                        id: 1,
                        isActive: false,
                        title: "放大镜",
                        path: "/tools/magnifier"
                    }, {
                        id: 2,
                        isActive: false,
                        title: "物体碰撞",
                        path: "/tools/collision"
                    }, {
                        id: 3,
                        isActive: false,
                        title: "页面特效",
                        path: "/tools/pageStyle"
                    },{
                        id: 4,
                        isActive: false,
                        title: "页面转换",
                        path: "/tools/pageConvert"
                    },{
                        id: 5,
                        isActive: false,
                        title: "富文本编辑器",
                        path: "/tools/editor"
                    },{
                        id: 6,
                        isActive: false,
                        title: "跨端动画交互",
                        path: "/tools/skipWeb"
                    },{
                        id: 7,
                        isActive: false,
                        title: "聊天框",
                        path: "/tools/communicate"
                    },{
                        id: 8,
                        isActive: false,
                        title: "易笔记",
                        path: "/tools/yNoteBook"
                    },{
                        id: 9,
                        isActive: false,
                        title: "地图",
                        path: "/tools/map"
                    },{
                        id: 10,
                        isActive: false,
                        title: "剪切板",
                        path: "/tools/clipBoard"
                    },{
                        id: 11,
                        isActive: false,
                        title: "时间轴",
                        path: "/tools/timeLine"
                    },{
                        id: 12,
                        isActive: false,
                        title: "低代码 - vForm",
                        path: "/tools/vForm"
                    },{
                        id: 13,
                        isActive: false,
                        title: "画图工具",
                        path: "/tools/painting"
                    }]
                },
                {
                    id: 2,
                    isActive: false,
                    title: "C#",
                    iconUrl: "https://visualstudio.microsoft.com/wp-content/uploads/2022/09/VisualStudio2022.svg",
                    childMenus: [{
                        id: 1,
                        isActive: false,
                        title: "二维码",
                        path: "/tools/qrCode"
                    }, {
                        id: 2,
                        isActive: false,
                        title: "蓝牙功能",
                        path: "/tools/blueTooth"
                    }, {
                        id: 3,
                        isActive: false,
                        title: "测试2345 - 2"
                    }]
                },
                {
                    id: 3,
                    isActive: false,
                    title: "GAME", // import("../../assets/imgs/MenuIcons/switch-fill.png"),
                    iconUrl: new URL('../../assets/imgs/MenuIcons/switch-fill.png', import.meta.url).href,
                    childMenus: [{
                        id: 1,
                        isActive: false,
                        title: "飞机大战",
                        path: "/tools/planeWar"
                    }, {
                        id: 2,
                        isActive: false,
                        title: "大鱼吃小鱼",
                        path: "/tools/fishEat"
                    }, {
                        id: 3,
                        isActive: false,
                        title: "水了个果",
                        path: "/tools/fruit"
                    }, {
                        id: 4,
                        isActive: false,
                        title: "蹦蹦鸟",
                        path: "/tools/bird"
                    },{
                        id: 5,
                        isActive: false,
                        title: "飞行棋",
                        path: "/tools/flyPlane"
                    },{
                        id: 6,
                        isActive: false,
                        title: "unityWebgl",
                        path: "/tools/webglUnity"
                    },{
                        id: 7,
                        isActive: false,
                        title: "第三人称跑酷",
                        path: "/tools/thirdPerson"
                    }]
                }
            ],
            childMenus: <Array<IChildMenu>>[]
        });

        const methods = {
            // 切换主菜单
            changeMainMenu(index: number) {
                data.menus[index].isActive = true;
                data.childMenus = data.menus[index].childMenus ?? [];
                data.menus.forEach((d, i) => {
                    if (i != index) {
                        d.isActive = false;
                    }
                });
            },
            // 切换子菜单
            changeChildMenu(index: number) {
                data.menus.forEach((d, i) => {
                    d.childMenus?.forEach(c => {
                        c.isActive = false;
                    })
                });
                data.childMenus.forEach((d, i) => {
                    if (i != index) {
                        d.isActive = false;
                    }
                });
                data.childMenus[index].isActive = true;
                router.push(data.childMenus[index].path ?? "/404");
            }
        };

        const privateMethods = {
            // 设置页面初始值
            settingVal(){
                data.childMenus[0].isActive = true;
                router.push(data.childMenus[0].path ?? "/404");
            }
        };

        onMounted(() => {
            methods.changeMainMenu(0);
            privateMethods.settingVal();
        });

        return {
            ...toRefs(data),
            ...methods
        };
    }
})
</script>

<style lang="scss">
.box {
    display: flex;

    .mainMenuBox {
        border-right: var(--box-border);
        overflow: hidden;

        >.menuIcon {
            padding: 10px;
            transition: 0.2s;
            position: relative;
            cursor: var(--cursor-pointer);

            &:before {
                content: "";
                position: absolute;
                width: 60px;
                height: 60px;
                top: 0px;
                right: -60px;
                transition: 0.3s;
                background-color: rgb(8, 8, 236);
                opacity: 0.4;
                cursor: var(--cursor-pointer);
            }

            &:hover {
                &:before {
                    right: 0px;
                }
            }


            >.iconImg {
                width: 40px;
                height: 40px;
                position: relative;
            }
        }

        >.active {
            &:before {
                right: 0px;
            }
        }
    }

    .childMenuBox {
        width: calc(100% - 60px);

        >.childMenus {
            padding-left: 15px;
            height: 25px;
            margin-top: 10px;
            line-height: 25px;
            font-size: 1rem;
            cursor: var(--cursor-pointer);
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            transition: 0.2s;

            &:hover {
                font-size: 1.1rem;
                color: green;
            }
        }

        >.active {
            color: green;
            font-weight: 600;
            font-size: 1.1rem;
        }
    }
}
</style>
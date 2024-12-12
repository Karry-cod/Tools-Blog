const htmls = [{
    path: "magnifier",
    name: "magnifier",
    component: () => import("@/views/tools/html/magnifier.vue"),
    meta: {
        title: "放大镜"
    }
},{
    path: "collision",
    name: "collision",
    component: () => import("@/views/tools/html/collision.vue"),
    meta: {
        title: "物体碰撞"
    }
},{
    path: "pageStyle",
    name: "pageStyle",
    component: () => import("@/views/tools/html/pageStyle.vue"),
    meta: {
        title: "页面特效"
    }
},{
    path: "pageConvert",
    name: "pageConvert",
    component: () => import("@/views/tools/html/pageConvert.vue"),
    meta: {
        title: "页面转换"
    }
},{
    path: "editor",
    name: "editor",
    component: () => import("@/views/tools/html/editor.vue"),
    meta: {
        title: "富文本编辑器"
    }
},{
    path: "skipWeb",
    name: "skipWeb",
    component: () => import("@/views/tools/html/skipWeb.vue"),
    meta: {
        title: "跨端交互"
    }
},{
    path: "communicate",
    name: "communicate",
    component: () => import("@/views/tools/html/communicate.vue"),
    meta: {
        title: "聊天框"
    }
},{
    path: "yNoteBook",
    name: "yNoteBook",
    component: () => import("@/views/tools/html/yNoteBook.vue"),
    meta: {
        title: "易笔记"
    }
},{
    path: "map",
    name: "map",
    component: () => import("@/views/tools/html/map.vue"),
    meta: {
        title: "地图"
    }
},{
    path: "clipBoard",
    name: "clipBoard",
    component: () => import("@/views/tools/html/clipBoard.vue"),
    meta: {
        title: "剪切板"
    }
},{
    path: "timeLine",
    name: "timeLine",
    component: () => import("@/views/tools/html/timeLine.vue"),
    meta: {
        title: "时间轴"
    }
},{
    path: "vForm",
    name: "vForm",
    component: () => import("@/views/tools/html/vForm.vue"),
    meta: {
        title: "低代码 - vForm"
    }
},{
    path: "painting",
    name: "painting",
    component: () => import("@/views/tools/html/painting/index.vue"),
    meta: {
        title: "画图工具"
    }
}];

const net = [{
    path: "qrCode",
    name: "qrCode",
    component: () => import("@/views/tools/net/qrCode.vue"),
    meta: {
        title: "二维码"
    }
},{
    path: "blueTooth",
    name: "blueTooth",
    component: () => import("@/views/tools/net/blueTooth.vue"),
    meta: {
        title: "蓝牙功能"
    }
}];

const games = [{
    path: "planeWar",
    name: "planeWar",
    component: () => import("@/views/tools/games/planeWar.vue"),
    meta: {
        title: "飞机大战"
    }
},{
    path: "fishEat",
    name: "fishEat",
    component: () => import("@/views/tools/games/fishEat.vue"),
    meta: {
        title: "大鱼吃小鱼"
    }
},{
    path: "fruit",
    name: "fruit",
    component: () => import("@/views/tools/games/fruit.vue"),
    meta: {
        title: "水了个果"
    }
},{
    path: "bird",
    name: "bird",
    component: () => import("@/views/tools/games/bird.vue"),
    meta: {
        title: "蹦蹦鸟"
    }
},{
    path: "flyPlane",
    name: "flyPlane",
    component: () => import("@/views/tools/games/flyPlane.vue"),
    meta: {
        title: "飞行棋"
    }
},{
    path: "webglUnity",
    name: "webglUnity",
    component: () => import("@/views/tools/games/webglUnity.vue"),
    meta: {
        title: "unityWebgl"
    }
},{
    path: "thirdPerson",
    name: "thirdPerson",
    component: () => import("@/views/tools/games/thirdPerson.vue"),
    meta: {
        title: "第三人称跑酷"
    }
}];

const routes = [
    ...htmls,
    ...net,
    ...games
];



export default routes;
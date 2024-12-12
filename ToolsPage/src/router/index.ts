import {
  createRouter,
  createWebHistory,
  createWebHashHistory,
} from "vue-router";
import NProgress from "nprogress";
import "nprogress/nprogress.css"; //这个样式必须引入
import tools from "./routers/tools";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  // history: createWebHashHistory(),
  routes: [
    {
      path: "/",
      name: "container",
      component: () => import("@/views/container.vue"),
      children: [
        {
          path: "",
          name: "blob",
          component: () => import("@/views/blog/index.vue"),
          meta: {
            title: "个人博客",
          },
        },
        {
          path: "tools",
          name: "tools",
          component: () => import("@/views/tools/index.vue"),
          meta: {
            title: "常用工具",
          },
          children: tools,
        },
        {
          path: "proBank",
          name: "proBank",
          component: () => import("@/views/aboutMe/proBank.vue"),
          meta: {
            title: "项目仓库",
          },
        },
        {
          path: "myTalk",
          name: "myTalk",
          component: () => import("@/views/aboutMe/myTalk.vue"),
          meta: {
            title: "个人感言",
          },
        },
        {
          path: "onlineResume",
          name: "onlineResume",
          component: () => import("@/views/aboutMe/onlineResume.vue"),
          meta: {
            title: "在线简历",
          },
        },
      ],
    },
    {
      path: "/404",
      name: "页面丢失",
      component: () => import("@/views/errorPages/404.vue"),
      meta: {
        title: "页面丢失",
      },
    },
    {
      path: "/mtEdit",
      name: "mtEdit",
      component: () => import("@/views/tools/html/painting/edit/index.vue"),
      meta: {
        title: "绘画-编辑",
      },
    },{
      path: "/mtPreview",
      name: "mtPreview",
      component: () => import("@/views/tools/html/painting/preview/index.vue"),
      meta: {
        title: "绘画-预览",
      },
    },
  ],
});

//全局前置导航守卫
router.beforeEach((to, from, next) => {
  NProgress.start();
  if (!to.meta.title || (to.meta.title as any).isNull()) {
    next("/404");
  }
  (document as any).title = to.meta.title;
  next();
});

// 后置导航
router.afterEach(() => {
  NProgress.done();
});

export default router;

import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import VForm3 from 'vform3-builds'  //引入VForm3库
import VueKonva from 'vue-konva';

const images = import.meta.glob('./assets/**/*.png');

for (const path in images) {
    const image = images[path]();
    // 在这里使用 image
}

//#region 导入Css
import 'element-plus/dist/index.css'
import './static/css/cursorStyle.css'
// import 'element-plus/theme-chalk/dark/css-vars.css' // 暗黑模式
import '@/static/css/pageStyle.scss'
import 'vform3-builds/dist/designer.style.css'  //引入VForm3样式
// #endregion

//#region 导入Js
import "@/static/js/proptypes/stringProp.js"
//#endregion

import App from './App.vue'
import router from './router'

const app = createApp(App)

for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
}

app.use(router)
app.use(ElementPlus)
app.use(VueKonva)
app.use(VForm3)  //全局注册VForm3(同时注册了v-form-designe、v-form-render等组件)
app.mount('#app')

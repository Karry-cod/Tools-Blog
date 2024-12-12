<template>
    <div class="box">
        <!-- 侧拉栏 -->
        <div class="left_Area">
            关于我
        </div>
        <!-- 内容栏 -->
        <div class="center_Area">
            <div class="li_menu" @click="changeRoute('proBank')">
                <el-icon class="li_icons">
                    <ShoppingBag />
                </el-icon>
                项目仓库
            </div>
            <div class="li_menu" @click="changeRoute('myTalk')">
                <el-icon class="li_icons">
                    <Flag />
                </el-icon>
                个人感言
            </div>
            <div class="li_menu" @click="changeRoute('onlineResume')">
                <el-icon class="li_icons">
                    <Stamp />
                </el-icon>
                在线简历
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs } from "vue"
import { ShoppingBag, Flag, Stamp } from "@element-plus/icons-vue"
import router from "@/router";

export default defineComponent({
    setup() {
        let data = reactive({
            pageUrls: [{
                name: "proBank",
                src: "/proBank"
            },{
                name: "myTalk",
                src: "/myTalk"
            },{
                name: "onlineResume",
                src: "/onlineResume"
            }]
        });

        let methods = {
            // 跳转路由
            changeRoute(name: string){
                let url = data.pageUrls.find(d => d.name === name);
                if(url){
                    router.push(url.src);
                }
            }
        };

        return {
            ...toRefs(data),
            ...methods
        };
    }
})
</script>

<style lang="scss" scoped>
.box {
    color: white;
    position: fixed;
    right: 0px;
    top: 50%;
    display: flex;
    transform: translateX(100%);
    cursor: var(--cursor-pointer);
    transition: 0.3s;
    z-index: 1000;

    &:hover {
        transform: translateX(-10px);

        &::before{
            background-image: url("https://wimg.588ku.com/gif/21/05/26/a676f9476033db2ffd553a86fe2d85a8.gif");
            transform: rotate(0deg);
            left: -100px;
            background-size: 120px 120px;
        }
    }

    &::before{
        content: "";
        position: absolute;
        position: absolute;
        left: -50px;
        top: -30px;
        width: 100px;
        height: 100px;
        background-image: url("https://wimg.588ku.com/gif/21/05/14/0e138cd9a60ff955f3562827882092de.gif");
        background-size: 100px 100px;
        transform: rotate(-45deg);
    }

    .left_Area {
        background-color: rgb(86, 86, 231);
        writing-mode: vertical-lr;
        /* 从上到下排列 */
        text-orientation: upright;
        /* 设置文字方向为正立 */
        text-align: center;
        position: relative;
        letter-spacing: 3px;
        border-top-left-radius: 50px;
        border-bottom-left-radius: 50px;
        padding: 3px;
        height: 100px;
    }

    .center_Area {
        background-color: rgba(101, 212, 243, 0.2);
        position: relative;
        color: rgb(11, 196, 237);

        .li_menu {
            padding: 20px;
            padding-top: 10px;
            padding-bottom: 10px;
            letter-spacing: 5px;
            transition: 0.2s;

            &:hover {
                background-color: rgb(107, 193, 243);
                cursor: var(--cursor-pointer);
                color: white;
            }

            .li_icons {
                transform: translateY(2px);
            }
        }
    }
}
</style>
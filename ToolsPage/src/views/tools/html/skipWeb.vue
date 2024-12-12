<template>
    <div class="child_boxSide">
        <div class="title">
            跨端动画交互
        </div>
        <div class="body">
            <el-button type="primary" @click="showPage()">效果演示</el-button>
            <el-dialog v-model="isShow" fullscreen>
                <iframe :src="htmlPageURL" width="100%" :height="height"></iframe>
            </el-dialog>
        </div>
        <!-- 源码路径 -->
        <div class="aBox">
            源码地址：
            <a href="https://blobtools.oss-cn-shanghai.aliyuncs.com/tools/%E8%B7%A8%E9%A1%B5%E9%9D%A2%E5%8A%A8%E7%94%BB%E4%BA%A4%E4%BA%92.rar">源码地址</a>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, reactive, toRefs } from "vue"
import store from "@/utils/store"

export default defineComponent({
    setup() {
        let data = reactive({
            htmlPageURL: store.state.vistPath + `/html/index.html`,
            isShow: false,
            height: 1080
        });

        onMounted(() => {
            console.log(import.meta.env.VITE_APP_ENV);
        })

        const methods = {
            showPage() {
                data.isShow = true;
                window.open(data.htmlPageURL, "_blank", "width=600,height=400");
            }
        };

        return {
            ...methods,
            ...toRefs(data)
        };
    }
})
</script>

<style lang="scss"></style>
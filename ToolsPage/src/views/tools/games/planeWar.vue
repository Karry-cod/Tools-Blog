<template>
    <div class="child_boxSide">
        <div class="title">
            飞机大战
        </div>
        <span style="color: red">* 源码地址在最下方</span>
        <div class="body">
            <iframe :src="htmlPageURL" width="100%" height="100%"></iframe>
        </div>
        <!-- 源码路径 -->
        <div class="aBox">
            源码地址：
            <a href="https://blobtools.oss-cn-shanghai.aliyuncs.com/tools/%E9%A3%9E%E6%9C%BA%E5%A4%A7%E6%88%98.rar">源码地址</a>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs } from "vue"
import store from "@/utils/store"

export default defineComponent({
    setup(){
        let data = reactive({
            htmlPageURL: store.state.vistPath + `/planeWar/index.html`,
        });

        return {
            ...toRefs(data)
        };
    }
})
</script>

<style lang="scss" scoped>
.child_boxSide{
    width: 100%;
    height: 100%;
}
.body{
    // margin-top: 5px;
    height: 80vh;
}
</style>
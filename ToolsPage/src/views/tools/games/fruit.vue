<template>
    <div class="child_boxSide">
        <div class="title">
            水了个果
        </div>
        <div class="body">
            <iframe :src="htmlPageURL" width="100%" height="100%"></iframe>
        </div>
        <!-- 源码路径 -->
        <div class="aBox">
            源码地址：
            <a href="https://blobtools.oss-cn-shanghai.aliyuncs.com/tools/%E5%A4%A7%E9%B1%BC%E5%90%83%E5%B0%8F%E9%B1%BC.rar">源码地址</a>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs } from "vue"
import store from "@/utils/store"

export default defineComponent({
    setup(){
        let data = reactive({
            htmlPageURL: store.state.vistPath + `/fruit/index.html`,
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
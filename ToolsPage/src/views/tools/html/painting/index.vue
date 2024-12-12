<template>
    <div class="child_boxSide">
        <p class="title">画图工具</p>
        <!-- 预览路径 -->
        <div class="aBox">
            <a href="/mtEdit" target="_blank">预览地址</a>
        </div>
        <div class="aBox">
            <a href="http://mt.yaolm.top/guide/get-started-quickly/install.html" target="_blank">官网地址</a>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, reactive, toRefs } from "vue"

export default defineComponent({
    setup() {
        let data = reactive({
        });

        const methods = {

        };

        onMounted(() => {
        });

        return {
            ...toRefs(data)
        };
    }
})

</script>

<style scoped>
.aBox{
    display: block;
}
</style>
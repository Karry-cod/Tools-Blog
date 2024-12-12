<template>
    <div class="child_boxSide">
        <div class="title">
            富文本编辑器 - TinyMCE
        </div>
        <div class="borde">
            <p style="color: red;padding-bottom: 5px">*：由于这个组件需要通过cdn导入到项目中去使用，所以我这边后续会专门发布一篇文章，介绍一下如何使用TinyMCE这个富文本编辑器</p>
            <TEditor/>
        </div>
        <div class="borde">
            <el-button type="primary" :icon="View" @click="isShowDialog = true">预览</el-button>
        </div>
        <div>
            <el-dialog v-model="isShowDialog" title="效果预览" width="90%" height="100vh" top="0px">
                <div v-html="store.state.editor.tinyHtml">
                </div>
            </el-dialog>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs, watchEffect } from "vue"
import TEditor from "@/components/tools/TEditor.vue"
import { View } from "@element-plus/icons-vue"
import store from "@/utils/store"

export default defineComponent({
    components:{
        TEditor,View
    },
    setup(){
        let data = reactive({
            isShowDialog: false
        });

        watchEffect(() => {
            // console.log(store.state.editor.tinyHtml);
        });

        const icons = {
            View
        };

        return {
            ...toRefs(data),
            ...icons,
            store
        };
    }
})
</script>

<style lang="scss" scoped>
.borde{
    padding-top: 10px;
}
</style>
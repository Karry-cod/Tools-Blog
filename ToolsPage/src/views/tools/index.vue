<template>
    <div class="boxSide">
        <!-- 左侧菜单栏 -->
        <div class="leftMenuBox">
            <LeftMenu />
        </div>
        <!-- 右侧中心展示区 -->
        <div class="centerBox">
            <el-scrollbar>
                <RouterView />
            </el-scrollbar>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue"
import LeftMenu from "@/components/tools/LeftMenu.vue"
import { RouterView } from 'vue-router'

export default defineComponent({
    components: {
        LeftMenu
    },
    setup() {

    }
})

</script>

<style scoped lang="scss">
.boxSide {
    display: flex;
    height: calc(100vh - 150px);

    .leftMenuBox {
        width: 20%;
        max-width: 300px;
        border-right: 1px dashed rgb(165, 162, 162, 0.5);
    }

    .centerBox {
        width: 80%;
        padding: 15px;
        max-width: calc(100% - 300px);
    }
}
</style>
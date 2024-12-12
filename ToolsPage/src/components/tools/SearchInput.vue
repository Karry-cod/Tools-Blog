<template>
    <div class="box">
        <el-input :class="inputClass" v-model="searchContent" :prefix-icon="Search" style="border: 0px;" @focus="focusInput()" @blur="blurInput()" placeholder="请输入搜索内容">
        </el-input>
    </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs } from "vue"
import { Search } from "@element-plus/icons-vue"

export default defineComponent({
    components: {
        Search
    },
    setup() {
        let data = reactive({
            inputClass: "searchInput",
            searchContent: ""
        });

        const icons = {
            Search
        };

        const methods = {
            focusInput(){
                data.inputClass += " active";
            },
            blurInput(){
                data.inputClass = "searchInput";
            }
        };

        return {
            ...icons,
            ...toRefs(data),
            ...methods
        };
    }
})
</script>

<style scoped lang="scss">
.box {}

.searchInput {
    // border: 1px solid rgb(206, 201, 201);
    height: 30px;
    width: 200px;
    background-color: transparent;
    // border: 0px;
    border-radius: 3px;
    outline: none;
    transition: 0.3s;
    line-height: 100%;
}

.active {
    height: 40px;
    width: 350px;
    font-size: 18px;
}
</style>
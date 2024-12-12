<template>
    <div class="boxSide">
        <p class="title">线上简历</p>
    </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs } from "vue"

export default defineComponent({
    setup(){
        let data = reactive({});

        return {
            ...toRefs(data)
        };
    }
})
</script>

<style lang="scss"></style>
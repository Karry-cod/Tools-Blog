<template>
    <mt-preview ref="ref_mtPreview"></mt-preview>
</template>

<script lang="ts">
import cookie from '@/utils/cookie';
import { MtPreview, type IExportJson } from 'maotu';
import { defineComponent, onMounted, ref } from "vue"
import 'maotu/dist/style.css';

export default defineComponent({
    components: {
        MtPreview
    },
    setup() {
        const methods = {};

        const refs = {
            ref_mtPreview: ref<InstanceType<typeof MtPreview>>()
        };

        onMounted(() => {
            let previewJson = cookie.get("previewJson") as string;
            console.log(previewJson);
            refs.ref_mtPreview.value?.setImportJson(JSON.parse(previewJson));
        });

        return {
            ...methods,
            ...refs
        };
    }
})
</script>
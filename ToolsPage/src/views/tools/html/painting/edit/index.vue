<template>
    <div style="width: 100vw;height: 100vh;">
        <mt-edit @on-preview-click="preview"></mt-edit>
    </div>
</template>

<script lang="ts">
import { MtEdit, type IExportJson } from 'maotu';
import { defineComponent } from "vue"
import 'maotu/dist/style.css';
import Cookie from "@/utils/cookie"
import router from '@/router';

export default defineComponent({
    components: {
        MtEdit
    },
    setup() {
        const methods = {
            // 预览
            preview(previewJson: IExportJson) {
                Cookie.set("previewJson", JSON.stringify(previewJson));
                const routeUrl = router.resolve({
                    name: 'mtPreview'
                });
                window.open(routeUrl.href, '_blank');
            }
        };

        return {
            ...methods
        };
    }
})
</script>
<template>
    <div class="child_boxSide">
        <div class="title">
            剪切板 - Clipboard API
        </div>
        <div>
            <el-collapse v-model="activeTab">
                <el-collapse-item title="显示剪切板文本" name="1">
                    <div>
                        {{ clipText }}
                    </div>
                </el-collapse-item>
                <el-collapse-item title="显示剪切板图片" name="2">
                    <div id="imgContainer" contenteditable></div>
                </el-collapse-item>
                <el-collapse-item title="是否阻止复制" name="3">
                    <div>
                        <el-switch v-model="isPrevent" @change="changeCopyState"
                            style="--el-switch-on-color: #13ce66; --el-switch-off-color: #ff4949" size="large"
                            active-text="允许" inactive-text="禁止">
                            <template #active-action>
                                <span class="custom-active-action">T</span>
                            </template>
                            <template #inactive-action>
                                <span class="custom-inactive-action">F</span>
                            </template>
                        </el-switch>
                    </div>
                </el-collapse-item>
            </el-collapse>
        </div>
    </div>
</template>

<script lang="ts">
import { ElMessage } from "element-plus";
import { defineComponent, onMounted, reactive, toRefs } from "vue"

export default defineComponent({
    setup() {
        let data = reactive({
            activeTab: "",
            isPrevent: true,
            clipText: ""
        });

        const methods = {
            // 切换复制行为
            changeCopyState(e: any){
                if(e){
                    document.removeEventListener("copy", events.preventCopy);
                }else{
                    document.addEventListener("copy", events.preventCopy);
                }
            }
        };

        const events = {
            // 初始剪切板文本内容
            initClipText() {
                navigator.clipboard.readText().then(text => {
                    data.clipText = text;
                })
            },
            // 监听图片粘贴
            initClipPicture() {
                const imgContainer = document.getElementById("imgContainer");
                (imgContainer as HTMLElement).addEventListener('paste', (e: any) => {
                    if (e.clipboardData.files.length > 0) {
                        e.preventDefault()
                        const file = e.clipboardData.files[0]
                        const reader = new FileReader()
                        reader.onload = e => {
                            const img = document.createElement('img');
                            img.src = e.target.result;
                            imgContainer.appendChild(img);
                        }
                        reader.readAsDataURL(file)
                    }
                })
            },
            // 阻止复制行为
            preventCopy(e: any) {
                // 阻止默认行为 不准复制
                e.preventDefault()
                ElMessage.warning("当前设置下禁止复制喔！");
                // 手动往剪贴板里面写入一段文本
                navigator.clipboard.writeText('不准复制,给我打钱！！！')
            }
        };

        onMounted(() => {
            events.initClipText();
            events.initClipPicture();
        });

        return {
            ...toRefs(data),
            ...methods
        };
    }
})

</script>

<style scoped>
:deep(.el-switch__core) {
    border-radius: 0px;
}

:deep(.el-switch__core .el-switch__action) {
    border-radius: 0px;
}
</style>
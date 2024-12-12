<template>
    <div class="child_boxSide">
        <div class="title">二维码</div>
        <div class="contentBox">
            <el-form label-position="right" label-width="100px">
                <el-form-item label="内容：">
                    <el-input type="textarea" placeholder="请输入二维码内容" v-model="content" :rows="5"></el-input>
                </el-form-item>
                <el-form-item label="">
                    <el-button type="primary" size="large" :icon="Printer" class="square"
                        @click="qrCode('png')">转换PNG</el-button>
                    <el-button type="danger" size="large" :icon="Printer" class="square"
                        @click="qrCode('svg')">转换SVG</el-button>
                </el-form-item>
                <el-form-item label="二维码图片：" v-loading="isLoad">
                    <div v-show="!qrCodeUrl.isNull()" class="imgBox">
                        <img :src="qrCodeUrl" alt="" class="qrCodeImg">
                        <div class="iconBox">
                            <el-icon size="25" @click="downloadImg()" class="pointer">
                                <Download />
                            </el-icon>
                        </div>
                    </div>
                </el-form-item>
            </el-form>
        </div>
        <div class="aBox block">
            <div class="line">
                该功能由后端实现，所用语言：asp.net core 6，API地址： Tools/CreateQrCode
            </div>
            <div class="line">
                Gitee: <a href="https://gitee.com/Xu_zhuojiu/tools-blog/tree/master/ToolsAPI">源码地址</a>
            </div>
            <div class="line">
                Github: <a href="https://gitee.com/Xu_zhuojiu/tools-blog/tree/master/ToolsAPI">源码地址</a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs, onMounted } from "vue"
import { Printer, Download } from "@element-plus/icons-vue"
import Tools from "@/api/apis/tools"
import { ElMessage } from "element-plus";

export default defineComponent({
    components: {
        Printer, Download
    },
    setup() {
        let data = reactive({
            content: "",
            qrCodeUrl: "",
            isLoad: false
        });

        const icons = {
            Printer, Download
        };

        const methods = {
            // 转换二维码 - PNG
            qrCode(type: string) {
                if(type.toLowerCase() === "png"){
                    ElMessage.warning("当前系统暂不支持png格式，请使用svg下载");
                    return;
                }
                data.isLoad = true;
                Tools.QrCode({
                    content: data.content,
                    type
                }).then(res => {
                    if (res.code === 1) {
                        data.qrCodeUrl = res.data[0];
                    } else {
                        ElMessage.error(res.message);
                    }
                    data.isLoad = false;
                });
            },
            // 下载图片
            downloadImg(){
                var a = document.createElement("a");
                a.href = data.qrCodeUrl;
                a.click();
                a.remove();
            }
        };

        onMounted(() => {
            // methods.qrCode_Png();
        })

        return {
            ...icons,
            ...toRefs(data),
            ...methods
        };
    }
})
</script>

<style lang="scss" scoped>
.contentBox {
    margin-top: 10px;

    .square {
        border-radius: 0px;
    }

    .imgBox {

        .qrCodeImg {
            width: 250px;
            height: 250px;
        }

        .iconBox {
            display: inline-block;
            opacity: 0;
            transition: 0.3s;

            .pointer{
                transition: 0.3s;
                padding:3px;
            }
        }

        &:hover{
            .iconBox{
                opacity: 1;

                .pointer:hover{
                    background-color: rgb(202, 201, 201, 0.5);
                }
            }
        }
    }

}
</style>
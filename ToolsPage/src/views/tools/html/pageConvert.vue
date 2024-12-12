<template>
    <div class="child_boxSide" id="body">
        <div class="title">
            页面转换
        </div>
        <div class="box">
            <div class="headBtn">
                <el-button size="large" @click="capture()" type="warning">
                    生成PNG（待完善）
                </el-button>
                <el-button size="large" @click="convertPDF()" type="primary">
                    生成PDF
                </el-button>
            </div>
            <div class="form" id="form">
                <h3>表单</h3>
                <el-form>
                    <el-form-item label="文本：">
                        <input type="text" placeholder="测试" />
                    </el-form-item>
                    <el-form-item label="按钮：">
                        <el-button type="danger">测试</el-button>
                    </el-form-item>
                    <el-form-item label="单选按钮：">
                        <el-radio>测试</el-radio>
                    </el-form-item>
                </el-form>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, nextTick } from "vue"
import html2canvas from 'html2canvas';
import html2pdf from 'html2pdf.js';

export default defineComponent({
    setup() {
        const methods = {
            // 页面生成图片并导出
            async capture() {
                // 获取要捕获的元素
                const captureElement = document.getElementById("form") as HTMLElement;

                await nextTick();

                try {
                    // 使用html2canvas渲染元素为canvas
                    const canvas = await html2canvas(captureElement);

                    // 将canvas转换为DataURL
                    const dataUrl = canvas.toDataURL('image/png');

                    // 创建一个下载链接并模拟点击以下载PNG
                    const a = document.createElement('a');
                    a.href = dataUrl;
                    a.download = 'captured_page.png';
                    a.click();
                } catch (error) {
                    console.error('Error capturing page:', error);
                }
            },
            // 将页面生成PDF并导出
            async convertPDF() {
                const options = {
                    margin: 1,
                    filename: 'my-document.pdf',
                    image: { type: 'jpeg', quality: 0.98 },
                    html2canvas: { dpi: 192, letterRendering: true },
                    jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' },
                };

                try {
                    const element = document.getElementById("form"); // 要导出为PDF的元素ID
                    const pdf = await html2pdf(element, options);
                    pdf.save(); // 保存PDF文件
                } catch (error) {
                    console.error('Error exporting to PDF:', error);
                }
            }
        }

        return {
            ...methods
        };
    }
})
</script>

<style lang="scss" scoped>
.box {
    padding-top: 10px;
    display: block;

    .form{
        padding: 10px;
    }
}
</style>
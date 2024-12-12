<template>
    <div class="child_boxSide">
        <span class="title">放大镜</span>
        <div style="padding-top: 15px">
            <el-upload class="upload-demo" drag multiple :before-upload="beforeUpload">
                <div v-if="!uploadFile">
                    <el-icon class="el-icon--upload"><upload-filled /></el-icon>
                    <div class="el-upload__text">
                        Drop file here or <em>click to upload</em>
                    </div>
                </div>
                <div v-else>
                    <img :src="uploadFile" style="width: 100%" alt="">
                </div>
                <template #tip>
                    <div class="el-upload__tip">
                        jpg/png files with a size less than 500kb
                    </div>
                </template>
            </el-upload>
        </div>
        <div class="center">
            <!-- 图片盒子 -->
            <div class="body">
                <!-- 展示图片 -->
                <div class="imgs">
                    <img class="imgs"
                        :src="uploadFile ?? 'https://img1.baidu.com/it/u=3129770633,3933811923&fm=253&app=138&size=w931&n=0&f=JPEG&fmt=auto?sec=1698858000&t=c6334b2c7566abbdfc28b5309efcf67a'"
                        alt="">
                </div>
                <!-- 放大镜 -->
                <div class="glass">
                    <img :src="uploadFile ?? 'https://img1.baidu.com/it/u=3129770633,3933811923&fm=253&app=138&size=w931&n=0&f=JPEG&fmt=auto?sec=1698858000&t=c6334b2c7566abbdfc28b5309efcf67a'" alt="">
                </div>
            </div>
        </div>
        <!-- 源码路径 -->
        <div class="aBox">
            源码地址：
            <a href="https://blobtools.oss-cn-shanghai.aliyuncs.com/tools/%E6%94%BE%E5%A4%A7%E9%95%9C.rar">源码地址</a>
        </div>
    </div>
</template>

<script lang="ts">
import { ElMessage } from "element-plus";
import { defineComponent, reactive, onMounted, toRefs } from "vue"

export default defineComponent({
    setup() {
        let data = reactive({
            uploadFile: <any>null
        });

        // 倍数（比例），位移量x，位移量y
        let glassList = <any>[]; // 放大镜集合
        let glassScale = {
            scale: 2,
            transX: 0,
            transY: 0,
            glass: null, // 放大镜实例
            img: null // 图片实例
        };

        // 循环所有放大镜，获取其绑定的图片地址
        let glasses = document.getElementsByClassName("glass");
        let i = 0; // 索引
        let glassIndex = -1; // 放大镜标识

        const privateMethods = {
            // 元素初始化
            initPage() {
                while (i < glasses.length) {
                    // 1. 获取其父元素
                    let body = glasses[i].parentElement;
                    // 2. 通过父元素获取其子元素中img绑定的图片地址
                    for (let j = 0; j < body.children.length; j++) {
                        if (body.children[j].className === "imgs") {
                            // 新增放大镜
                            glassList.push(glassScale);
                            glassIndex++;

                            // window.getComputedStyle(Element)：获取元素的实时样式（所有），只读
                            // Element.style：可读可写，只能获取元素的内联样式
                            // 1. 设置放大镜显示的区域
                            let glassBox = glasses[i];
                            glassBox.style.width = window.getComputedStyle(body.children[j]).width.replace("px", "") * 1.2 + "px";
                            glassBox.style.height = window.getComputedStyle(body.children[j]).height.replace("px", "") * 1.2 + "px";
                            glassBox.style.left = body.children[j].clientLeft + 10 + window.getComputedStyle(body.children[j]).width.replace("px", "").parseInt() + "px";


                            // 2. 设置放大镜查看的倍数
                            let glassImg = glasses[i].children[0];
                            glassImg.style.width = window.getComputedStyle(body.children[j]).width.replace("px", "").parseInt() * glassList[glassIndex].scale + "px";
                            glassImg.style.height = window.getComputedStyle(body.children[j]).height.replace("px", "").parseInt() * glassList[glassIndex].scale + "px";
                            glassImg.attributes["src"].value = body.children[j].children[0].attributes["src"].value;

                            // 3. 给每一个被查看的图片绑定鼠标移入事件和移出事件
                            let imgBox = body.children[j];
                            glassList[glassIndex].glass = glassImg;
                            glassList[glassIndex].img = imgBox;
                            imgBox.addEventListener("mouseenter", () => privateMethods.mouseEnter(imgBox, glassImg, glassIndex));
                            imgBox.addEventListener("mouseleave", () => privateMethods.mouseLeave(glassImg));
                            imgBox.addEventListener("wheel", (e) => privateMethods.scroll(e, glassIndex));

                            break;
                        }
                    }
                    i++;
                }
            },
            // 鼠标滑入
            mouseEnter(currentDom: any, dom: any, index: any) {
                // 监听鼠标移动
                currentDom.addEventListener("mousemove", (e: any) => {
                    dom.parentElement.style.display = "block";

                    // 以图片区的左上角为原点，获取鼠标距离原点的x,y偏移量
                    let clientX = e.clientX - currentDom.getBoundingClientRect().left;
                    let clientY = e.clientY - currentDom.getBoundingClientRect().top;

                    // 放大镜 - 位移量
                    // console.log(clientX, currentDom.getBoundingClientRect().left, window.getComputedStyle(currentDom.parentNode).left.replace("px", "").parseInt(), window.getComputedStyle(currentDom.parentNode).marginLeft.replace("px", "").parseInt());
                    let transX = (clientX * glassList[index].scale - dom.parentElement.style.width.replace("px", "").parseInt() / 2) * -1;
                    let transY = (clientY * glassList[index].scale - dom.parentElement.style.height.replace("px", "").parseInt() / 2) * -1;
                    glassList[index].transX = transX;
                    glassList[index].transY = transY;

                    // 3. 偏移量*比例，获取放大镜应该显示的区域
                    dom.style.transform = `translate(${transX}px, ${transY}px)`;
                });
            },
            // 鼠标滑出
            mouseLeave(dom: any) {
                dom.parentElement.style.display = "none";
            },
            // 鼠标缩放
            scroll(e: any, index: any) {
                if (e.deltaY === 100) {
                    glassList[index].scale -= 0.1;
                } else {
                    glassList[index].scale += 0.1;
                }
                glassList[index].glass.style.width = window.getComputedStyle(glassList[index].img).width.replace("px", "").parseInt() * glassList[index].scale + "px";
                glassList[index].glass.style.height = window.getComputedStyle(glassList[index].img).height.replace("px", "").parseInt() * glassList[index].scale + "px";
            }
        };

        const methods = {
            beforeUpload(file: any) {
                if (file.type.indexOf("image") === -1) {
                    ElMessage.error("请上传正确的文件格式");
                }else{
                    data.uploadFile = URL.createObjectURL(file);
                }
                return false;
            }
        };

        onMounted(() => {
            privateMethods.initPage();
        });

        return {
            ...methods,
            ...toRefs(data)
        };
    }
})
</script>

<style scoped lang="scss">
.body {
    position: relative;
    margin-top: 10px;
}

.imgs {
    width: 300px;
    height: 350px;
}

.glass {
    position: absolute;
    overflow: hidden;
    top: 0px;
    display: none;
    background-color: rgb(248, 244, 244, 0.8);
}
</style>
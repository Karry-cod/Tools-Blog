<template>
    <div class="child_boxSide">
        <span class="title">物体碰撞</span>
        <!-- 源码路径 -->
        <div class="aBox">
            源码地址：
            <a href="https://blobtools.oss-cn-shanghai.aliyuncs.com/tools/%E7%89%A9%E4%BD%93%E7%A2%B0%E6%92%9E.rar">源码地址</a>
        </div>
        <div class="outside">
            <div class="left"></div>
            <div class="right"></div>
        </div>
    </div>
</template>

<script>
import { defineComponent, onMounted } from "vue"

export default defineComponent({
    setup() {

        let left_x = 0;
        let right_x = 0;
        let leftDiv;
        let rightDiv;
        let timers;

        const privateMethods = {
            initPage() {
                timers = setInterval(() => {
                    left_x += 10;
                    right_x += 10;
                    leftDiv = document.getElementsByClassName("left")[0];
                    rightDiv = document.getElementsByClassName("right")[0];
                    leftDiv.style.left = left_x + "px";
                    rightDiv.style.right = right_x + "px";
                    let mark = privateMethods.testColl(privateMethods.getElementLeft(leftDiv), privateMethods.getElementTop(leftDiv), leftDiv.clientWidth, leftDiv.clientHeight, privateMethods.getElementLeft(rightDiv), privateMethods.getElementTop(rightDiv), rightDiv.clientWidth, rightDiv.clientHeight);
                    if (mark) {
                        clearInterval(timers);
                        alert("碰撞！");
                    }
                }, 100);
            },
            //参数 鱼的x y width height 炮弹 x y width height
            testColl(x1, y1, yw, yh, x2, y2, bw, bh) {
                var yx = x1;
                var yr = x1 + yw;
                var yy = y1;
                var yb = y1 + yh;

                var bx = x2;
                var br = x2 + bw;
                var by = y2;
                var bb = y2 + bh;
                console.log(x1, y1, yw, yh, x2, y2, bw, bh);
                //如果鱼的右边小于炮弹的x坐标or鱼的x坐标大于炮弹的右边or鱼的底部小于炮弹的y坐标or鱼的y坐标大于炮弹的底部
                if (yr < bx || yx > br || yb < by || yy > bb) {
                    return false;
                } else {
                    return true;
                }
            },
            //获取element的x坐标
            getElementLeft(element) {
                var actualLeft = element.offsetLeft;
                var current = element.offsetParent;
                while (current !== null) {
                    actualLeft += current.offsetLeft;
                    current = current.offsetParent;
                }
                return actualLeft;
            },
            //获取element的y坐标
            getElementTop(element) {
                var actualTop = element.offsetTop;
                var current = element.offsetParent;
                while (current !== null) {
                    actualTop += current.offsetTop;
                    current = current.offsetParent;
                }
                return actualTop;
            }
        };

        onMounted(() => {
            privateMethods.initPage();
        });
    }
})
</script>

<style lang="scss" scoped>
.outside {
    margin-top: 20px;
    position: relative;
    height: 400px;
}

.left {
    position: absolute;
    top: 0px;
    left: 0px;
    width: 200px;
    height: 200px;
    background-color: red;
}

.right {
    position: absolute;
    top: 200px;
    right: 0px;
    width: 200px;
    height: 200px;
    background-color: black;
}
</style>
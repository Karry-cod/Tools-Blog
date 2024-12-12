<template>
    <VueUnity :unity="unityContext" width="800" height="600"></VueUnity>
    <div style="width: 50%; margin-left: auto; margin-right: auto;">
        <button @click="onShowCube" class="defaultButton">{{ visible ? '隐藏' : '显示' }}</button>
        <button @click="onSetColor" class="redButton"></button>
        <button @click="onSetColor" class="blueButton"></button>
        <button @click="onSetColor" class="yellowButton"></button>
    </div>
</template>

<script setup>
import UnityWebgl from 'unity-webgl';
import VueUnity from 'unity-webgl/vue';
import { ref } from 'vue';

const unityContext = new UnityWebgl({
    loaderUrl: '/Unity/Publish/Publish.loader.js',
    dataUrl: '/Unity/Publish/Publish.data.gz',
    frameworkUrl: '/Unity/Publish/Publish.framework.js.gz',
    codeUrl: '/Unity/Publish/Publish.wasm.gz',
})

const visible = ref(true)

unityContext.on('mounted', () => console.log('Unity加载完成...'))
    .on('showDialog', (tip) => alert(tip))//监听unity调用的方法


//向Unity发送信息
function postUnityMessage(methodName, arg) {
    unityContext.send('MessageManager', methodName, arg)
}
//调用unity的方法
//设置cube显隐
function onShowCube() {
    visible.value = !visible.value;
    postUnityMessage('Vue_SetVisible', visible.value ? "1" : "0")
}

//设置cube颜色
function onSetColor(event) {
    const button = event.target;
    const style = window.getComputedStyle(button);
    const htmlcolor = rgbtohex(style.backgroundColor.toString());
    postUnityMessage('Vue_SetColor', htmlcolor);
}

function rgbtohex(rgb) {
    // rgby颜色值的正则
    var reg = /^(rgb|RGB)/;
    // 判断是否为rgb格式 
    if (reg.test(rgb)) {
        // 将rgb的三个数值切割成数组 rgb(255,0,0) ——> ["255","0","0"]
        var colorArr = rgb.replace(/(?:rgb|RGB|\(|\))*/g, "").split(',');
        var hex = "#" + ((1 << 24) + (parseInt(colorArr[0]) << 16) + (parseInt(colorArr[1]) << 8) + parseInt(colorArr[2])).toString(16).slice(1);
        return hex;
    } else {
        return rgb
    }
}

</script>

<style>
.defaultButton {
    width: 50px;
    height: 50px;
}

.redButton {
    background-color: red;
    width: 50px;
    height: 50px;
}

.yellowButton {
    background-color: yellow;
    width: 50px;
    height: 50px;
}

.blueButton {
    background-color: blue;
    width: 50px;
    height: 50px;
}
</style>

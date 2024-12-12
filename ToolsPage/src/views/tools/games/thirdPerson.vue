<template>
    <VueUnity :unity="unityContext" width="100%" height="800"></VueUnity>
</template>

<script>
import { defineComponent } from "vue";
import UnityWebgl from 'unity-webgl';
import VueUnity from 'unity-webgl/vue';

export default defineComponent({
    components: {
        VueUnity
    },
    setup() {
        const unityContext = new UnityWebgl({
            loaderUrl: '/Unity/ThirdPerson/Unity-ThirdPerson.loader.js',
            dataUrl: '/Unity/ThirdPerson/Unity-ThirdPerson.data.gz',
            frameworkUrl: '/Unity/ThirdPerson/Unity-ThirdPerson.framework.js.gz',
            codeUrl: '/Unity/ThirdPerson/Unity-ThirdPerson.wasm.gz',
        })

        unityContext.on('mounted', () => console.log('Unity加载完成...'))

        return {
            unityContext
        };
    }
})

</script>

<style></style>

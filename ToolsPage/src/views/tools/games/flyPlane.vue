<template>
    <div class="body">
        <div class="map_body">
            <img src="@/assets/imgs/map.png" class="map" alt="">
            <div class="planeBox">
                <!-- 起飞点位 -->
                <div class="start_green"></div>
                <div class="start_yellow"></div>
                <div class="start_blue"></div>
                <div class="start_red"></div>
                <!-- /// -->
                <!-- 绿色待机位置 -->
                <div class="wait_green_1 wait_green"></div>
                <div class="wait_green_2 wait_green"></div>
                <div class="wait_green_3 wait_green"></div>
                <div class="wait_green_4 wait_green"></div>
                <!-- /// -->
                <!-- 黄色待机位置 -->
                <div class="wait_yellow_1 wait_yellow"></div>
                <div class="wait_yellow_2 wait_yellow"></div>
                <div class="wait_yellow_3 wait_yellow"></div>
                <div class="wait_yellow_4 wait_yellow"></div>
                <!-- /// -->
                <!-- 蓝色待机位置 -->
                <div class="wait_blue_1 wait_blue"></div>
                <div class="wait_blue_2 wait_blue"></div>
                <div class="wait_blue_3 wait_blue"></div>
                <div class="wait_blue_4 wait_blue"></div>
                <!-- /// -->
                <!-- 红色待机位置 -->
                <div class="wait_red_1 wait_red"></div>
                <div class="wait_red_2 wait_red"></div>
                <div class="wait_red_3 wait_red"></div>
                <div class="wait_red_4 wait_red"></div>
                <!-- /// -->

                <!-- 所有飞行路径点位 -->
                <template v-for="item in 76" :key="item">
                    <div class="plane_dont" :id="'dont_' + item"></div>
                </template>

                <!-- 飞机 棋子 -->
                <div class="plane plane_green" id="plane_green_1"></div>
                <div class="plane plane_green" id="plane_green_2"></div>
                <div class="plane plane_green" id="plane_green_3"></div>
                <div class="plane plane_green" id="plane_green_4"></div>

                <div class="plane plane_yellow" id="plane_yellow_1"></div>
                <div class="plane plane_yellow" id="plane_yellow_2"></div>
                <div class="plane plane_yellow" id="plane_yellow_3"></div>
                <div class="plane plane_yellow" id="plane_yellow_4"></div>

                <div class="plane plane_blue" id="plane_blue_1"></div>
                <div class="plane plane_blue" id="plane_blue_2"></div>
                <div class="plane plane_blue" id="plane_blue_3"></div>
                <div class="plane plane_blue" id="plane_blue_4"></div>

                <div class="plane plane_red" id="plane_red_1"></div>
                <div class="plane plane_red" id="plane_red_2"></div>
                <div class="plane plane_red" id="plane_red_3"></div>
                <div class="plane plane_red" id="plane_red_4"></div>
            </div>
        </div>
        <input type="text" v-model="num">
    </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs, onMounted, watchEffect } from "vue"

export default defineComponent({
    setup() {
        let data = reactive({
            dont_list: <any>[],
            planeGreen_list: <any>[],
            planeYellow_list: <any>[],
            planeBlue_list: <any>[],
            planeRed_list: <any>[],
            num: <number>0
        });

        const methods = {
        };

        watchEffect(() => {

        });

        const events = {
            // 初始化飞机路径点位
            initDont() {
                console.log(1 / 3);
                for (let i = 1; i <= 52; i++) {
                    data.dont_list.push({
                        id: "dont_" + i,
                        green: i,
                        yellow: i + 14,
                        blue: i + 28,
                        red: i + 42,
                        color: events.checkColor(i)
                    });
                }

                for (let i = 1; i <= 4; i++) {
                    for (let step = 1; step <= 6; step++) {
                        data.dont_list.push({
                            id: "dont_" + i * step,
                            green: 52 + step,
                            yellow: i == 2 ? 52 + 6 + step : 0,
                            blue: i == 3 ? 52 + 12 + step : 0,
                            red: i == 4 ? 52 + 18 + step : 0,
                            color: ""
                        })
                    }
                }
            },
            // 判断格子的颜色
            checkColor(index: number): string {
                if ((index / 4).toFixed(2) === "0.25") {
                    return "blue";
                }
                if ((index / 3).toFixed(2) === "0.33") {
                    return "yellow";
                }
                if ((index / 2).toFixed(2) === "0.50") {
                    return "green";
                }
                if ((index / 1).toFixed(2) === "0.00") {
                    return "red";
                }
                return "";
            },
            // 初始化飞机待机位置
            initPlane() {
                let waitGreens = document.querySelectorAll(".wait_green");
                let waitYellows = document.querySelectorAll(".wait_yellow");
                let waitBlues = document.querySelectorAll(".wait_blue");
                let waitReds = document.querySelectorAll(".wait_red");

                let planeGreens = document.querySelectorAll(".plane_green");
                let planeYellows = document.querySelectorAll(".plane_yellow");
                let planeBlues = document.querySelectorAll(".plane_blue");
                let planeReds = document.querySelectorAll(".plane_red");

                for (let i = 0; i < 4; i++) {
                    data.planeGreen_list.push({
                        dom: planeGreens[i],
                        dontNum: 0,
                        color: "green"
                    });
                    planeGreens[i].addEventListener("click", () => {
                        events.jump(1, data.num, data.planeGreen_list[i]);
                    });
                    (planeGreens[i] as HTMLElement).style.top = (waitGreens[i] as HTMLElement).offsetTop + "px";
                    (planeGreens[i] as HTMLElement).style.left = (waitGreens[i] as HTMLElement).offsetLeft + "px";
                }

                for (let i = 0; i < 4; i++) {
                    data.planeYellow_list.push({
                        dom: planeYellows[i],
                        dontNum: 13,
                        color: "yellow"
                    });
                    planeYellows[i].addEventListener("click", () => {
                        events.jump(1, data.num, data.planeYellow_list[i]);
                    });
                    (planeYellows[i] as HTMLElement).style.top = (waitYellows[i] as HTMLElement).offsetTop + "px";
                    (planeYellows[i] as HTMLElement).style.left = (waitYellows[i] as HTMLElement).offsetLeft + "px";
                }

                for (let i = 0; i < 4; i++) {
                    data.planeBlue_list.push({
                        dom: planeBlues[i],
                        dontNum: 26,
                        color: "blue"
                    });
                    planeBlues[i].addEventListener("click", () => {
                        events.jump(1, data.num, data.planeBlue_list[i]);
                    });
                    (planeBlues[i] as HTMLElement).style.top = (waitBlues[i] as HTMLElement).offsetTop + "px";
                    (planeBlues[i] as HTMLElement).style.left = (waitBlues[i] as HTMLElement).offsetLeft + "px";
                }

                for (let i = 0; i < 4; i++) {
                    data.planeRed_list.push({
                        dom: planeReds[i],
                        dontNum: 39,
                        color: "red"
                    });
                    planeReds[i].addEventListener("click", () => {
                        events.jump(1, data.num, data.planeRed_list[i]);
                    });
                    (planeReds[i] as HTMLElement).style.top = (waitReds[i] as HTMLElement).offsetTop + "px";
                    (planeReds[i] as HTMLElement).style.left = (waitReds[i] as HTMLElement).offsetLeft + "px";
                }


            },
            // 逐步飞行
            jump(currentIndex: number, totalNum: number, plane: any) {
                let dont = document.querySelector("#dont_" + (parseInt(plane.dontNum) + 1));
                plane.dom.style.top = (dont as HTMLElement).offsetTop + "px";
                plane.dom.style.left = (dont as HTMLElement).offsetLeft + "px";
                currentIndex += 1;
                plane.dontNum += 1;
                if (currentIndex <= totalNum) {
                    setTimeout(() => {
                        events.jump(currentIndex, totalNum, plane);
                    }, 200);
                }
            }
        };

        onMounted(() => {
            events.initDont();
            setTimeout(() => {
                events.initPlane();
            }, 1000);
        })

        return {
            ...toRefs(data)
        };
    }
})
</script>

<style scoped>
@import "@/assets/styles/index.css";

.body {
    width: 100%;
    height: 100%;
}

.map_body {
    position: relative;
}

.map {
    width: 100%;
    margin: 0 auto;
}

.planeBox {
    position: absolute;
    width: 100%;
    height: 100%;
    top: 0px;
    left: 0px;
}

.planeBox>div {
    border-radius: 50%;
    width: 5%;
    height: 5%;
    position: absolute;
}
</style>
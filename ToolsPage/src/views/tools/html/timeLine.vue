<!--
 * @Description: 
 * @Version: 1.0
 * @Autor: yyq
 * @Date: 2023-02-13 18:56:19
 * @LastEditors: yyq
 * @LastEditTime: 2023-02-14 22:54:08
-->
<template>
    <div id="app" class="child_boxSide">
      <div><div style="text-align: left;margin: 45px 0 8px; ">无参示例：</div></div>
  
      <TimeLineCanvas></TimeLineCanvas>
      <div style="text-align: left;margin: 45px 0 8px; ">指定参数示例：</div>
      <TimeLineCanvas
        ref="time_line"
        @click="clickCanvas"
        @change="changeDate"
        :mark-time="markTime"
        :time-range="timeRange"
        :isAutoPlay="isAutoPlay"
        :startMeddleTime="startMeddleTime"
      />
      <div style="text-align: left;">
        <el-button type="primary" @click="setTimeRange">设置时间区域</el-button>
        <el-button type="primary" @click="setStartMeddleTime">设置起点时间</el-button>
        <el-button type="primary" @click="setMarkTime">设置区域标签</el-button>
        <el-button type="primary" @click="setIsAutoPlay">开启自动播放</el-button>
        <el-button type="primary" @click="stop">暂停播放</el-button>
        <el-button type="primary" @click="play">开启播放</el-button>
      </div>
      <div style="color: #03A9F4; margin-top: 5px;text-align: center;">{{ msg }}</div>
    </div>
  </template>
  
  <script>
  import TimeLineCanvas from "@/components/tools/timeline-canvas.vue";
  export default {
    name: "App",
    components: {
      TimeLineCanvas,
    },
    data() {
      return {
        msg: "",
        isAutoPlay: false,
        boxwidth: 0,
        width: 500,
        startMeddleTime: "",
        timeRange: [],
        markTime: [],
      };
    },
    methods: {
      //设置起点时间
      setStartMeddleTime() {
        this.startMeddleTime = "2023-02-10 09:01:00";
      },
      //设置时间区域
      setTimeRange() {
        this.timeRange = "2023-02-10";
        //或
        //this.timeRange=["2023-02-10 00:00:00","2023-02-10 23:59:59"];
        //起点时间根据timeRange计算（timeRange的中间值）
        this.startMeddleTime = null;
      },
      //设置区域标签
      setMarkTime() {
        this.markTime = [
          {
            beginTime: "2023-02-10 06:01:00",
            endTime: "2023-02-10 12:02:00",
            bgColor: "#FFCC99",
            text: "活动",
          },
          {
            beginTime: "2023-02-10 15:01:00",
            endTime: "2023-02-10 16:02:00",
            bgColor: "#FF6666",
            text: "故障",
          },
        ];
      },
      //开启自动播放
      setIsAutoPlay() {
        this.isAutoPlay = true;
      },
      //开启播放
      play() {
        this.$refs.time_line.play("2024-02-06 13:01:00");
      },
      //暂停播放
      stop() {
        this.$refs.time_line.stop();
      },
      clickCanvas(date) {
        console.log(date);
      },
      changeDate(date, status) {
        console.log("changeDate:" + date);
        this.msg = "选择时间：" + date + "  播放 状 态：" + status;
      },
    },
    beforeMount() {
      let date = "2024-02-06"; //moment(new Date()).format("YYYY-MM-DD");
      this.timeRange = date; // [date + " 00:00:00", date + "  23:59:59"];//"2023-2-10"//;
      this.startMeddleTime = date + " 00:00:00";
      this.markTime = [
        {
          beginTime: date + " 01:01:00",
          endTime: date + " 02:02:00",
          bgColor: "#CC3333",
          text: "困人",
        },
        {
          beginTime: date + " 08:01:00",
          endTime: date + " 10:02:00",
          bgColor: "#FF9966",
          text: "非法闯入",
        },
        {
          beginTime: date + " 15:01:00",
          endTime: date + " 16:02:00",
          bgColor: "#FF9900",
          text: "故障",
        },
      ];
    },
    mounted() {
      navigator.clipboard.readText().then(text => {
         console.log(text, "剪切板");
       })
    },
  };
  </script>
  
  <style scoped>
  </style>
  
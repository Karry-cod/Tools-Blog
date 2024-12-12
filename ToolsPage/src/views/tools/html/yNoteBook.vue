<template>
  <div class="child_boxSide">
    <div class="body">
      <div class="ybook">
        <div class="ybook_left" align="center">
          <div class="icon1">
            <svg aria-hidden="true" style="width:20px">
              <use xlink:href="#icon-bi"></use>
            </svg>
          </div>
          <!-- <div class="icon2">
            <svg aria-hidden="true" style="width:20px;">
              <use xlink:href="#icon-jiajianzujianjiahao-copy"></use>
            </svg>
          </div> -->
          <div class="icon1">
            <svg aria-hidden="true" style="width:34px">
              <use xlink:href="#icon-shoucang"></use>
            </svg>
          </div>
          <!-- <div class="icon1">
            <el-icon>
              <Delete />
            </el-icon>
          </div> -->
        </div>
        <div class="ybook_middle" align="center">
          <el-input v-model="booksearch" size="small" style="margin-top: 6px;width: 97%;" placeholder="输入笔记关键字搜索"
            class="input-with-select">
            <template #append>
              <el-button :icon="Search" />
            </template>
          </el-input>
          <div class="books" v-for="item in notebooks" :key="item.noteid">
            <div class="bookicon">
              <svg aria-hidden="true" style="width:15px">
                <use xlink:href="#icon-tushu"></use>
              </svg>
            </div>
            <div class="bookname">
              <p style="color: #101010;font-size:14px;margin-top: 6px;">{{ item.note1 }}</p>
              <p style="color: #686868;font-size: 13px;line-height: 22px;margin-top: 2px;">{{ item.note2 }}</p>
              <p style="color: #adadad;font-size: 10px;margin-top: 8px;">{{ item.note3 }}</p>
            </div>
          </div>
        </div>
        <div class="ybook_right"></div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent, onMounted, reactive, ref, toRefs, watchEffect } from "vue"
import { Plus, Search, SetUp } from "@element-plus/icons-vue"
export default defineComponent({
  components: {
    Plus, Search
  },
  setup() {
    let data = reactive({
      booksearch: "",
      notebooks: [{
        noteid: 1,
        note1: "如何早日实现暴富？",
        note2: "上课的荆得视剧佛IE女吃辣的会计法老叟诶是上课了豆浆粉你们......",
        note3: "2023-05-08 15:50:00"
      }, {
        noteid: 2,
        note1: "如何早日实现暴富？",
        note2: "上课的荆得视剧佛IE女吃辣的会计法老叟诶是上课了豆浆粉你们......",
        note3: "05-08 15:50:00"
      }],
      isbianse: false,

    })
    const methods = {

    }

    const events = {
      // 将聊天框自适应页面
      initCommentBoxSize() {
        let bodyHeight = window.getComputedStyle(document.getElementsByClassName("boxSide")[0] as HTMLElement).height.replace("px", "").parseInt();
        console.log(bodyHeight, "??");
        (document.getElementsByClassName("body")[0] as HTMLElement).style.height = bodyHeight - 50 + "px";
        // (document.getElementsByClassName("commBox")[0] as HTMLElement).style.height = bodyHeight - 90 + "px"; // 整个聊天框高度
        // data.commentHeight = bodyHeight - 90 - 60 + "px"; // 左侧联系人高度
        // let inputHeight = window.getComputedStyle(document.getElementsByClassName("c_input")[0] as HTMLElement).height.replace("px", "").parseInt(); // 输入框高度
        // data.commInfoHeight = bodyHeight - 90 - 60 - inputHeight + "px";
        // window.addEventListener("resize", () => {
        //   events.initCommentBoxSize();
        // });
      },
    };

    onMounted(() => {
      events.initCommentBoxSize();
    });

    return {
      ...toRefs(data),
      ...methods
    };
  }
})
</script>
<style lang="scss">
.body {
  .ybook {
    border: var(--box-border);
    display: flex;
    height: 100%;
    box-shadow: 0 1px 5px 0 rgba(81, 81, 81, 0.3);

    .ybook_left {
      min-width: 6%;
      height: 100%;
      background-color: #f5f5ff;
      color: #BBBBBB;

      .icon1 {
        width: 100%;
        height: 60px;
        margin-top: 5px;
        align-content: center;
        font-size: 19px;
      }

      .icon2 {
        width: 60%;
        height: 40px;
        margin-top: 5px;
        background-color: #c3c4fb;
        align-content: center;
      }
    }

    .ybook_middle {
      width: 20%;
      height: 100%;
      border-right: var(--box-border);

      .books {
        width: 97%;
        height: 100px;
        border-bottom: var(--box-border);
        display: flex;

        &:hover{
          background-color: #F5F5FF;
        }

        .bookicon {
          height: 100%;
          margin-top: -58px;
        }

        .bookname {
          width: 100%;
          height: 100%;
          text-align: left;
          margin-left: 5px;
        }

        .bookname-changed {
          width: 100%;
          height: 100%;
          text-align: left;
          margin-left: 5px;
          background-color: #F5F5FF;
        }
      }
    }

    .ybook_right {
      width: 74%;
      height: 100%;
    }
  }
}
</style>
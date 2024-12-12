<template>
    <div class="container" :class="pageBackClass">
        <!-- 顶部功能栏 -->
        <div class="headerBox" id="header" :class="toTop">
            <!-- 左侧切换页面模式 -->
            <div class="left-header">
                <el-switch v-model="pageMode" class="mb-2" @change="changePageMode()" active-text="工具"
                    inactive-text="博客" />
            </div>
            <!-- 右侧用户信息操作 -->
            <div class="right-header">
                <!-- 搜索框 -->
                <SearchInput class="searchInputBox" />
                <!-- 页面状态 -->
                <el-switch v-model="pageType" class="mb-2" @change="changePageType()"
                    style="--el-switch-on-color: black;margin-right: 10px;" :active-action-icon="Moon"
                    :inactive-action-icon="Sunny" />
                <img src="https://i-1.sosoyx.com/2023/0216/e91b55b.png" @click="showAIBox()" class="AI_Icon pointer"
                    alt="">
                <!-- 头像 -->
                <el-dropdown trigger="click">
                    <span class="el-dropdown-link">
                        <img src="https://img1.baidu.com/it/u=165764012,751844309&fm=253&fmt=auto&app=138&f=JPEG?w=500&h=500"
                            class="headImg" alt="">
                    </span>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <el-dropdown-item :icon="IconGitee">
                                <a target="_blank" href="https://gitee.com/Xu_zhuojiu">Gitee</a>
                            </el-dropdown-item>
                            <el-dropdown-item :icon="IconGithub">
                                <a target="_blank" href="https://github.com/Karry-cod">Github</a>
                            </el-dropdown-item>
                            <el-dropdown-item divided :icon="Check">修改信息</el-dropdown-item>
                            <el-dropdown-item divided :icon="CircleCheck"
                                @click="store.state.isShowLogin = true">退出登录</el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
                <!-- 发布 -->
                <el-popover :width="400" trigger="hover" content="this is content, this is content, this is content">
                    <template #reference>
                        <el-button round type="danger" size="large" :icon="CirclePlus" style="font-size: 1rem"
                            class="marginL">发布</el-button>
                    </template>
                    <div class="bodyBox">
                        <!-- 发布文章 - 代码 - 动态 等 -->
                        <div class="publishTypes_line">
                            <div class="types">
                                <div @click="publishType(1)">
                                    <el-icon size="20" class="typeIcons pointer">
                                        <Document />
                                    </el-icon>
                                </div>
                                写文章
                            </div>
                            <div class="types">
                                <div @click="publishType(2)">
                                    <el-icon size="20" class="typeIcons pointer">
                                        <Folder />
                                    </el-icon>
                                </div>
                                写代码
                            </div>
                            <div class="types">
                                <div @click="publishType(3)">
                                    <el-icon size="20" class="typeIcons pointer">
                                        <Cloudy />
                                    </el-icon>
                                </div>
                                发动态
                            </div>
                            <div class="types">
                                <div @click="publishType(4)">
                                    <el-icon size="20" class="typeIcons pointer">
                                        <ChatRound />
                                    </el-icon>
                                </div>
                                提问题
                            </div>
                            <div class="types">
                                <div @click="publishType(5)">
                                    <el-icon size="20" class="typeIcons pointer">
                                        <UploadFilled />
                                    </el-icon>
                                </div>
                                传资源
                            </div>
                            <div class="types">
                                <div @click="publishType(6)">
                                    <el-icon size="20" class="typeIcons pointer">
                                        <Share />
                                    </el-icon>
                                </div>
                                建项目
                            </div>
                        </div>
                        <!-- 他们都在参与话题 -->
                        <div class="topic_line">
                            <p>
                                他们都在参与话题
                                <span style="float: right; color: gray; font-size: 16px" class="pointer lookMore">围观看看
                                    ></span>
                            </p>
                            <!-- 用户头像 -->
                            <div class="userImgBox">
                                <img src="https://img1.baidu.com/it/u=165764012,751844309&fm=253&fmt=auto&app=138&f=JPEG?w=500&h=500"
                                    v-for="item in 9" class="userImgs pointer" alt="">
                            </div>
                            <!-- 话题列表 -->
                            <div style="padding-top: 7px">
                                <el-carousel height="20px" direction="vertical" :autoplay="true">
                                    <el-carousel-item v-for="item in 4" :key="item">
                                        <h3 text="2xl" justify="center" class="topics pointer">
                                            <div class="grayBox"></div>
                                            {{ item }}
                                        </h3>
                                    </el-carousel-item>
                                </el-carousel>
                            </div>
                        </div>
                        <!-- 新评论 -->
                        <div class="comment_line">
                            <p>新评论
                                <span style="float: right; color: gray; font-size: 16px" class="pointer lookMore">查看全部
                                    ></span>
                            </p>
                            <div style="margin-top: 5px">
                                <p v-for="item in 3" class="comments pointer">
                                    这是第{{ item }}条评论卡四角打孔数量单价卡了大家撒开了大阿萨德撒旦
                                </p>
                            </div>
                        </div>
                    </div>
                </el-popover>
            </div>
        </div>
        <!-- 页面内容展示区 -->
        <div id="centerBody" :class="isShowAIBox ? 'flexBody' : ''">
            <div :id="isShowAIBox ? 'page' : ''">
                <RouterView />
            </div>
            <!-- AI聊天框 -->
            <div :id="isShowAIBox ? 'AIBox' : ''" v-show="isShowAIBox"
                :style="ai_isFullScrren ? { display: isShowAIBox ? 'block' : 'none', width: '98vw', right: '1vw' } : {}">
                <div class="AI_Title" :style="ai_isFullScrren ? { backgroundColor: 'white' } : {}">
                    <img src="https://i-1.sosoyx.com/2023/0216/e91b55b.png" alt="">
                    <span> 微客AI - GPT4</span>
                    <div class="fullScrren pointer" :title="!ai_isFullScrren ? '全屏' : '缩小'"
                        @click="ai_isFullScrren = !ai_isFullScrren">
                        <el-icon v-show="!ai_isFullScrren">
                            <FullScreen />
                        </el-icon>
                        <el-icon v-show="ai_isFullScrren">
                            <Crop />
                        </el-icon>
                    </div>
                </div>
                <div class="AI_Window">
                    <div class="AI_Body">
                        <el-scrollbar height="100%" ref="ref_AIScroll">
                            <div class="AI_Comm" v-for="(item, index) in ai_messages" :key="index"
                                v-show="item.role != 'system'">
                                <div class="assisant" v-if="item.role === 'assistant'">
                                    <Markdown :source="item.content" />
                                </div>
                                <div class="user" v-if="item.role === 'user'">
                                    {{ item.content }}
                                </div>
                            </div>
                        </el-scrollbar>
                    </div>
                    <div class="AI_Footer">
                        <!-- AI 提供操作栏 -->
                        <div>
                            <el-scrollbar width="100%">
                                <div class="AI_Tools">
                                    <div v-for="item in 10" class="tools" :key="item">
                                        {{ item }}
                                    </div>
                                </div>
                            </el-scrollbar>
                        </div>
                        <!-- 聊天框 -->
                        <div class="AI_Input">
                            <textarea id="textarea" v-model="ai_input_content"
                                placeholder="向我问任何问题...(Shfit + Enter换行，Enter发送)" @keydown="AI_Input"
                                @focus="AI_focusInput()" @blur="AI_blurInput()"></textarea>
                            <div class="AI_InputBtn">
                                {{ ai_input_content.length }} / 1000
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- 登录弹窗 -->
        <div id="loginBox_Dialog">
            <el-dialog v-model="store.state.isShowLogin" width="0px" height="0px">
                <div class="loginBox_box">
                    <LoginBox style="top: 50%" />
                </div>
            </el-dialog>
        </div>
        <!-- 我的信息浮窗 -->
        <div id="myInfo_box">
            <MyInfoBox />
        </div>
    </div>
</template>

<script lang="ts">
import { RouterView } from 'vue-router'
import { defineComponent, reactive, toRefs, onMounted, nextTick, ref, watchEffect } from "vue"
import router from '@/router';
import {
    ArrowDown,
    Check,
    CircleCheck,
    CirclePlus,
    CirclePlusFilled,
    Plus,
    Sunny, Moon, Document, Folder, Cloudy, ChatRound, UploadFilled, Share, FullScreen, Crop
} from '@element-plus/icons-vue'
import IconGitee from "@/components/icons/IconGitee.vue"
import IconGithub from "@/components/icons/IconGithub.vue"
import SearchInput from "@/components/tools/SearchInput.vue"
import { ElMessage } from 'element-plus';
import Notification from "@/utils/classes/Notification";
import store from "@/utils/store"
import LoginBox from "@/components/tools/LoginBox.vue"
import MyInfoBox from "@/components/tools/MyInfoBox.vue"
import Cookie from "@/utils/cookie"
import type { IUser } from "@/utils/interfaces/IUser";
import UserService from "@/api/apis/user"
import axios from "axios";
import Markdown from 'vue3-markdown-it';
import 'highlight.js/styles/a11y-dark.css';
import chatAI from "@/configs/chatAI.json"

export default defineComponent({
    components: {
        Markdown, MyInfoBox, LoginBox, ArrowDown, Check, CircleCheck, CirclePlus, CirclePlusFilled, Plus, IconGitee, IconGithub, SearchInput, Sunny, Moon, Document, Folder, Cloudy, ChatRound, UploadFilled, Share, FullScreen, Crop
    },
    setup() {
        let data = reactive({
            pageMode: "博客",
            pageType: false,
            icon: ``,
            pageBackClass: "",
            toTop: "",
            isShowAIBox: false,
            ai_input_content: "",
            ai_messages: chatAI.ALIYUN_TYQW.textAI.messages,
            ai_isFullScrren: false
        });

        const refs = {
            ref_AIScroll: <any>ref(null)
        };

        watchEffect(() => {
            if (data.ai_messages.length) {
                setTimeout(() => {
                    refs.ref_AIScroll.value!.setScrollTop(99999);
                }, 100);
            }
        });

        const methods = {
            // 切换页面 -> 博客 / 工具
            changePageMode() {
                if (!data.pageMode) {
                    router.push("/");
                } else {
                    router.push("/tools");
                }
            },
            // 切换页面状态 -> 明 / 暗
            changePageType() {
                data.pageBackClass = data.pageType ? "back" : "";
            },
            // 发布文章 - 代码 等
            publishType(index: number) {
                switch (index) {
                    case 1:
                        var noti = new Notification("你好", "hello");
                        noti.Error(undefined, "还行吧");
                        break;
                    default:
                        ElMessage.warning("该功能尚未开发，敬请期待");
                        break;
                }
            },
            // 展示 / 隐藏 AIBox
            showAIBox() {
                data.isShowAIBox = !data.isShowAIBox;
                setTimeout(() => {
                    refs.ref_AIScroll.value!.setScrollTop(99999);
                }, 100);
            },
            // AI-Input 聚焦
            AI_focusInput() {
                document.getElementsByClassName("AI_Input")[0].className = "AI_Input active";
            },
            // AI-Input 失焦
            AI_blurInput() {
                document.getElementsByClassName("AI_Input")[0].className = "AI_Input";
            },
            // AI-Input 输入
            AI_Input(event: any) {
                if (event.keyCode == 13 && event.shiftKey) {
                    //ctrl+enter换行
                    // data.ai_input_content = data.ai_input_content + "\n";// 获取textarea数据进行 换行
                } else if (event.keyCode == 13) {
                    event.preventDefault();//禁止回车的默认换行
                    //enter发送
                    data.ai_messages.push({
                        role: "user",
                        content: data.ai_input_content
                    });
                    data.ai_input_content = "";
                    privateMethods.chatGPT4();
                }
            }
        };

        const privateMethods = {
            // 监听页面滚动
            pageScroll() {
            },
            // 判断用户是否登录过，若登录过则无需弹出登录框
            isLogin() {
                UserService.GetLocalUser().then(res => {
                    if (res.code === 1) {
                        store.state.localUser = res.data;
                        store.state.isShowLogin = false;
                        Cookie.set("localUser", JSON.stringify(res.data))
                    } else {
                        Cookie.remove("localUser");
                        store.state.localUser = <IUser>{};
                        store.state.isShowLogin = true;
                        ElMessage.error(res.message);
                    }
                });
            },
            // 获取配置文件内容
            getAmapConfigs() {
                axios.get("/config.json").then(res => {
                    store.state.config = res.data;
                    console.log(store.state.config, "json内容");
                });
            },
            // 与GPT4对话
            async chatGPT4() {
                // 给用户一个等待的展示
                data.ai_messages.push({
                    role: "assistant",
                    content: "容我思考一下哦！"
                });

                const res = await fetch(chatAI.ALIYUN_TYQW.textAI.url, {
                    headers: {
                        "Content-Type": "application/json",
                        "Authorization": "Bearer " + chatAI.ALIYUN_TYQW.api_key
                    },
                    method: "post",
                    body: JSON.stringify({
                        model: "qwen-plus",
                        messages: data.ai_messages,
                        stream: true
                    }),
                });
                const reader = res.body.getReader();
                const decoder = new TextDecoder();
                data.ai_messages[data.ai_messages.length - 1].content = "";

                while (1) {
                    // 读取数据流的第一块数据，done表示数据流是否完成，value表示当前的数
                    const { done, value } = await reader.read();
                    if (done) break;
                    let text = decoder.decode(value);
                    let lines = text.split("\n");
                    for (let i = 0; i < lines.length - 1; i++) {
                        // 处理每一行
                        console.log('Received chunk: ', lines[i].replace("data: ", ""), "=-=-");
                        text = lines[i].replace("data: ", "");
                        try {
                            data.ai_messages[data.ai_messages.length - 1].content += JSON.parse(text).choices[0].delta.content;
                        } catch (ex) { }
                    }
                }

                // axios({
                //     url: chatAI.ALIYUN_TYQW.textAI.url,
                //     method: "post",
                //     headers: {
                //         "Content-Type": "application/json",
                //         "Authorization": "Bearer " + chatAI.ALIYUN_TYQW.api_key
                //     },
                //     data: JSON.stringify({
                //         model: "qwen-plus",
                //         messages: data.ai_messages,
                //         stream: true
                //     }),
                // }).then(res => {
                //     data.ai_messages.push({
                //         role: "assistant",
                //         content: res.data.choices[0].message.content
                //     });
                // });
            }
        };

        onMounted(() => {
            privateMethods.pageScroll();
            privateMethods.isLogin();
            privateMethods.getAmapConfigs();
        })

        const icons = {
            ArrowDown, Check, CircleCheck, CirclePlus, CirclePlusFilled, Plus, IconGitee, IconGithub, Sunny, Moon, FullScreen, Crop
        };

        return {
            ...toRefs(data),
            ...methods,
            ...icons,
            store,
            ...refs
        };
    }
})

</script>

<style lang="scss">
.headerBox {
    z-index: 999;
    height: 70px;
    width: 100vw;
    background-color: white;
    border-bottom: var(--box-border);
    box-shadow: 0 0px 5px 1px rgb(196, 195, 195, 0.8);
    // 垂直居中
    display: flex;
    align-items: center;
    position: fixed;
    top: 0px;

    &>.left-header {
        position: absolute;
        left: 20px;
    }

    &>.right-header {
        position: absolute;
        right: 20px;
        display: flex;
        align-items: center;

        .searchInputBox {
            margin-right: 10px;
        }
        
        .headImg {
            width: 37px;
            height: 37px;
            border-radius: 50%;
            border: 1px solid rgb(218, 215, 215);
            padding: 3px;
            transition: 0.3s;

            &:hover {
                border: 1px solid black;
            }
        }
    }

    .marginL {
        margin-left: 15px;
    }

    .AI_Icon {
        width: 30px;
        height: 30px;
        margin-right: 10px;
        transition: 0.3s;
        opacity: 0.5;

        &:hover {
            opacity: 1;
        }
    }
}

// 发布类型
.bodyBox {
    .publishTypes_line {
        display: flex;
        flex-wrap: nowrap;
        padding-bottom: 13px;
        border-bottom: var(--box-border);

        .types {
            width: 60px;
            text-align: center;
            margin-left: 5px;

            .typeIcons {
                background-color: rgb(211, 208, 208, 0.3);
                padding: 10px;
                border-radius: 50%;
                margin-bottom: 13px;
                transition: 0.3s;

                &:hover {
                    background-color: rgb(211, 208, 208, 0.6);
                }
            }
        }
    }

    .topic_line {
        padding-top: 15px;
        color: black;
        border-bottom: var(--box-border);
        padding-bottom: 20px;

        .lookMore {
            &:hover {
                color: red !important;
            }
        }

        .userImgBox {
            padding-top: 20px;

            .userImgs {
                width: 40px;
                height: 40px;
                border-radius: 50%;
                margin-left: 1px;
                opacity: 0.9;

                &:hover {
                    opacity: 1;
                }
            }
        }

        .topics {
            &:hover {
                color: gray;
            }

            .grayBox {
                background-color: rgb(233, 218, 218);
                width: 10px;
                height: 10px;
                display: inline-block;
                transform: translateY(-1px);
                margin-right: 5px;
            }
        }
    }

    .comment_line {
        padding-top: 15px;

        .lookMore {
            &:hover {
                color: red !important;
            }
        }

        .comments {
            padding-top: 2px;
            color: black;
            font-size: 15px;
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
            width: 100%;

            &:hover {
                color: red;
            }
        }
    }
}

.container {
    // padding-top: 70px;
    width: 100vw;
    height: 100vh;
    overflow: scroll;
    overflow-x: hidden;
}

.container::-webkit-scrollbar {
    width: 0px;
}

.back {
    width: 100vw;
    height: 100vh;
    background-image: url("@/static/imgs/blog-back.gif");

    .boxSide {
        background-color: white;
    }

    .userInfoBox {
        background-color: white;
    }

    .box {
        background-color: transparent;
    }

    .borde {
        .child_boxSide {
            background-color: white;
        }
    }
}

.flexBody {
    display: flex;
}

#centerBody {
    margin-top: 80px;

    #page {
        width: 80%;
    }

    #AIBox {
        width: 20vw;
        height: calc(100vh - 120px);
        margin-left: 20px;
        position: fixed;
        top: 90px;
        right: 20px;
        box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.3);
        animation: AIBox 0.7s;
        transition: 0.3s;
        z-index: 999;

        .AI_Title {
            height: 80px;
            background-color: rgb(235, 235, 235, 0.4);
            padding-left: 20px;
            font-size: 25px;
            position: relative;
            border-bottom: 1px solid rgb(224, 223, 223);

            >img {
                width: 50px;
                height: 50px;
                transform: translateY(15px);
            }

            >.fullScrren {
                position: absolute;
                right: 15px;
                top: 20%;

                :hover {
                    background-color: rgba(230, 230, 230, 0.5);
                }
            }
        }

        .AI_Window {
            height: calc(100% - 80px);
            background-color: rgb(250, 250, 250);

            .AI_Body {
                height: 75%;

                .AI_Comm {
                    width: 100%;
                    float: left;
                    padding-bottom: 10px;
                    padding-top: 10px;

                    >.assisant {
                        float: left;
                        background-color: white;
                        padding: 15px;
                        border-radius: 5px;
                        padding-top: 10px;
                        padding-bottom: 10px;
                        margin-left: 20px;
                        max-width: 80%;
                        box-shadow: 0px 0px 1px 0px gray;
                    }

                    >.user {
                        background-color: rgb(86, 186, 129);
                        padding: 15px;
                        float: right;
                        border-radius: 5px;
                        padding-top: 10px;
                        padding-bottom: 10px;
                        margin-right: 20px;
                        max-width: 80%;
                    }
                }

            }

            .AI_Footer {
                height: 25%;
                padding-top: 10px;
                padding-left: 20px;
                padding-right: 20px;

                .AI_Tools {
                    width: 100%;
                    display: flex;

                    .tools {
                        // display: inline-block;
                        border: 1px solid rgb(202, 207, 245);
                        border-radius: 5px;
                        padding: 10px;
                        padding-top: 5px;
                        padding-bottom: 5px;
                        background: white;
                        margin-right: 20px;
                    }
                }

                .AI_Input {
                    border: 1px solid rgb(163, 162, 162, 0.5);
                    border-radius: 5px;
                    padding: 15px;
                    background-color: white;
                    margin-top: 10px;
                    height: 55%;
                    padding-top: 10px;
                    transition: 0.3s;

                    #textarea {
                        width: 100%;
                        border: none;
                        outline: none;
                        font-size: 16px;
                        resize: none;
                        height: 85%;
                    }

                    .AI_InputBtn {
                        font-size: 12px;
                    }

                    &:hover {
                        border: 1px solid green;
                    }
                }

                .active {
                    border: 1px solid green;
                }
            }
        }
    }

    @keyframes AIBox {
        0% {
            transform: translateX(100%);
        }

        100% {
            transform: translateX(0px);
        }
    }
}

#loginBox_Dialog {
    .loginBox_box {
        position: relative;
        height: 50vh;
    }
}

@media screen and (max-width: 800px) {
    #centerBody {
        #page {
            display: none;
            width: 0%;
        }

        #AIBox {
            width: 90vw;
        }
    }
}
</style>
<template>
    <div class="child_boxSide">
        <p class="title">聊天框</p>
        <div class="body">
            <div class="commBox">
                <div class="left">
                    <!-- 左侧联系栏 -->
                    <div class="l_head">
                        <el-input placeholder="搜索" style="width: 80%"></el-input>
                        <el-button type="success" @click="dialog_addFriend.isShow = true" :icon="Plus" plain
                            style="margin-left: 5%;width: 30px"></el-button>
                    </div>
                    <div class="l_body">
                        <el-scrollbar :height="commentHeight">
                            <div class="comments" v-for="item in commRooms" @click="changeRoom(item)"
                                :key="item.roomId">
                                <!-- 联系人头像 -->
                                <div class="comm_headImg">
                                    <img :src="item.roomAvatar" alt="">
                                </div>
                                <!-- 联系信息 -->
                                <div class="comm_info">
                                    <p>{{ item.roomName }}</p>
                                    <span>这是聊天信息</span>
                                </div>
                            </div>
                        </el-scrollbar>
                    </div>
                </div>
                <!-- 中心聊天信息区 -->
                <div class="center">
                    <div class="c_head">
                        公共聊天室
                    </div>
                    <div class="c_body">
                        <!-- 信息 -->
                        <div class="c_commInfos" :style="{ height: commInfoHeight }">
                            <el-scrollbar :height="commInfoHeight" ref="ref_commScroll">
                                <div class="comms" v-for="item in commInfos" :key="item.commId"
                                    :class="item.user.uId === localUser.uId ? 'right' : ''">
                                    <!-- 头像 -->
                                    <div class="comm_headImg">
                                        <img :src="item.user.uAvatar" alt="">
                                    </div>
                                    <div class="comm_line">
                                        <p>{{ item.user.uName }}</p>
                                        <!-- 消息内容 -->
                                        <div class="comm_info">
                                            <Markdown :source="item.content" />
                                            <!-- {{ item.content }} -->
                                        </div>
                                    </div>
                                </div>
                            </el-scrollbar>
                        </div>
                        <!-- 输入框 -->
                        <div class="c_input">
                            <div class="input_tools">
                                <el-icon>
                                    <Picture></Picture>
                                </el-icon>
                                <el-icon @click="selectFile()">
                                    <Link>
                                    </Link>
                                </el-icon>
                                <input type="file" id="fileInput" value="" v-show="false" @change="changeFile" />
                                <div class="selectedFile pointer" v-if="selectedFile.name">
                                    <div class="filename">
                                        <img src="@/static/imgs/excel.png" alt="">
                                        {{ selectedFile.name }}
                                    </div>
                                    <div class="close_file">
                                        ×
                                    </div>
                                </div>
                            </div>
                            <textarea style="width: 100%;height: 90%" placeholder="请输入你的问题" v-model="inputContent"
                                @keydown.enter.prevent="sendMessage()"></textarea>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- 添加好友 - 弹框 -->
        <div>
            <el-dialog v-model="dialog_addFriend.isShow" title="添加好友" width="60%">
                <div id="dialog_friend_body">
                    <!-- 搜索栏 -->
                    <div>
                        <el-input placeholder="请输入用户名称/账号" v-model="dialog_addFriend.selectUserName">
                            <template #append>
                                <el-button :disabled="dialog_addFriend.selectUserName.isNull()" :icon="Search"
                                    @click="getUsers()" />
                            </template>
                        </el-input>
                    </div>
                    <!-- 搜索内容 -->
                    <p style="line-height: 50px;">搜索到以下用户：</p>
                    <div class="friend_outside">
                        <div class="friend_box" v-for="item in dialog_addFriend.friendList">
                            <div class="friend_list">
                                <div class="friend_avatar">
                                    <img :src="item.uAvatar" alt="">
                                </div>
                                <div class="friend_info">
                                    <p :title="item.uName + '(' + item.uAccount + ')'"
                                        v-html="store.methods.redMainText(item.uName + '(' + item.uAccount + ')', dialog_addFriend.selectUserName)">
                                    </p>
                                    <p>
                                        <img v-if="item.uSex === 1" src="/src/assets/imgs/boy.png" style="width: 10px"
                                            alt="">
                                        <img v-else src="/src/assets/imgs/girl.png" alt="">
                                    </p>
                                    <p>
                                        <el-tag>{{ item.uIpAddress }}</el-tag>
                                    </p>
                                    <div class="toolsBtn">
                                        <img src="/src/assets/imgs/userInfo.png" title="查看信息" alt="">
                                        <img src="/src/assets/imgs/addFriend.png" title="添加好友" alt="">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </el-dialog>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, reactive, ref, toRefs, watchEffect } from "vue"
import { Plus, Search, Picture, Link } from "@element-plus/icons-vue"
import type { ICommunicate, ICommRoom, ICommMessage } from "@/utils/interfaces/ICommunicate"
import store from "@/utils/store"
import type { IUser } from "@/utils/interfaces/IUser"
import WebSocketHander from "@/utils/classes/WebSocketHandle"
import UserService from "@/api/apis/user"
import Notification from "@/utils/classes/Notification"
import { ElMessage } from "element-plus";
import axios from "axios";
import Markdown from 'vue3-markdown-it';
import 'highlight.js/styles/a11y-dark.css';
import * as XLSX from 'xlsx';

export default defineComponent({
    components: {
        Plus, Search, Markdown, Picture, Link
    },
    setup() {
        let data = reactive({
            commentHeight: "",
            commInfoHeight: "",
            inputContent: "",
            localUser: store.state.localUser,
            commInfos: <Array<ICommunicate>>[
                {
                    commId: "1",
                    user: {
                        uId: "ai",
                        uName: "微客AI",
                        uAvatar: "https://i-1.sosoyx.com/2023/0216/e91b55b.png"
                    },
                    content: "你好，我是微客AI，很高兴在此与你相遇！！"
                }
            ],
            commRooms: <Array<ICommRoom>>[{
                roomId: "ai-" + store.state.localUser.uId,
                roomName: "微客AI助手",
                roomNo: "ai-" + store.state.localUser.uId,
                roomAvatar: "https://i-1.sosoyx.com/2023/0216/e91b55b.png"
            }],
            currentRoom: <ICommRoom>{},
            messages: <Array<ICommMessage>>[],
            dialog_addFriend: {
                isShow: false,
                selectUserName: "",
                friendList: <Array<IUser>>[]
            },
            commMsgs: [
                {
                    role: "system", content: [{
                        type: "text",
                        text: "You are a helpful assistant. You will talk like a pirate."
                    }]
                },
                {
                    role: "user", content: [{
                        type: "text",
                        text: "你好"
                    }]
                },
                {
                    role: "assistant", content: [{
                        type: "text",
                        text: "你好，我是微客AI，很高兴在此与你相遇！！"
                    }]
                }
            ],
            selectedFile: {
                name: "",
                data: []
            }
        });

        // WebSocket
        let wsUrl = import.meta.env.DEV ? "ws://localhost:5103/ws" : "ws://47.116.114.17:5000/ws";
        let socket = store.state.localUser.uId ? new WebSocketHander(new WebSocket(wsUrl), store.state.localUser.uName ?? "", "ai-" + store.state.localUser.uId, store.state.localUser.uId) : null;

        const refs = {
            ref_commScroll: <any>ref(null)
        };

        watchEffect(() => {
            if (data.messages.length) {
                let message = data.messages[data.messages.length - 1];
                data.commInfos.push({
                    commId: message.messageId,
                    user: {
                        uId: message.uid,
                        uName: message.nick,
                        uAvatar: message.uAvatar
                    },
                    content: message.msg
                });
                setTimeout(() => {
                    refs.ref_commScroll.value!.setScrollTop(99999);
                }, 100);
            }
        });

        const privateMethods = {
            // 将聊天框自适应页面
            initCommentBoxSize() {
                let bodyHeight = window.getComputedStyle(document.getElementsByClassName("boxSide")[0] as HTMLElement).height.replace("px", "").parseInt();
                (document.getElementsByClassName("commBox")[0] as HTMLElement).style.height = bodyHeight - 90 + "px"; // 整个聊天框高度
                data.commentHeight = bodyHeight - 90 - 60 + "px"; // 左侧联系人高度
                let inputHeight = window.getComputedStyle(document.getElementsByClassName("c_input")[0] as HTMLElement).height.replace("px", "").parseInt(); // 输入框高度
                data.commInfoHeight = bodyHeight - 90 - 60 - inputHeight + "px";
                window.addEventListener("resize", () => {
                    privateMethods.initCommentBoxSize();
                });
            },
            // 连接聊天室
            connectRoom() {
                socket?.open();
                data.messages = socket?.receiveMsg(data.messages);
            },
        };

        const methods = {
            // 发送消息
            sendMessage() {
                data.commMsgs.push({
                    role: "user",
                    content: [{
                        type: "text",
                        text: data.inputContent + "\r\n " + JSON.stringify(data.selectedFile.data.length > 0 ? data.selectedFile.data : "")
                    }]
                });
                data.commInfos.push({
                    commId: "user",
                    user: {
                        uId: data.localUser.uId,
                        uName: data.localUser.uName,
                        uAvatar: data.localUser.uAvatar
                    },
                    content: data.inputContent
                });
                axios({
                    url: "https://karryai.openai.azure.com/openai/deployments/KarryAI-4/chat/completions?api-version=2024-02-15-preview",
                    method: "POST",
                    headers: {
                        "api-key": "34cd79ecda0c4adf9d058a8e8f838cde",
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify({
                        messages: data.commMsgs
                    })
                }).then(res => {
                    data.selectedFile = {
                        name: "",
                        data: []
                    };
                    data.commInfos.push({
                        commId: "ai",
                        user: {
                            uId: "ai",
                            uName: "微客AI",
                            uAvatar: "https://i-1.sosoyx.com/2023/0216/e91b55b.png"
                        },
                        content: res.data.choices[0].message.content
                    });
                    data.commMsgs.push({
                        role: "assistant",
                        content: [{
                            type: "text",
                            text: res.data.choices[0].message.content
                        }]
                    });
                    setTimeout(() => {
                        refs.ref_commScroll.value!.setScrollTop(99999);
                    }, 100);
                });

                // WebSocket
                // socket?.sendMsg({
                //     msg: data.inputContent,
                //     roomNo: data.currentRoom.roomNo,
                //     nick: data.localUser.uName,
                //     uid: data.localUser.uId,
                //     uAvatar: data.localUser.uAvatar ?? ""
                // });
                data.inputContent = "";
                setTimeout(() => {
                    refs.ref_commScroll.value!.setScrollTop(99999);
                }, 100);
            },
            // 更改聊天室
            changeRoom(room: ICommRoom) {
                data.currentRoom = room;
            },
            // 查询用户列表
            getUsers() {
                let params = {
                    selectUserName: data.dialog_addFriend.selectUserName
                };
                if (data.dialog_addFriend.selectUserName.isNull()) {
                    return;
                }
                UserService.GetUsers(params).then(res => {
                    if (res.code === 1) {
                        data.dialog_addFriend.friendList = res.data;
                    } else {
                        ElMessage.warning(res.message);
                    }
                });
            },
            // 选择文件
            selectFile() {
                let fileInput = document.getElementById("fileInput") as HTMLElement;
                fileInput.click();
                // fileInput.dispatchEvent(new Event("change", {
                //     bubbles: true,
                //     cancelable: true,
                // }));
            },
            // 选择文件
            changeFile(event: any) {
                const file = event.target.files[0];
                if (!file) return;
                if (file.name.indexOf(".xlsx") <= 0) {
                    Notification.Error("文件格式错误，暂只接受xlsx文件");
                    return;
                }
                // 初始化样式
                data.selectedFile = {
                    name: "",
                    data: []
                };

                const reader = new FileReader();
                reader.onload = (e: any) => {
                    const excelData = new Uint8Array(e.target.result);
                    const workbook = XLSX.read(excelData, { type: 'array' });
                    const sheetName = workbook.SheetNames[0]; // 读取第一个 sheet
                    const sheet = workbook.Sheets[sheetName];
                    const jsonData: any = XLSX.utils.sheet_to_json(sheet, { header: 2 });

                    data.selectedFile = {
                        name: file.name,
                        data: jsonData
                    };
                    console.log(jsonData, "数据？");
                };
                reader.readAsArrayBuffer(file);
            }
        };

        const icons = {
            Plus, Search, Picture, Link
        };

        onMounted(() => {
            privateMethods.initCommentBoxSize();
            data.localUser = store.state.localUser;
            data.currentRoom = data.commRooms[0];
            // if (store.state.localUser.uId) {
            //    privateMethods.connectRoom();
            // } else {
            //    Notification.Warning("请先进行登录！");
            //}
        });

        return {
            ...icons,
            ...toRefs(data),
            ...methods,
            ...refs,
            store
        };
    }
})
</script>

<style lang="scss" scoped>
.body {
    .commBox {
        border: var(--box-border);
        box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.3);
        display: flex;
        overflow-y: hidden;

        $border: 1px solid rgb(189, 187, 187, 0.4);

        .left {
            width: 25%;
            height: 100%;
            padding-top: 10px;
            border-right: $border;

            .l_head {
                padding: 10px;
                height: 32px;
            }

            .l_body {
                border-top: $border;

                .comments {
                    padding: 10px;
                    display: flex;
                    transition: 0.1s;

                    &:hover {
                        background-color: rgb(213, 211, 210, 0.4);
                    }

                    .comm_headImg {
                        img {
                            width: 50px;
                            height: 50px;
                        }
                    }

                    .comm_info {
                        padding-left: 10px;

                        p {
                            font-size: 18px;
                            padding-bottom: 5px;
                        }

                        span {
                            color: rgb(172, 168, 168);
                            font-size: 14px;
                        }
                    }
                }
            }
        }

        .center {
            width: 75%;
            height: 100%;
            padding-top: 20px;

            .c_head {
                padding-bottom: 15px;
                font-size: 20px;
                padding-left: 20px;
            }

            .c_body {
                border-top: $border;
                height: 100%;

                .c_commInfos {
                    padding-left: 15px;

                    .comms {
                        display: flex;
                        margin-top: 10px;
                        margin-bottom: 10px;
                        margin-right: 10px;

                        .comm_headImg {
                            padding-left: 10px;
                            padding-right: 10px;

                            img {
                                border-radius: 50%;
                                height: 40px;
                                width: 40px;
                            }
                        }

                        .comm_line {
                            max-width: 70%;

                            p {
                                font-size: 14px;
                                color: gray;
                                padding-bottom: 5px;
                            }

                            .comm_info {
                                background-color: rgb(216, 216, 216, 0.5);
                                padding: 10px;
                                position: relative;

                                &:before {
                                    content: "";
                                    position: absolute;
                                    width: 0;
                                    height: 0;
                                    border-top: 5px solid transparent;
                                    border-bottom: 5px solid transparent;
                                    border-right: 8px solid rgb(216, 216, 216, 0.5);
                                    /* 三角形的颜色 */
                                    top: 10px;
                                    left: -7px;
                                }
                            }
                        }
                    }

                    .right {
                        flex-direction: row-reverse;

                        .comm_line {

                            p {
                                text-align: right;
                            }

                            .comm_info {
                                background-color: rgb(149, 236, 105);
                                float: right;

                                &:before {
                                    display: none;
                                }

                                &:after {
                                    content: "";
                                    position: absolute;
                                    width: 0;
                                    height: 0;
                                    border-top: 5px solid transparent;
                                    border-bottom: 5px solid transparent;
                                    border-left: 8px solid rgb(149, 236, 105);
                                    /* 三角形的颜色 */
                                    top: 10px;
                                    left: 100%;
                                }
                            }
                        }
                    }
                }

                .c_input {
                    height: 30%;
                    max-height: 200px;
                    padding: 5px;
                    border-top: $border;

                    textarea {
                        outline: none;
                        border: none;
                        resize: none;
                        font-size: 15px;
                    }

                    .input_tools {
                        font-size: 20px;
                        margin-bottom: 10px;

                        .el-icon {
                            color: gray;
                            margin-right: 10px;
                            transition: 0.2s;
                            cursor: var(--cursor-pointer);

                            &:hover {
                                color: black;
                            }
                        }

                        .selectedFile {
                            display: inline-block;
                            background-color: white;
                            animation: selectedFile 0.4s;
                            border: 1px solid rgb(202, 200, 200);
                            border-radius: 3px;
                            text-align: justify;

                            .filename {
                                display: inline-block;
                                padding-bottom: 4px;
                                line-height: 20px;
                                position: relative;
                                padding-left: 25px;
                                padding-right: 5px;
                                font-size: 12px;
                                padding-top: 6px;

                                img {
                                    position: absolute;
                                    width: 15px;
                                    height: 17px;
                                    left: 5px;
                                    top: 7px;
                                }

                                &:hover{
                                    background-color: rgb(211, 211, 211);
                                }
                            }

                            .close_file{
                                display: inline-block;
                                font-size: 20px;
                                padding: 5px;
                                padding-top: 0px;
                                padding-bottom: 4px;
                                border-left: 1px solid rgb(202, 200, 200);

                                &:hover{
                                    background-color: rgb(211, 211, 211);
                                }
                            }
                        }

                        @keyframes selectedFile {
                            0% {
                                opacity: 0;
                                transform: translateY(-20px);
                            }

                            100% {
                                opacity: 1;
                                transform: translateY(0px);
                            }
                        }
                    }
                }
            }
        }
    }

}

#dialog_friend_body {
    background-color: white;
    padding-top: 0px;

    .friend_outside {

        .friend_box {
            display: inline-block;
            width: 31%;
            margin-top: 10px;
            margin-left: 10px;

            .friend_list {
                height: 70px;
                display: flex;
                padding: 10px;
                border: 1px solid rgb(206, 201, 201, 0.4);
                border-radius: 3px;

                .friend_avatar {
                    width: 30%;

                    >img {
                        width: 100%;
                    }
                }

                .friend_info {
                    width: 70%;
                    padding-left: 15px;
                    position: relative;

                    p {
                        overflow: hidden;
                        text-overflow: ellipsis;
                        white-space: nowrap;
                        line-height: 20px;
                        font-size: 12px;
                    }

                    .toolsBtn {
                        position: absolute;
                        right: 0px;
                        bottom: -10px;
                        opacity: 0;
                        transition: 0.2s;

                        img {
                            width: 15px;
                            margin-left: 10px;
                            cursor: var(--cursor-pointer);
                            opacity: 0.6;
                            transition: 0.2s;

                            &:hover {
                                opacity: 1;
                            }
                        }
                    }
                }

                &:hover {
                    border: 1px solid lightblue;

                    .toolsBtn {
                        opacity: 1;
                    }
                }
            }
        }
    }
}
</style>
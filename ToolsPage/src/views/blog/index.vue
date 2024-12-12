<template>
    <div>
        <div class="boxSide box">
            <div class="header "></div>
        </div>
        <div class="boxSide userInfoBox">
            <!-- 头像栏 -->
            <div class="headImgBox">
                <img :src="store.state.localUser?.uAvatar" class="headImg" alt="">
            </div>
            <!-- 个人信息 -->
            <div class="userNameBox">
                <!-- 名称 - 操作 -->
                <div class="nickNameBox">
                    <span class="nickName">{{ store.state.localUser?.uName ?? "游客" }}</span>
                    <!-- 操作 -->
                    <div class="toolsBox">
                        <el-button round plain :icon="Edit">编辑资料</el-button>
                        <el-button round plain :icon="Setting">设置</el-button>
                        <el-button round plain :icon="Document">管理文章</el-button>
                    </div>
                </div>
                <!-- 访客量 -->
                <div class="userProcBox">
                    <div class="userProcs">
                        <span>{{ store.methods.toThousands(2999) }}</span>
                        总访问量
                    </div>
                    <div class="userProcs">
                        <span>{{ store.methods.toThousands(2999) }}</span> 原创
                    </div>
                    <div class="userProcs">
                        <span>{{ store.methods.toThousands(2999) }}</span> 排名
                    </div>
                    <div class="userProcs">
                        <span>{{ store.methods.toThousands(2999) }}</span> 粉丝
                    </div>
                    <div class="userProcs">
                        <span>{{ store.methods.toThousands(2999) }}</span> 铁粉
                    </div>
                    <div class="userProcs">
                        <span>{{ store.methods.toThousands(2999) }}</span> 学习成就
                    </div>
                </div>
                <!-- 个人简历 -->
                <div class="userDetail">
                    个人简历： {{ store.state.localUser?.uIntroduce }}
                </div>
                <!-- IP属地 -->
                <div class="ipPlace">
                    IP属地： {{ store.state.localUser?.uIpAddress }}
                </div>
                <!-- 查看详细资料 -->
                <div class="showDetails pointer" @click="isShowDetails = true" v-show="!isShowDetails">
                    查看详细资料
                    <el-icon class="showIcons">
                        <ArrowDownBold />
                    </el-icon>
                </div>
                <!-- 收起详细资料 -->
                <div class="hideDetails" v-show="isShowDetails">
                    <div class="line">
                        加入Blobs时间： 2021-04-02
                    </div>
                    <div class="line">
                        博客简介： Karry的博客
                    </div>
                    <div class="line pointer" @click="isShowDetails = false">
                        收起详细资料
                        <el-icon class="showIcons">
                            <ArrowUpBold />
                        </el-icon>
                    </div>
                </div>
            </div>
        </div>
        <!-- 文章盒 -->
        <div class="borde">
            <div class="l_borde">
                <!-- 原力等级 -->
                <div class="yldj child_boxSide">
                    <div class="head">
                        <span style="font-weight: 600;font-size: 18px;">原力等级</span>
                        <span style="float: right" class="pointer">成就 ></span>
                    </div>
                    <div class="body">
                        <div class="body_child">
                            当前等级
                            <p>1</p>
                        </div>
                        <div class="body_child">
                            当前总分
                            <p>68</p>
                        </div>
                        <div class="body_child">
                            当月
                            <p>15</p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="r_borde child_boxSide">
                <el-tabs v-model="activeName">
                    <el-tab-pane label="最近" name="最近">
                        <div v-for="item in 10" class="tabBoxs">
                            <div class="tabImg">
                                <img src="https://img1.baidu.com/it/u=1546227440,2897989905&fm=253&app=138&size=w931&n=0&f=JPEG&fmt=auto?sec=1700845200&t=470f6a9f369ce5b8e88efc849b7ae894"
                                    alt="">
                            </div>
                            <div class="tabInfo">
                                <p class="title">
                                    微信小程序 - 富文本编辑器
                                </p>
                            </div>
                        </div>
                    </el-tab-pane>
                    <el-tab-pane label="文章" name="文章">Config</el-tab-pane>
                    <el-tab-pane label="资源" name="资源">Role</el-tab-pane>
                    <el-tab-pane label="问答" name="问答">Task</el-tab-pane>
                    <el-tab-pane label="帖子" name="帖子">Task</el-tab-pane>
                    <el-tab-pane label="视频" name="视频">Task</el-tab-pane>
                    <el-tab-pane label="更多" name="更多">Task</el-tab-pane>
                </el-tabs>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs, onMounted } from "vue"
import {
    Edit, Setting, Document, ArrowDownBold, ArrowUpBold
} from '@element-plus/icons-vue'
import store from "@/utils/store"
import axios from "axios";

export default defineComponent({
    components: {
        Edit, Setting, Document, ArrowDownBold, ArrowUpBold
    },
    setup() {
        let data = reactive({
            isShowDetails: false,
            activeName: "最近"
        });

        const icons = {
            Edit, Setting, Document, ArrowDownBold, ArrowUpBold
        };

        return {
            ...icons,
            store,
            ...toRefs(data)
        };
    }
})
</script>

<style scoped lang="scss">
.boxSide {
    width: 80%;
    margin-top: 0px;

    .header {
        width: 100%;
        height: 100px;
        background-image: url("@/static/imgs/blog-header.gif");
        background-repeat: no-repeat;
        background-size: cover;
        background-position: center;
    }
}

.box {
    border: none;
    box-shadow: none;
    background-color: transparent;
    margin-bottom: 0px;
    padding-bottom: 0px;
}

.userInfoBox {
    display: flex;
    padding-bottom: 30px;

    .headImgBox {
        width: 120px;

        .headImg {
            width: 100px;
            height: 100px;
            border-radius: 50%;
            border: 5px solid white;
            cursor: url("@/static/imgs/cursor.png"), default !important;
            transform: translateY(-50px);
        }
    }

    .userNameBox {
        width: calc(100% - 120px);

        .nickNameBox {
            position: relative;
            width: 100%;

            .nickName {
                font-size: 24px;
                // font-weight: 600;
            }

            .toolsBox {
                position: absolute;
                top: 0px;
                right: 20px;
            }
        }

        .userProcBox {
            padding-top: 10px;

            .userProcs {
                display: inline-block;
                padding-left: 15px;
                padding-right: 15px;
                border-left: 1px solid rgb(196, 196, 196, 0.3);

                &:nth-child(1) {
                    padding-left: 0px;
                    border-left: 0px;
                    ;
                }

                span {
                    font-size: 22px;
                    font-weight: 600;
                }
            }
        }

        .userDetail {
            padding-top: 15px;
        }

        .ipPlace {
            padding-top: 15px;
        }

        .showDetails {
            padding-top: 15px;

            .showIcons {
                transform: translateY(3px);
            }
        }

        .hideDetails {

            .showIcons {
                transform: translateY(3px);
            }

            .line {
                padding-top: 15px;
            }
        }
    }
}

.borde {
    display: flex;
    margin: 0 auto;
    margin-top: 10px;
    width: calc(80% + 30px);
    $boxShadow: 0 2px 12px 0 rgba(0, 0, 0, 0.3);

    .l_borde {
        width: 26%;

        .yldj {
            box-shadow: $boxShadow;
            height: auto;

            .head {
                padding: 15px;
                border-bottom: var(--box-border);
            }

            .body {
                display: flex;
                padding: 15px;

                .body_child {
                    width: 33%;
                    text-align: center;

                    p {
                        font-weight: 600;
                        font-size: 20px;
                    }
                }
            }
        }
    }

    .r_borde {
        width: 73%;
        margin-left: 1%;
        box-shadow: $boxShadow;
        padding: 15px;

        .tabBoxs {
            padding: 20px;
            border-bottom: var(--box-border);
            display: flex;

            .tabImg {
                width: 25%;

                img {
                    width: 100%;
                }
            }

            .tabInfo {
                margin-left: 2%;
                width: 73%;

                .title {}
            }
        }
    }
}

@media screen and (max-width: 800px) {
    .userInfoBox {
        .userNameBox {
            .nickNameBox {

                // 编辑资料等按钮
                .toolsBox {
                    position: relative;
                    color: red;
                    display: block;
                    text-align: center;
                }
            }
        }
    }

    .borde {
        display: block;

        .l_borde,
        .r_borde {
            width: 100%;
        }

        .r_borde {
            margin: 0 auto;
            margin-top: 10px;
            padding-left: 0px;
            padding-right: 0px;
            margin-bottom: 10px;
        }
    }
}

:deep(.el-button) {
    &:hover {
        border: 1px solid black;
        color: #606266;
    }
}

:deep(.is-active) {
    color: darkgreen !important;
}

:deep(.is-top) {

    font-size: 18px;

    &:hover {
        color: rgb(100, 192, 100);
    }
}

:deep(.el-tabs__active-bar) {
    background-color: green;
}
</style>
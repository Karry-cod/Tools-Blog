<template>
    <div>
        <!-- 开头 -->
        <div class="boxSide">
            <p class="title">项目仓库</p>
        </div>
        <!-- 项目列表 -->
        <div class="body">
            <div class="boxSide centerBox">
                <el-scrollbar>
                    <div class="pro_li" v-for="item, index in proList">
                        <!-- 项目封面 -->
                        <div class="pro_img">
                            <img :src="item.imgUrl" alt="" />
                        </div>
                        <!-- 项目信息 -->
                        <div class="pro_info">
                            <p class="pro_title">{{ item.title }}</p>
                            <p class="pro_tag">
                                标签：
                                <el-tag v-for="tag in item.proTags" class="tags">{{ tag }}</el-tag>
                            </p>
                            <p class="pro_src">
                                项目地址：
                            <div>
                                <div v-for="proSrc in item.proSrcs" class="line">
                                    <span class="form">{{ proSrc.form }}</span>
                                    （<a :href="proSrc.src" class="src" target="_blank">
                                        {{ proSrc.src }}
                                    </a>）
                                </div>
                            </div>
                            </p>
                            <p style="display: flex;">
                            <div style="width: 33%;">项目介绍：</div>
                            <div class="pro_introduce" @click="showProIntroduce(index)">{{ item.introduce
                                }}撒大街上的科技撒赖扩大就撒肯定就萨洛克大家埃里克森角度来看撒旦吉萨肯德基案例撒肯定就撒赖可见度凯撒建档立卡时间啊大家离开洒家的撒肯定就奥斯陆扩大asdjakdja
                            </div>
                            </p>
                        </div>
                        <!-- 操作侧栏 -->
                        <div class="pro_tools">
                            <div @click="onlineLook(item)">
                                <el-icon class="toolIcons">
                                    <View />
                                </el-icon>
                                预览
                            </div>
                            <div>
                                <el-icon class="toolIcons">
                                    <Memo />
                                </el-icon>
                                <a :href="item.demoSrc" target="_blank">在线案例</a>
                            </div>
                        </div>
                    </div>
                </el-scrollbar>
            </div>
            <!-- 项目展示区 -->
            <div class="demoBox">
                <p v-if="!isDemoShow">项目展示</p>
                <iframe :src="currentFrame" frameborder="0" class="demoArea" v-else></iframe>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs } from "vue"
import { View, Memo } from "@element-plus/icons-vue"

export default defineComponent({
    setup() {
        let data = reactive({
            isDemoShow: true,
            proList: [{
                imgUrl: "https://img1.baidu.com/it/u=2249856134,3028142123&fm=253&app=138&size=w931&n=0&f=JPEG&fmt=auto?sec=1711126800&t=3f87a3ffd653d35eb2c44894a9164e22",
                title: "微客",
                proSrcs: [{
                    form: "gitee",
                    src: "https://gitee.com/Xu_zhuojiu/tools-blog.git",
                    icon: ""
                }],
                introduce: "项目介绍",
                proTags: ["vue3", ".net 6", "element-plus", "PC"],
                demoSrc: "http://karry.org.cn:5001/"
            },{
                imgUrl: "https://img1.baidu.com/it/u=2249856134,3028142123&fm=253&app=138&size=w931&n=0&f=JPEG&fmt=auto?sec=1711126800&t=3f87a3ffd653d35eb2c44894a9164e22",
                title: "Vitepress-Blob",
                proSrcs: [{
                    form: "gitee",
                    src: "https://gitee.com/Xu_zhuojiu/tools-blog.git",
                    icon: ""
                }],
                introduce: "项目介绍",
                proTags: ["Vitepress", "Vue", "PC"],
                demoSrc: "http://karry.org.cn:5002/"
            }],
            currentFrame: ""
        });

        const methods = {
            // 线上预览
            onlineLook(item: any) {
                data.currentFrame = item.demoSrc;
            },
            // 展示项目介绍
            showProIntroduce(index: number){
                console.log(index);
                let proIntroduces = document.getElementsByClassName("pro_introduce");
                if((proIntroduces[index] as HTMLElement).classList.contains('pro_introduce_show')){
                    (proIntroduces[index] as HTMLElement).classList.remove('pro_introduce_show');
                }else{
                    (proIntroduces[index] as HTMLElement).classList.add("pro_introduce_show");
                }
            }
        };

        return {
            ...toRefs(data),
            ...methods
        };
    }
})
</script>

<style lang="scss">
.body {
    width: 97%;
    display: flex;
    margin: 0 auto;
    margin-top: 15px;

    .centerBox {
        width: 62%;

        .pro_li {
            display: flex;
            border-bottom: 1px solid rgb(215, 209, 209, 0.5);
            min-height: 170px;
            position: relative;
            transition: 0.2s;
            overflow-x: hidden;

            &:hover {
                background-color: rgba(230, 223, 223, 0.3);
                box-shadow: 0px 5px 5px 0px gray;

                .pro_tools {
                    transform: translateX(0px);
                }
            }

            .pro_img {
                width: 30%;
                position: relative;

                >img {
                    display: block;
                    position: absolute;
                    max-width: 90%;
                    max-height: 90%;
                    top: 50%;
                    left: 50%;
                    transform: translate(-50%, -50%);
                }
            }

            .pro_info {
                padding-left: 20px;
                width: 70%;

                .pro_title {
                    font-size: 25px;
                    font-weight: 600;
                    margin-top: 0px;
                }

                .pro_tag {

                    .tags {
                        margin-left: 5px;
                        border-radius: 3px;
                    }
                }

                .pro_src {
                    >div {
                        display: inline-block;

                        .line {
                            .src {
                                text-decoration: underline;
                                color: rgb(12, 193, 193);
                                transition: 0.2s;

                                &:hover {
                                    color: red;
                                    cursor: var(--cursor-pointer);
                                }
                            }
                        }
                    }
                }

                p:nth-child(n) {
                    margin-top: 10px;
                }

                .pro_introduce {
                    display: -webkit-box;
                    -webkit-box-orient: vertical;
                    -webkit-line-clamp: 2;
                    overflow: hidden;
                    cursor: var(--cursor-pointer);
                    transition: 0.2s;

                    &:hover{
                        color: mediumpurple;
                    }
                }

                .pro_introduce_show {
                    display: inline-block;
                }
            }

            .pro_tools {
                position: absolute;
                right: 0px;
                height: 100%;
                box-shadow: 0px 0px 10px 1px gray;
                transform: translateX(120%);
                text-align: center;
                background-color: white;
                padding-left: 20px;
                padding-right: 20px;
                transition: 0.2s;

                >div {
                    margin-top: 20px;
                    cursor: var(--cursor-pointer);
                    transition: 0.2s;

                    .toolIcons {
                        display: block;
                        font-size: 25px;
                        margin: 0 auto;
                    }

                    &:hover {
                        text-shadow: 0px 0px 5px gray;
                    }
                }
            }
        }

        // display: inline-block;
    }

    .demoBox {
        width: 35%;
        border: 1px dashed rgb(216, 212, 212);
        margin-left: 2%;
        height: 75vh;
        position: relative;

        >p {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            text-align: center;
            color: gray;
        }

        .demoArea {
            width: 100%;
            height: 100%;
        }
    }
}
</style>
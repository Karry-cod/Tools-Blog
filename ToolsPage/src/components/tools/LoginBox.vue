<template>
    <div class="outside">
        <div class="header">
            <el-icon :size="30" class="closeBtn" @click="closeLogin()">
                <Close />
            </el-icon>
        </div>
        <!-- 中间主体 -->
        <div class="body">
            <!-- 扫码登录区 -->
            <div class="leftBody">
                <p>扫描二维码登录</p>
                <div style="border-right: 1px solid rgb(167, 164, 164, 0.2);">
                    <div class="ewm">
                        <img src="../../assets/imgs/loginEwm.png" alt="">
                    </div>
                    <div style="font-size: 13px;margin-top: 20px;">
                        请使用 <span style="color:rgb(255, 102 ,153)">哔哩哔哩客户端</span>
                        <br />
                        扫码登录或扫码下载APP
                    </div>
                </div>
            </div>
            <!-- 账号密码区 -->
            <div class="rightBody">
                <p>
                    <span style="cursor: pointer;" @click="loginType = 'pwd'"
                        :style="loginType === 'pwd' ? { color: 'rgb(255, 102 ,153)' } : { color: 'rgb(94, 90, 90)' }">密码登录</span>
                    <span
                        style="color: rgb(180, 175, 175, 0.8);transform: scaleX(0.2);display: inline-block;margin-left: 15px;margin-right: 15px;">|</span>
                    <span style="cursor: pointer;" @click="loginType = 'message'"
                        :style="loginType === 'message' ? { color: 'rgb(255, 102 ,153)' } : { color: 'rgb(94, 90, 90)' }">短信登录</span>
                </p>
                <!-- 输入登录信息区 -->
                <!-- 密码登录 -->
                <div class="loginBox_pwd" v-show="loginType === 'pwd'">
                    <!-- 账号 -->
                    <div class="accountBox">
                        <div class="title">账号</div>
                        <div class="input">
                            <input type="number" v-model="loginInfo.account" maxlength="20" placeholder="请输入账号" />
                        </div>
                    </div>
                    <!-- 密码 -->
                    <div class="pwdBox">
                        <div class="title">密码</div>
                        <div class="input" style="width: 50%;">
                            <input type="password" @focus="activeImg(true)" @blur="activeImg(false)"
                                v-model="loginInfo.pwd" v-show="!isShowPwd" maxlength="20" placeholder="请输入密码" />
                            <input type="text" @focus="activeImg(true)" @blur="activeImg(false)" v-model="loginInfo.pwd"
                                v-show="isShowPwd" maxlength="20" placeholder="请输入密码" />
                        </div>
                        <div class="pwdTools" style="width: calc(100% - 70px - 50%);">
                            <el-icon :size="20" class="pwd_showIcon" style="transform: translateY(4px);" color="gray">
                                <View v-show="isShowPwd" @click="isShowPwd = !isShowPwd" />
                                <Hide v-show="!isShowPwd" @click="isShowPwd = !isShowPwd" />
                            </el-icon>
                            <span
                                style="margin-left: 10px;font-size: 14px;cursor: pointer;color: rgb(255, 102 ,153);line-height: 40px;">忘记密码？</span>
                        </div>
                    </div>
                </div>
                <!-- 短信登录 -->
                <div class="loginBox_message" v-show="loginType === 'message'">
                    <!-- 手机号 / 电子邮箱 -->
                    <div class="phoneBox">
                        <div class="title" style="cursor: default;">
                            + 86
                        </div>
                        <div class="input" style="width: 48%;">
                            <input type="text" v-model="loginInfo.phone" placeholder="请输入手机号/电子邮箱" />
                        </div>
                        <div class="getPhone">
                            <span @click="getCode()"
                                :style="loginInfo.phone != '' ? { color: 'rgb(255, 102 ,153)', cursor: 'pointer' } : { cursor: 'not-allowed', color: 'rgb(156, 148, 148, 0.6)' }"
                                style="line-height: 45px;font-size: 14px;">获取验证码</span>
                        </div>
                    </div>
                    <!-- 短信验证码 -->
                    <div class="messageBox">
                        <div class="title">验证码</div>
                        <div class="input">
                            <input type="number" @focus="activeImg(true)" @blur="activeImg(false)"
                                v-model="loginInfo.message" placeholder="请输入验证码" maxlength="6" />
                        </div>
                    </div>
                </div>
                <!-- 账号登录按钮 -->
                <div class="btnBox_pwd" v-show="loginType === 'pwd'">
                    <div class="btn_regist" @click="toRegist()">注册</div>
                    <div class="btn_login" @click="toLogin()"
                        :class="loginInfo.account != '' && loginInfo.pwd != '' ? 'btn_login_active' : ''">登录</div>
                </div>
                <!-- 短信登录按钮 -->
                <div class="btnBox_message" @click="confirmLogin()"
                    :class="loginInfo.phone != '' && loginInfo.message != '' ? 'btnBox_message_active' : ''"
                    v-show="loginType === 'message'">
                    登录/注册
                </div>
            </div>
        </div>
        <!-- 底部信息 -->
        <div class="footInfo">
            未注册过哔哩哔哩的手机号，我们将自动帮你注册账号
            <br />
            登录或完成注册即代表你同意 <a href="https://www.bilibili.com/protocal/licence.html" target="_blank"
                style="cursor: pointer;color: rgb(255, 102 ,153);">用户协议</a> 和 <a
                href="https://www.bilibili.com/blackboard/privacy-pc.html" target="_blank"
                style="cursor: pointer;color: rgb(255, 102 ,153)">隐私政策</a>
        </div>
        <!-- 底部图片切换 -->
        <img src="../../assets/imgs/loginLeftImg.png" class="lbImg" v-show="!img_isActive" alt="">
        <img src="../../assets/imgs/loginRightImg.png" class="rbImg" v-show="!img_isActive" alt="">
        <img src="../../assets/imgs/loginLeftImg_active.png" class="lbImg" v-show="img_isActive" alt="">
        <img src="../../assets/imgs/loginRightImg_active.png" class="rbImg" v-show="img_isActive" alt="">
    </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs, onMounted } from "vue";
import { Close, View, Hide } from "@element-plus/icons-vue";
import type { ILoginInfo } from "@/utils/interfaces/ILogin";
import { ElMessage } from "element-plus";
import store from "@/utils/store"
import UserService from "@/api/apis/user"
import Notification from "@/utils/classes/Notification";
import Cookie from "@/utils/cookie"

export default defineComponent({
    components: {
        Close, View, Hide
    },
    setup() {
        let data = reactive({
            canMoving: false,
            XY: <any>{},
            loginType: "pwd",
            loginInfo: <ILoginInfo>{
                phone: ""
            },
            isShowPwd: false,
            img_isActive: false,
        });

        const methods = {
            // 当密码或者验证码聚焦时，切换捂眼睛的图片
            activeImg(mark: boolean) {
                data.img_isActive = mark;
            },
            // 点击注册按钮
            toRegist() {
                data.loginType = "message";
                ElMessage.info("输入手机号，注册账号");
            },
            // 关闭登录弹窗
            closeLogin() {
                store.state.isShowLogin = false;
            },
            // 去登录
            async toLogin() {
                let params = {
                    account: data.loginInfo.account,
                    password: data.loginInfo.pwd,
                    ip: await store.methods.getRealIP()
                };
                console.log(params, "????");
                UserService.Login(params).then(res => {
                    if (res.code === 1) {
                        ElMessage.success("登录成功！");
                        store.state.localUser = res.data;
                        store.state.isShowLogin = false;
                        Cookie.set("wk_cookie", res.message);
                        Cookie.set("localUser", JSON.stringify(res.data));
                    } else {
                        ElMessage.error(res.message);
                    }
                }).catch(err => {
                    ElMessage.error(err.message);
                });
            }, 
            // 获取验证码
            getCode(){
                if(data.loginInfo.phone?.indexOf("@") as number > -1){
                    UserService.CreateByEmail({
                        email: data.loginInfo.phone
                    }).then(res => {
                        ElMessage.success(res.message);
                    });
                }else{
                    ElMessage.warning("暂时只支持邮箱注册，手机号注册功能开通中");
                    return;
                    UserService.CreateByPhone({
                        phone: data.loginInfo.phone
                    }).then(res => {
                        Notification.Info(res.message);
                    });
                }
            },
            // 提交登录请求 - 手机号 / 电子邮箱
            confirmLogin(){
                let type = "";
                if(data.loginInfo.phone?.indexOf("@") as number > -1){
                    type = "Email";
                }else{
                    type = "Phone";
                    ElMessage.warning("暂时只支持邮箱注册，手机号注册功能开通中");
                    return;
                }
                let params = {
                    target: data.loginInfo.phone,
                    type,
                    password: "",
                    code: data.loginInfo.message,
                    ip: store.methods.getRealIP()
                };
                UserService.ConfirmCreate(params).then(res => {
                    console.log(res);
                    if(res.code === 1){
                        ElMessage.success(res.message);
                        Cookie.set("wk_cookie", res.message);
                        Cookie.set("localUser", JSON.stringify(res.data));
                    }else{
                        ElMessage.error(res.message);
                    }
                });
            }
        };

        return {
            ...toRefs(data),
            ...methods
        };
    }
})
</script>

<style lang="scss" scoped>
.outside {
    background-color: white;
    border-radius: 10px;
    overflow: hidden;
    width: 820px;
    text-align: center;
    height: 440px;
    position: absolute;
    z-index: 999;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);

    >.header {
        height: 50px;


        >.closeBtn {
            float: right;
            margin-right: 10px;
            margin-top: 10px;
            transition: 0.2s;
            cursor: pointer;
            border-radius: 5px;

            &:hover {
                background-color: rgb(136, 134, 134, 0.2);
            }
        }
    }

    >.body {
        display: flex;

        >.leftBody {
            width: 39%;

            >p {
                font-size: 18px;
                color: rgb(94, 90, 90);
                margin-bottom: 10px;
            }

            >div>.ewm {
                width: 175px;
                height: 175px;
                border-radius: 8px;
                border: 1px solid rgb(161, 157, 157, 0.2);
                margin: 0 auto;
                transition: 0.3s;

                >img {
                    margin-top: 7px;
                }
            }
        }

        >.rightBody {
            width: 61%;

            >p {
                font-size: 18px;
                color: rgb(94, 90, 90);
            }

            >.loginBox_pwd {
                border: 1px solid rgb(180, 176, 176, 0.5);
                border-radius: 10px;
                height: 90px;
                width: 80%;
                margin: 0 auto;
                margin-top: 30px;

                >.accountBox {
                    height: 50%;
                    border-bottom: 1px solid rgb(180, 176, 176, 0.5);
                    display: flex;
                }

                >.pwdBox {
                    display: flex;
                    height: 50%;

                    .pwdTools {
                        .pwd_showIcon {
                            &:hover {
                                cursor: pointer;
                                color: rgb(255, 102, 153) !important;
                            }
                        }
                    }
                }

                .title {
                    width: 70px;
                    line-height: 45px;
                    font-size: 14px;
                }

                .input {
                    width: calc(100% - 70px);
                    height: 100%;
                    overflow: hidden;
                    text-align: left;

                    >input {
                        line-height: 42px;
                        width: 80%;
                        outline: none;
                        border: 0px;
                    }
                }
            }

            >.loginBox_message {
                border: 1px solid rgb(180, 176, 176, 0.5);
                border-radius: 10px;
                height: 90px;
                width: 80%;
                margin: 0 auto;
                margin-top: 30px;

                >.phoneBox {
                    height: 50%;
                    border-bottom: 1px solid rgb(180, 176, 176, 0.5);
                    display: flex;

                    >.getPhone {
                        border-left: 1px solid rgb(180, 176, 176, 0.5);
                        text-align: center;
                        width: calc(100% - 80px - 50%);
                    }
                }

                >.messageBox {
                    display: flex;
                    height: 50%;
                }

                .title {
                    width: 80px;
                    line-height: 45px;
                    font-size: 14px;
                }

                .input {
                    width: calc(100% - 80px);
                    height: 100%;
                    overflow: hidden;
                    text-align: left;

                    >input {
                        line-height: 42px;
                        width: 80%;
                        outline: none;
                        border: 0px;
                    }
                }
            }

            >.btnBox_pwd {
                width: 80%;
                margin: 0 auto;
                margin-top: 40px;
                display: flex;
                font-size: 13px;

                >.btn_regist {
                    width: 48%;
                    height: 40px;
                    border: 1px solid rgb(180, 176, 176, 0.5);
                    border-radius: 7px;
                    line-height: 40px;
                    transition: 0.2s;
                    cursor: pointer;

                    &:hover {
                        border: 1px solid rgb(255, 102, 153);
                        color: rgb(255, 102, 153);
                    }
                }

                >.btn_login {
                    width: 48%;
                    height: 40px;
                    border-radius: 7px;
                    line-height: 40px;
                    color: white;
                    background-color: rgb(255, 179, 204);
                    margin-left: 4%;
                    cursor: not-allowed;
                }

                >.btn_login_active {
                    background-color: rgb(255, 102, 153);
                    transition: 0.2s;
                    cursor: pointer;

                    &:hover {
                        background-color: rgb(255, 125, 169);
                    }
                }
            }

            >.btnBox_message {
                background-color: rgb(255, 179, 204);
                border-radius: 8px;
                width: 195px;
                height: 40px;
                color: white;
                font-size: 13px;
                margin: 0 auto;
                margin-top: 40px;
                line-height: 40px;
                cursor: not-allowed;
            }

            >.btnBox_message_active {
                background-color: rgb(255, 102, 153);
                transition: 0.2s;
                cursor: pointer;

                &:hover {
                    background-color: rgb(255, 125, 169);
                }
            }
        }
    }

    >.footInfo {
        color: rgb(149, 145, 145);
        font-size: 13px;
        margin-top: 35px;
    }

    >.lbImg {
        position: absolute;
        bottom: 0px;
        left: 0px;
    }

    >.rbImg {
        position: absolute;
        bottom: 0px;
        right: 0px;
    }
}

input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
    -webkit-appearance: none !important;
}
</style>
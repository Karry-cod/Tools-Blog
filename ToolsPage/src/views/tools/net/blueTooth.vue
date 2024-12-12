<template>
    <div class="child_boxSide">
        <p class="title">蓝牙功能</p>
        <!-- 当前绑定蓝牙设备 -->
        <div class="module-Box">
            <p class="module-Title">当前已绑定蓝牙设备信息</p>
            <div>
                <el-descriptions title="当前设备信息：" direction="vertical" :column="3" border>
                    <template #extra>
                        <el-button type="primary" :icon="Key" @click="BindCurrentBlueTooth()"
                            :disabled="isBindCurrentBlueTooth()">绑定当前设备</el-button>
                    </template>
                    <el-descriptions-item label="蓝牙名称">{{ currentBlueTooth.name }}</el-descriptions-item>
                    <el-descriptions-item label="设备类型">{{ currentBlueTooth.driviceName }}</el-descriptions-item>
                    <el-descriptions-item label="蓝牙地址">{{ currentBlueTooth.address }}</el-descriptions-item>
                    <el-descriptions-item label="蓝牙状态">
                        <el-tag size="small" v-if="currentBlueTooth.mode === 'Discoverable'">暴露</el-tag>
                        <el-tag size="small" v-else-if="currentBlueTooth.mode === 'Coverable'">隐藏</el-tag>
                    </el-descriptions-item>
                    <el-descriptions-item label="绑定状态">
                        <el-tag size="small" v-if="isBindCurrentBlueTooth()">已绑定</el-tag>
                        <el-tag size="small" type="warning" v-else>未绑定</el-tag>
                    </el-descriptions-item>
                    <el-descriptions-item></el-descriptions-item>
                </el-descriptions>
                <el-table :data="myBlueTooths">
                    <el-table-column type="index"></el-table-column>
                    <el-table-column label="蓝牙名称">
                        <template #default="scoped">
                            {{ scoped.row.blueToothName }}
                        </template>
                    </el-table-column>
                    <el-table-column label="蓝牙地址">
                        <template #default="scoped">
                            {{ scoped.row.blueToothAddress }}
                        </template>
                    </el-table-column>
                    <el-table-column label="绑定时间">
                        <template #default="scoped">
                            {{ scoped.row.createdTime.toFullTime() }}
                        </template>
                    </el-table-column>
                    <el-table-column label="操作">
                        <template #default="scoped">
                            <el-popconfirm title="确定移除此设备吗?" @confirm="cancelBindBlueTooth(scoped.row.btuId)">
                                <template #reference>
                                    <el-button type="danger" size="small" :icon="CircleCloseFilled">移除</el-button>
                                </template>
                            </el-popconfirm>
                        </template>
                    </el-table-column>
                </el-table>
                <p class="topic">* 每个用户最多只允许绑定5个设备的蓝牙</p>
            </div>
        </div>
        <!-- 附件可检测的蓝牙设备 -->
        <div class="module-Box" style="margin-top: 40px">
            <p class="module-Title">附件可检测的蓝牙设备</p>
            <div>
                <el-table :data="nearBlueTooths">
                    <el-table-column type="index"></el-table-column>
                    <el-table-column label="蓝牙名称">
                        <template #default="scoped">
                            {{ scoped.row.blueToothName }}
                        </template>
                    </el-table-column>
                    <el-table-column label="设备类型">
                        <template #default="scoped">
                            {{ scoped.row.blueToothType }}
                        </template>
                    </el-table-column>
                    <el-table-column label="是否用户">
                        <template #default="scoped">
                            <el-tag type="success" v-if="scoped.row.uid">是</el-tag>
                            <el-tag type="danger" v-else>否</el-tag>
                        </template>
                    </el-table-column>
                    <el-table-column label="操作">
                        <template #default="scoped">
                            <div v-if="scoped.row.uid">
                                <el-button type="success" size="small">添加好友</el-button>
                                <el-button type="primary" size="small">立即聊天</el-button>
                            </div>
                        </template>
                    </el-table-column>
                </el-table>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, reactive, toRefs } from "vue"
import UserService from "@/api/apis/user"
import { Key, CircleCloseFilled } from "@element-plus/icons-vue"
import { ElMessage } from "element-plus";
import type { IBlueToothRadio, IBlueTooth_Users } from "@/utils/interfaces/IBlueTooth"

export default defineComponent({
    components: {
        Key, CircleCloseFilled
    },
    setup() {
        let data = reactive({
            currentBlueTooth: <IBlueToothRadio>{},
            myBlueTooths: <Array<IBlueTooth_Users>>[],
            nearBlueTooths: <Array<IBlueTooth_Users>>[]
        });

        const methods = {
            // 判断是否已绑定当前设备
            isBindCurrentBlueTooth() {
                let drivice = data.myBlueTooths.find(d => d.blueToothName + "-" + d.blueToothAddress === data.currentBlueTooth.name + "-" + data.currentBlueTooth.address);
                return drivice ? true : false;
            },
            // 绑定当前设备
            BindCurrentBlueTooth() {
                UserService.BindBlueToothByUser().then(res => {
                    if (res.code === 1) {
                        ElMessage.success("绑定成功");
                        events.getMyBlueTooths();
                    } else {
                        ElMessage.error(res.message);
                    }
                });
            },
            // 解除绑定的蓝牙设备
            cancelBindBlueTooth(btId: string) {
                UserService.CancelBindBlueTooth({ btId }).then(res => {
                    if (res.code === 1) {
                        ElMessage.success("操作成功！");
                        data.myBlueTooths = data.myBlueTooths.filter(item => {
                            if (item.btuId != btId) {
                                return item;
                            }
                        });
                    } else {
                        ElMessage.error(res.message);
                    }
                });
            },
            // 获取附近可检测到的蓝牙设备列表
            getBlueTooths() {
                UserService.GetBlueTooths().then(res => {
                    if (res.code === 1) {
                        data.nearBlueTooths = res.data;
                        console.log(data.nearBlueTooths, "??");
                    } else {
                        ElMessage.error(res.message);
                    }
                });
            },
        };

        const events = {
            // 获取已绑定的蓝牙设备信息集合
            getMyBlueTooths() {
                UserService.GetMyBlueTooths().then(res => {
                    console.log(res, "result");
                    if (res.code === 1) {
                        data.myBlueTooths = res.data;
                    } else {
                        ElMessage.error(res.message);
                    }
                });
            },
            // 获取当前设备信息
            getCurrentBlueTooth() {
                UserService.GetCurrentBlueTooth().then(res => {
                    if (res.code === 1) {
                        data.currentBlueTooth = res.data;
                        events.getMyBlueTooths();
                        methods.getBlueTooths();
                    } else {
                        ElMessage.error(res.message);
                    }
                });
            }
        };

        onMounted(() => {
            events.getCurrentBlueTooth();
        });

        const icons = {
            Key, CircleCloseFilled
        };

        return {
            ...toRefs(data),
            ...icons,
            ...methods
        };
    }
})
</script>

<style lang="scss" scoped>
.module-Box {
    .topic {
        color: red;
        font-size: 12px;
    }
}
</style>
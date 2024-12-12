<template>
    <div class="child_boxSide">
        <p class="title">地图</p>
        <div class="searchBox">
            <el-row :span="24">
                <el-col :span="12">
                    名称：
                    <div class="searchInput">
                        <el-input placeholder="请输入要搜索的关键字" v-model="searchName">
                            <template #append>
                                <el-button :disabled="searchName.isNull()" :icon="Search" @click="getPois()" />
                            </template>
                        </el-input>
                        <div class="searchContentBox" v-show="!isLoad">
                            <div class="pois pointer" v-for="item in pois" :key="item.id">
                                <p v-html="store.methods.redMainText(item.name, searchName)"></p>
                                <p>{{ item.pname + " · " + item.cityname + " · " + item.adname + " · " +
                item.address }}</p>
                            </div>
                        </div>
                    </div>
                </el-col>
                <el-col :span="8">
                    &nbsp;&nbsp;显示模式：
                    <el-select v-model="viewMode" @change="changeViewMode()">
                        <el-option key="2d" value="2d" label="2d">2d</el-option>
                        <el-option key="3d" value="3d" label="3d">3d</el-option>
                    </el-select>
                </el-col>
            </el-row>
        </div>
        <!-- 地图 -->
        <div class="map" v-loading="isLoad" id="container">

        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, reactive, toRefs, onMounted, onUnmounted } from "vue"
import AMapLoader from "@amap/amap-jsapi-loader";
import store from "@/utils/store"
import Notification from "../../../utils/classes/Notification";
import AmapService from "@/api/apis/amap"
import { Search } from "@element-plus/icons-vue"

export default defineComponent({
    components: {
        Search
    },
    setup() {
        let data = reactive({
            map: <any>null,
            amapConfig: <any>{},
            AMap: <any>null,
            pos: <any>[], // 经纬度
            viewMode: "2d",
            searchName: "", // 关键字
            pois: [{
                name: "国家会展中心(上海)",
                id: "B0FFFDXOI4",
                location: "121.302183,31.189991",
                type: "科教文化服务;会展中心;会展中心",
                typecode: "140300",
                pname: "上海市",
                cityname: "上海市",
                adname: "青浦区",
                address: "崧泽大道333号",
                pcode: "310000",
                adcode: "310118",
                citycode: "021"
            }],
            isLoad: true
        });

        const methods = {
            // 加载2d地图
            loadMap_2d() {
                data.map = new data.AMap.Map("container", {
                    // 设置地图容器id
                    viewMode: "3D", // 是否为3D地图模式
                    zoom: 15, // 初始化地图级别
                    center: data.pos ? [data.pos[0], data.pos[1]] : null // 初始化地图中心点位置
                });

                data.isLoad = false;
            },
            // 加载3d地图
            loadMap_3d() {
                data.map = new data.AMap.Map('container', {
                    rotateEnable: true,
                    pitchEnable: true,
                    zoom: 17,
                    pitch: 50,
                    rotation: -15,
                    viewMode: '3D', //开启3D视图,默认为关闭
                    zooms: [2, 20],
                    center: data.pos ? [data.pos[0], data.pos[1]] : null
                });

                var controlBar = new data.AMap.ControlBar({
                    position: {
                        right: '10px',
                        top: '10px'
                    }
                });
                controlBar.addTo(data.map);

                var toolBar = new data.AMap.ToolBar({
                    position: {
                        right: '40px',
                        top: '110px'
                    }
                });
                toolBar.addTo(data.map);
                data.isLoad = false;
            },
            // 获取当前所在具体定位
            getLocation() {
                data.AMap.plugin('AMap.Geolocation', function () {
                    console.log("正常？");
                    var geolocation = new data.AMap.Geolocation({
                        enableHighAccuracy: true, // 是否使用高精度定位，默认：true
                        timeout: 10000, // 设置定位超时时间，默认：无穷大
                        offset: [10, 20],  // 定位按钮的停靠位置的偏移量
                        zoomToAccuracy: true,  //  定位成功后调整地图视野范围使定位位置及精度范围视野内可见，默认：false
                        position: 'RB' //  定位按钮的排放位置,  RB表示右下
                    })
                    console.log("正常吗？");
                    geolocation.getCurrentPosition(function (status: any, result: any) {
                        if (status == 'complete') {
                            console.log(result);
                            data.pos = [result.position.lng, result.position.lat];
                            console.log(data.pos);
                            methods.loadMap_2d();
                        } else {
                            data.pos = null;
                            methods.loadMap_2d();
                            Notification.Warning("当前环境无法定位");
                            console.log(status, result, "错误");
                        }
                    });
                })
            },
            // 切换显示模式
            changeViewMode() {
                if (data.viewMode === "2d") {
                    methods.loadMap_2d();
                } else {
                    methods.loadMap_3d();
                }
            },
            // 通过关键字获取地点列表
            getPois() {
                let params = {
                    keywords: data.searchName
                };
                AmapService.GetPOI2(params).then(res => {
                    console.log(res, "结果");
                    if (res.code === 1) {
                        data.pois = res.data.pois
                    }
                });
            }
        };

        const events = {
            // 加载地图渲染器
            loadMapLoader() {
                AMapLoader.load({
                    key: store.state.config.amap.key, // 申请好的Web端开发者Key，首次调用 load 时必填
                    version: "2.0", // 指定要加载的 JSAPI 的版本，缺省时默认为 1.4.15
                    plugins: ["AMap.Scale"], //需要使用的的插件列表，如比例尺'AMap.Scale'，支持添加多个如：['...','...']
                })
                    .then((AMap) => {
                        data.AMap = AMap;
                        methods.getLocation();
                    })
            },
        };

        const icons = {
            Search
        };

        onMounted(() => {
            window._AMapSecurityConfig = {
                securityJsCode: store.state.config.amap.secret,
            };
            events.loadMapLoader();
        });

        onUnmounted(() => {
            data.map?.destroy();
        });

        return {
            ...toRefs(data),
            ...methods,
            store,
            ...icons
        };
    }
})
</script>

<style lang="scss" scoped>
.searchBox {
    margin-top: 10px;
    margin-bottom: 10px;

    .searchInput {
        width: 90%;
        position: relative;
        display: inline-block;

        .searchContentBox {
            background-color: white;
            padding-top: 5px;
            margin-top: 5px;
            width: 100%;
            position: absolute;
            z-index: 999;
            box-shadow: 0px 0px 5px 1px rgb(205, 204, 204, 0.5);
            border-radius: 5px;
            max-height: 350px;
            overflow-y: scroll;

            .pois {
                padding: 10px;
                transition: 0.3s;

                p {
                    line-height: 25px;
                }

                p:nth-child(2) {
                    font-size: 13px;
                    color: gray;
                }

                &:hover {
                    background-color: rgb(209, 202, 202, 0.2);
                }
            }
        }
    }
}

#container {
    padding: 0px;
    margin: 0px;
    width: 100%;
    height: 800px;
}
</style>
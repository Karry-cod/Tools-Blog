String.prototype.parseInt = function () {
    let str = this.replace("px", "");
    return parseInt(str);
};

// 生成唯一标识
let date = new Date();
let id = parseInt(Math.random() * 10).toString() + date.getMilliseconds();

// 场景
let screen = document.getElementsByClassName("screen")[0];

// 鱼
let fishDiv = document.createElement("div");
fishDiv.className = "fish " + id;
screen.append(fishDiv);
let fish = document.getElementsByClassName(id)[0];

// 游戏状态
// 0: 准备中, 1: 开始, 2: 结束
let gameState = 0;

// 开始游戏
function startGame() {
    /** 建立连接
         */
    const socket = new WebSocket("ws://47.116.114.17:5000/ws");
    let socketState = "";

    // 连接成功
    socket.onopen = (e) => {
        console.log("连接中... ", e);
        socketState = "success";
        socket.send(JSON.stringify({
            action: "join",
            msg: `用户${id}进入游戏`,
            nick: `${id}`,
            roomNo: "大鱼吃小鱼",
            uid: id
        }));
    };

    // 接收消息
    socket.onmessage = function (e) {
        // console.log("接收消息：", e);
        try {
            let data = JSON.parse(e.data); // 获取其他鱼儿的信息
            if (data.uid != id) {
                let fishes = document.getElementsByClassName("fish");
                let newFish = null;
                for (let i = 0; i < fishes.length; i++) {
                    if (fishes[i].className.indexOf(data.uid) > -1) {
                        newFish = fishes[i];
                    }
                }
                // 如果游戏有新的鱼儿，则加入场景中
                if (!newFish) {
                    newFish = document.createElement("div");
                    newFish.className = "fish " + data.uid;
                    screen.append(newFish);
                } else {
                    if (data.msg === "变大") {
                        newFish.style.width = getComputedStyle(newFish).width.parseInt() + 5 + "px";
                        newFish.style.height = getComputedStyle(newFish).height.parseInt() + 2 + "px";
                    } else if (data.msg.indexOf("大鱼生成") > -1) {
                        return;
                        console.log(data.msg);
                        let strs = data.msg.split(",");
                        let bigFish = document.createElement("div");
                        let img = document.createElement("img");
                        img.src = "fish.png";
                        bigFish.append(img);
                        bigFish.className = "bigfish";
                        bigFish.style.top = strs[1];
                        bigFish.style.left = strs[2];
                        // 追加大鱼
                        screen.append(bigFish);
                        let timer = setInterval(() => {
                            if (bigFishAction(bigFish, strs[3])) {
                                clearInterval(timer);
                                timer = null;
                            }
                        }, 100);
                    }
                    else {
                        newFish.style.top = data.msg.split(",")[2];
                        newFish.style.left = data.msg.split(",")[1];
                        if(data.msg.split(",")[3] === "a"){
                            newFish.style.transform = "rotate3d(0, 1, 0, 180deg)";
                        }else if(data.msg.split(",")[3] === "d"){
                            newFish.style.transform = "rotate3d(0, 1, 0, 0deg)";
                        }
                    }
                }
            }
        } catch { }
    };


    /** 监听场景下的按键W,S,A,D,控制小鱼的行动
    */
    function fishAction() {
        // 按键状态
        // 0: 松开，1：按住
        let keyState = 0;

        // 用于管理鱼行动的操作者
        let actionTimer = null;

        // 用于保存按下的按键
        let keyer = {};

        // 键盘按下
        window.document.addEventListener("keydown", (e) => {
            const key = e.key;
            keyer[key] = true;
            keyState = 1;
            // action(key);
            if (!actionTimer) {
                actionTimer = setInterval(() => {
                    Object.keys(keyer).forEach(item => {
                        keyer[item] ? action(item, fish, true) : null;
                    });
                }, 100);
            }
        });

        // 键盘松开
        window.document.addEventListener("keyup", (e) => {
            keyState = 0;

            // 清除松开的按键
            keyer[e.key] = false;

            // 用于判断是否所有按键都松开
            let mark = false;
            Object.values(keyer).forEach(d => {
                if (d) {
                    mark = true;
                }
            });
            if (!mark) {
                // 行动结束要清理操作者
                clearInterval(actionTimer);
                actionTimer = null;
            }
        });

    }

    // 鱼的行动
    function action(key, currentFish, bool) {
        key = key.toLowerCase();

        switch (key) {
            case "a":
                currentFish.style.transform = "rotate3d(0, 1, 0, 180deg)";
                if (getComputedStyle(currentFish).left.parseInt() > 0) {
                    currentFish.style.left = getComputedStyle(currentFish).left.parseInt() - 10 + "px";
                }
                break;
            case "w":
                if (getComputedStyle(currentFish).top.parseInt() > 0) {
                    currentFish.style.top = getComputedStyle(currentFish).top.parseInt() - 10 + "px";
                }
                break;
            case "s":
                if (getComputedStyle(currentFish).top.parseInt() < document.body.clientHeight - getComputedStyle(currentFish).height.parseInt()) {
                    currentFish.style.top = getComputedStyle(currentFish).top.parseInt() + 10 + "px";
                }
                break;
            case "d":
                currentFish.style.transform = "rotate(0deg)";
                if (getComputedStyle(currentFish).left.parseInt() < document.body.clientWidth - getComputedStyle(currentFish).width.parseInt()) {
                    currentFish.style.left = getComputedStyle(currentFish).left.parseInt() + 10 + "px";
                }
                break;
        }

        if (bool) {
            socket.send(JSON.stringify({
                action: "send_to_room",
                msg: `鱼的行动,${currentFish.style.left},${currentFish.style.top},${key}`,
                nick: `${id}`,
                roomNo: "大鱼吃小鱼",
                uid: id
            }));
        }
    }

    /** 刷新大鱼
     */
    function refreshBigFish() {
        let bigFish = document.createElement("div");
        let img = document.createElement("img");
        img.src = "bigfish.png";
        bigFish.append(img);
        // 定义随机数
        let random = Math.random() * 100;
        bigFish.style.top = random + "%";

        bigFish.className = "bigfish " + random;

        // 追加大鱼
        screen.append(bigFish);

        // 防止大鱼的身体卡在场景上下
        if (getComputedStyle(bigFish).top.parseInt() + getComputedStyle(bigFish).height.parseInt() > getComputedStyle(screen).height.parseInt()) {
            bigFish.style.top = getComputedStyle(screen).height.parseInt() - getComputedStyle(bigFish).height.parseInt() + "px";
        }
        if (getComputedStyle(bigFish).top.parseInt() < 0) {
            bigFish.style.top = 0 + "px";
        }

        // 判断大鱼从何方向进场
        let isLeft = random > 50 ? false : true;

        // 默认大鱼从场景外进入
        if (!isLeft) {
            bigFish.style.transform = "rotate3d(0, 1, 0, 180deg)";
            bigFish.style.left = getComputedStyle(screen).width.parseInt() + "px";
        } else {
            bigFish.style.left = getComputedStyle(img).width.parseInt() * -1 + "px";
        }

        // 同步大鱼的生成
        if(socketState === "success"){
            let top = 0;
            let left = 0;
            if(bigFish.style.top.indexOf("px") > -1){
                top = bigFish.style.top.parseInt() / getComputedStyle(screen).height.parseInt() + "%";
            }else{
                top = bigFish.style.top;
            }
            if(bigFish.style.left.indexOf("px") > -1){
                left = bigFish.style.left.parseInt() / getComputedStyle(screen).width.parseInt() + "%";
            }else{
                left = bigFish.style.left;
            }

            socket.send(JSON.stringify({
                action: "send_to_room",
                msg: `大鱼生成,${top},${left},${isLeft}`,
                nick: `${id}`,
                roomNo: "大鱼吃小鱼",
                uid: id
            }));
        }

        let timer = setInterval(() => {
            if (bigFishAction(bigFish, isLeft)) {
                clearInterval(timer);
                timer = null;
            }
        }, 100);

        setTimeout(() => {
            window.requestAnimationFrame(refreshBigFish);
        }, 1000);
    }

    // 大鱼的行动
    function bigFishAction(bigFish, isLeft) {
        bigFish.style.left = getComputedStyle(bigFish).left.parseInt() + (isLeft ? 10 : -10) + "px";
        let screenW = getComputedStyle(screen).width.parseInt();
        let bigFishL = getComputedStyle(bigFish).left.parseInt();
        let bigFishW = getComputedStyle(bigFish).width.parseInt();
        let bigFishY = getComputedStyle(bigFish).top.parseInt();
        let bigFishH = getComputedStyle(bigFish).height.parseInt();
        // 判断大鱼是否行动超出场景, 超出则销毁
        if (isLeft) {
            if (bigFishL > screenW) {
                bigFish.remove();
                return true;
            }
        } else {
            if (bigFishL < bigFishW * -1) {
                bigFish.remove();
                return true;
            }
        }

        // 判断大鱼是否鱼主角发生了碰撞
        let fishX = getComputedStyle(fish).left.parseInt();
        let fishY = getComputedStyle(fish).top.parseInt();
        let fishW = getComputedStyle(fish).width.parseInt();
        let fishH = getComputedStyle(fish).height.parseInt();
        let isCrash = checkCrash(fishX, fishY, fishW, fishH, bigFishL, bigFishY, bigFishW, bigFishH);
        if (isCrash) {
            bigFish.remove();
            fish.style.width = fishW + 5 + "px";
            fish.style.height = fishH + 2 + "px";

            // 同步鱼的状态
            socket.send(JSON.stringify({
                action: "send_to_room",
                msg: `变大`,
                nick: `${id}`,
                roomNo: "大鱼吃小鱼",
                uid: id
            }));

            return true;
        }
        return false;
    }

    /** 监听是否碰撞
     */
    function checkCrash(x1, y1, yw, yh, x2, y2, bw, bh) {
        var yx = x1;
        var yr = x1 + yw;
        var yy = y1;
        var yb = y1 + yh;

        var bx = x2;
        var br = x2 + bw;
        var by = y2;
        var bb = y2 + bh;
        //如果鱼的右边小于炮弹的x坐标or鱼的x坐标大于炮弹的右边or鱼的底部小于炮弹的y坐标or鱼的y坐标大于炮弹的底部
        if (yr < bx || yx > br || yb < by || yy > bb) {
            return false;
        } else {
            return true;
        }
    }

    window.requestAnimationFrame(refreshBigFish);
    fishAction();
}

if (window.confirm("是否开始游戏")) {
    gameState = 1;
    startGame();
}

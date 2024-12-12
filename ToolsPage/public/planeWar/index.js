String.prototype.parseInt = function () {
    let str = this.replace("px", "");
    return parseInt(str);
};

// 飞机
const plane = document.getElementsByClassName("plane")[0];

// 游戏场景
const screen = document.getElementsByClassName("canvas")[0];

// 敌机
let enemys = document.getElementsByClassName("enemy");

// 游戏进程
// 0: 准备中，1：进行时，2：结束
let gameState = 0;

/** 开始游戏
 * 
*/ 
function startGame() {
    /** 刷新子弹
    */
    function refreshBullet() {
        // 1. 先定义一个子弹
        let bullet = document.createElement("div");
        bullet.className = "bullet";

        // 2. 获取飞机所在位置
        let planeX = getComputedStyle(plane).left;
        let planeY = getComputedStyle(plane).top;

        // 3. 设置子弹的起始位置
        bullet.style.top = (parseInt(planeY.replace("px", "")) - 20) + "px";
        bullet.style.left = (parseInt(planeX.replace("px", "")) - 10) + "px";

        // 4. 将子弹追加进场景中
        screen.appendChild(bullet);

        // 5. 更新子弹的行动
        bulletAction(bullet);

        if (gameState === 1) {
            setTimeout(() => {
                window.requestAnimationFrame(refreshBullet);
            }, 100);
        }
    }

    /** 子弹行动
    */
    function bulletAction(bullet) {
        bullet.style.top = parseInt(getComputedStyle(bullet).top.replace("px", "")) - 20 + "px";

        // 判断子弹是否飞出场景
        if (parseInt(getComputedStyle(bullet).top.replace("px", "")) < 0) {
            bullet.remove();
        } else {
            // 获取子弹坐标
            let bulletX = getComputedStyle(bullet).left.parseInt();
            let bulletY = getComputedStyle(bullet).top.parseInt();
            let bulletW = getComputedStyle(bullet).width.parseInt();
            let bulletH = getComputedStyle(bullet).height.parseInt();

            // 判断子弹是否与敌机相撞
            for (let i = 0; i < enemys.length; i++) {
                const item = enemys[i];

                // 获取敌机坐标
                let enemyX = getComputedStyle(item).left.parseInt();
                let enemyY = getComputedStyle(item).top.parseInt();
                let enemyWidth = getComputedStyle(item).width.parseInt(); // 敌机宽度
                let enemyHeight = getComputedStyle(item).height.parseInt(); // 敌机高度

                // 监测是否碰撞
                let mark = checkCrash(bulletX, bulletY, bulletW, bulletH, enemyX, enemyY, enemyWidth, enemyHeight);

                if (mark) {
                    bullet.remove();
                    item.remove();
                    return;
                }
            }

            if (gameState === 1) {
                setTimeout(() => {
                    bulletAction(bullet);
                }, 100);
            }
        }

    }

    /** 飞机随着鼠标滑动
    */
    function planeAction() {
        document.body.addEventListener("mousemove", (e) => {
            if(gameState != 1){
                return;
            }

            // 1. 监听鼠标的位置
            let X = e.pageX;
            let Y = e.pageY;

            // 2. 飞机移动
            plane.style.top = Y + "px";
            plane.style.left = X + "px";

            // 3. 监测飞机是否与敌机碰撞
            // 飞机状态
            let planeX = getComputedStyle(plane).left.parseInt();
            let planeY = getComputedStyle(plane).top.parseInt();
            let planeW = getComputedStyle(plane).width.parseInt();
            let planeH = getComputedStyle(plane).height.parseInt();

            for(let i = 0; i < enemys.length; i++){
                const item = enemys[i]; // 敌机

                // 敌机状态
                let enemyX = getComputedStyle(item).left.parseInt();
                let enemyY = getComputedStyle(item).top.parseInt();
                let enemyW = getComputedStyle(item).width.parseInt();
                let enemyH = getComputedStyle(item).height.parseInt();

                let mark = checkCrash(planeX, planeY, planeW, planeH, enemyX, enemyY, enemyW, enemyH);
                if(mark){
                    alert("与敌机发生碰撞，游戏结束");
                    gameState = 2;
                }
            }
        });
    }

    /** 刷新敌机
    */
    function refreshEnemy() {
        // 1. 定义一个新的敌机
        let enemy = document.createElement("div");
        enemy.className = "enemy";

        // 2. 初始敌机的位置
        let x = parseInt(Math.random() * 100);
        enemy.style.top = "-150px";
        enemy.style.left = x + "%";

        // 3. 将敌机追加进画布中
        screen.append(enemy);

        // 判断敌机是否超出场景
        if(getComputedStyle(enemy).left.replace("px", "").parseInt() + 150 > getComputedStyle(screen).width.replace("px", "").parseInt()){
            enemy.style.left = getComputedStyle(enemy).left.replace("px", "").parseInt() - 150 + "px";
        }

        enemyAction(enemy);
        enemys = document.getElementsByClassName("enemy"); // 更新敌机集合

        if (gameState === 1) {
            setTimeout(() => {
                window.requestAnimationFrame(refreshEnemy);
            }, 500);
        }
    }

    /** 敌机行动
    */
    function enemyAction(enemy) {
        enemy.style.top = getComputedStyle(enemy).top.parseInt() + 50 + "px";

        let bodyHeight = getComputedStyle(screen).height.parseInt();

        if (getComputedStyle(enemy).top.parseInt() > bodyHeight - 150) {
            gameState = 2;
            alert("游戏结束");
        }

        if (gameState === 1) {
            setTimeout(() => {
                enemyAction(enemy);
            }, 500);
        }
    }

    /** 检测是否飞机与敌机发生碰撞 
     * 参数 飞机的x y width height 敌机 x y width height
    */
    function checkCrash(x1, y1, yw, yh, x2, y2, bw, bh){
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

    window.requestAnimationFrame(refreshBullet);
    window.requestAnimationFrame(refreshEnemy);
    planeAction();

}

// 询问是否开始游戏
let mark = window.confirm("确定开始游戏吗");
if(mark){
    gameState = 1;
    startGame();
}else{

}
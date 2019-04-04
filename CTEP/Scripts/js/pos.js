

var _posconfig = {
    baseUrl: "http://localhost:55810/",
    actionList: [
        { name: "login", url: "User/Login/", data: { id: 1, email: "1132067567@qq.com", pw: 123, role: 0 } },
        { name: "register", url: "User/register/", data: { id: 1, email: "1132067567@qq.com", pw: 123, role: 0 } }
    ],
    responseList: [
        {
            name: "login",
            func: function (response) {
                if (response == null) {

                }
                else {
                    alert(response.id);
                }

            }
        },
        {
            name: "register",
            func: function (data) {
                console.log(this.name + "响应处理器 收到数据:" + data);
            }
        }
    ],
    triggerList: [
        { name: "#login", action: "login", event: "click" },
        { name: "#register", action: "register", event: "click", index: -1 },
    ],
}

// var Data={};
//post 数据的方法
function _post(url, obj) {
    var result;
    try {
        $.ajax({
            url: url,
            type: "post",
            data: obj,
            async: false,
            success: function (res) {
                result = res;
            }
        });
    } catch (error) {
        result = null;
    }

    return result;
};

function POS() {

    //网站根URL 
    this.baseUrl = _posconfig.baseUrl;
    // 动作列表
    this.actionList = _posconfig.actionList;
    //响应列表
    this.responseList = _posconfig.responseList;
    //触发器列表
    this.triggerList = _posconfig.triggerList;
    // 数据列表
    this.dataList = [];

    //动作 得到数据 
    this.Do = function (activ, data, index) {
        let items = this.actionList;
        for (var i in items) {
            let act = items[i];
            if (act.name == activ) {
                console.log("《—————————————————————动作运行——————————————————————》");
                console.log("生成动作 url:" + this.baseUrl + act.url);
                console.log("提交数据↓:");
                console.log(data);
                console.log("提交数据↑:");
                var res = _post(this.baseUrl + act.url, data);
                this.responseList.forEach(element => {
                    if (element.name == activ) {
                        // let res=1 测试数据
                        console.log("-↓");
                        console.log("动作:" + activ + "的响应数据↓");
                        console.log(res);
                        console.log("---↓");
                        if (arguments.length == 2) {
                            console.log("系统写入标识符 index:" + i);
                            index = i;
                        } else {
                            console.log("写入标识符 index:" + index);
                        }
                        console.log("--↓");
                        console.log("执行动作:" + activ + "响应配置表中的响应操作");
                        element.func(res);
                        console.log("---↓");
                        console.log("写入数据列表");
                        this.dataList.push({ name: activ, response: res, index: index });
                        console.log("---数据列表↓");
                        for (const i in this.dataList) { console.log(this.dataList[i]); }
                        return res;
                    }

                });

            }
        }

    };

    this.DoIt = function (activ, index) {

        let items = this.actionList;
        for (var i in items) {
            let act = items[i];
            if (act.name == activ) {

                if (arguments.length == 1) {
                   return this.Do(activ, act.data);
                }
                else {
                    return this.Do(activ, act.data, index);
                }

            }
        }


    }

    //通过行为与得到的数据绑定
    this.Bind = function (activ, func, index) {
        console.log("已绑定" + activ + "数据队列处理器");
        //获取数据列表
        let items = this.dataList;
        for (var i in items) {

            let act = items[i];
            console.log("遍历数据列表数据显示:↓");
            console.log(act);
            if (act.name == activ) {
                console.log("捕获动作:" + activ + "的响应数据:");
                console.log(act.response);
                try {
                    console.log("数据列表数据已传入->动作:" + activ + "的响应处理器！");
                    if (index != null) {
                        if (act.index == index) {
                            console.log("标识符数据队列处理器 执行-----↓");
                            func(act.response);
                        }
                        else {
                            console.log("此动作的标识符下没有处理数据");
                        }

                    } else {
                        console.log("数据队列处理器 执行-----↓");
                        func(act.response);
                    }


                } catch (error) {
                    return error;
                }
                this.dataList.splice(i, 1);
                console.log("动作:" + activ + "的响应数据处理完成,数据弹出数据列表！");
                console.log("数据列表数据显示:-----↓");
                for (const i in this.dataList) { console.log(this.dataList[i]); }

                return true;
            }
        }


    }



    this.triggerRegister = function () {
        // 注册激活器事件
        console.log("POS 初始化->独立实例 每一个激发器都是新的POS对象发起的 注册激活器事件:↓");
        let triggers = _posconfig.triggerList
        for (const i in triggers) {
            let trigger = triggers[i]

            console.log("注册激活器事件:" + trigger.action + "↓");
            console.log("激活器:" + trigger.name + "↓");
            $(trigger.name).on(trigger.event, function () {
                console.log("———————————————————————————————trigger-index——》" + trigger.index);
                if (trigger.index < 0 || trigger.index == null) {
                    new POS().DoIt(trigger.action);
                }
                else {
                    new POS().DoIt(trigger.action, trigger.index);
                }

            });
        }

        console.log("数据列表数据显示:--------↓");
        for (const i in this.dataList) { console.log(this.dataList[i]); }
        console.log("数据列表数据显示----↑");



    }

    this.triggersRegister = function (pos) {
        // 注册激活器事件
        console.log("POS 初始化->公用实例 共享传入的POS对象 数据队列 注册激活器事件:↓");
        let triggers = _posconfig.triggerList
        for (const i in triggers) {
            let trigger = triggers[i]
            console.log("注册激活器事件:" + trigger.action + "↓");
            console.log("激活器:" + trigger.name + "↓");
            $(trigger.name).on(trigger.event, function () {
                console.log("(相同动作的不同处理器)trigger-index->:" + trigger.index);
                if (trigger.index < 0 || trigger.index == null) {
                    pos.DoIt(trigger.action);
                }
                else {
                    pos.DoIt(trigger.action, trigger.index);
                }

            });
        }

        console.log("————————————————————数据列表数据显示:↓——————————————————————");

        for (const i in this.dataList) { console.log(this.dataList[i]); }
        console.log("----↓");


    }


    console.log("POS 初始化开始状态:↓");
    console.log(this instanceof POS);
}

function POSIni(pos) {
    pos.triggersRegister(pos);
    console.log("《—————————————————————POS 公用实例 初始化完成！——————————————————————》");
}
function POSini(pos) {
    pos.triggerRegister();
    console.log("《—————————————————————POS 独立实例 初始化完成！——————————————————————》");
}

// _posconfig.activeList = [];

// var appdata=_post("http://localhost:55810/User/Login/", { email: "1132067567@qq.com", pw: 123, role: 0 });


// console.log(appdata.id);

// 网站根URL
window.onload = function () {
    // POSIni(new POS());
    // POSini(new POS());
}

// var pos = new POS();

// pos.DoIt("login", 5);
// pos.DoIt("login", 2);
// pos.Bind("login", function (date) {   
//     console.log(date);
// },5);




$(document).ready(function () {
    //
    String.prototype.format = function () {
        if (arguments.length == 0) return this;
        var param = arguments[0];
        var s = this;
        if (typeof (param) == 'object') {
            for (var key in param)
                s = s.replace(new RegExp("\\{" + key + "\\}", "g"), param[key]);
            return s;
        } else {
            for (var i = 0; i < arguments.length; i++)
                s = s.replace(new RegExp("\\{" + i + "\\}", "g"), arguments[i]);
            return s;
        }
    }


    //元数据类型声明
    // 用户
    var emb_User = {
        id: -1,
        email: "",
        pw: "",
        role: "",
        status: "",
    };
    // 用户信息
    var emb_User_info = {
        id: "",
        img: "",
        gender: "",
        name: "",
        descrition: "",
        address: "",
        BandiID: ""
    }
    var emb_User_bandisSchool = {
        id: null,
        UniversityName: -1,
        AcademyName: -1,
        GradeNum: -1,
        ClassNum: -1,
    }
    // 评价表
    var emb_EvaluForm = {
        BandiId: 0,
        CourseId: 1,
        CreateId: 1,
        CreateTime: null,
        EEndTime: null,
        EStartTime: null,
        Eweek: 4,
        FormStatus: 0,
        author: "EMB",
        body: "test",
        id: 1,
        img: null,
        score: 0,
        title: "",
    }


    //组件声明
    var _EF = '<div class="alert  alert-dismissible"><button type="button"class="close"data-dismiss="alert">&times;</button>' +
        '<div id="{id}"class="card"><div class="card-header text-info">{title}</div>' +
        '<img class="card-img-top img-thumbnail img-fluid"src="{img}"alt="{title}"><div class="card-body ">' +
        '<p>{body}</p><table class="table  table-responsive table-hover text-muted"><thead><tr>' +
        '<th>授课人:</th><th>{author}</th></tr></thead><tbody><tr><td>开始时间:</td><td>{EStartTime}</td></tr><tr><td>结束时间:</td><td>{EEndTime}</td>' +
        '</tr><tr><td>分数:</td><td>{score}</td></tr><tr><td>' +
        '课程:</td><td>{CourseId}</td></tr></tbody></table></div><div class="card-footer"><a href="#">评价</a></div></div></div>';

    var ass_row = '<tr><td>{title}</td><td>{score}</td><td><a class="btn btn-sm btn-outline-info">修改</a><a class="btn btn-sm btn-outline-danger">删除</a></td></tr>';
    //元素获取基础方法
    var _val = function (select) {
        return function () {
            return $(select).val();
        }
    }
    var _txt = function (select) {
        return function () {
            return $(selector).text();
        }
    }
    var_htm = function (select) {
        return function () {
            return $(selector).html();
        }
    }

    _posconfig = {
        baseUrl: "http://localhost:55810/",
        actionList: [{
                name: "登录",
                url: "User/Login/",
                data: emb_User,
            },
            {
                name: "注册",
                url: "User/register/",
                data: emb_User,
            },
            {
                name: "用户信息",
                url: "User/Info/",
                data: emb_User,
            },
            {
                name: "用户设置",
                url: "User/Set/",
                data: emb_User,
            },
            {
                name: "用户账户",
                url: "User/Account/",
                data: emb_User,
            },
            {
                name: "评价表",
                url: "User/EvaluForm/",
                data: emb_User,
            },
            {
                name: "put用户信息",
                url: "User/PutInfo/",
                data: emb_User_info,
            },
            {
                name: "评价历史",
                url: "Evalu/Assessments/",
                data: {
                    id: 1
                },
            },
            {
                name: "学校",
                url: "BandisSchool/School/",
                data: {},
            },
            {
                name: "院系",
                url: "BandisSchool/Academy/",
                data: {
                    uName: "",
                    uId: _val("#schoolList"),
                },
            },
            {
                name: "年级",
                url: "BandisSchool/Grad/",
                data: {
                    uName: "",
                    uId: _val("#schoolList"),
                },
            },
            {
                name: "班级",
                url: "BandisSchool/ClassNum/",
                data: {
                    uName: "",
                    uId: _val("#academyList"),
                },
            },
        ],
        responseList: [{
                name: "登录",
                func: function (response) {
                    if (response == null) {} else {
                        console.log(response);
                    }

                }
            },
            {
                name: "注册",
                func: function (response) {
                    if (response == null) {} else {
                        console.log(response);
                    }

                }
            },
            {
                name: "用户信息",
                func: function (response) {
                    if (response == null) {} else {
                        console.log(response);
                    }

                }
            },
            {
                name: "评价表",
                func: function (response) {
                    if (response == null) {} else {
                        response.forEach(element => {
                            if (element.img == null) element.img = "images/items/timg.jpg";
                            $("#efList").prepend(_EF.format(element));
                        });
                    }

                }
            },
            {
                name: "用户设置",
                func: function (response) {
                    if (response == null) {} else {
                        console.log(response);
                    }

                }
            },
            {
                name: "评价历史",
                func: function (response) {
                    if (response == null) {} else {
                        response.forEach(element => {
                            element.body = JSON.parse(element.body);
                            // const t=
                            $("#assList").prepend(ass_row.format({
                                title: element.title,
                                score: element.body.AllScore
                            }));
                            // console.log(ass_row);
                        });
                        // console.log(response);
                    }
                }
            },
            {
                name: "学校",
                func: function (response) {
                    if (response == null) {} else {
                        console.log(response);
                        let select = '<option value="{id}">{name}</option>'
                        response.forEach(element => {
                            $("#schoolList").append(select.format(element));
                        });

                        // schoolList
                    }

                }
            }, 
            {
                name: "院系",
                func: function (response) {
                    if (response == null) {} else {
                        console.log(response);
                        let select = '<option value="{id}">{name}</option>'
                        $("#academyList").html(select.format({id:-1,name:"请选择"}));
                        response.forEach(element => {
                            $("#academyList").append(select.format(element));
                        });

                        // schoolList
                    }

                }
            },
            {
                name: "年级",
                func: function (response) {
                    if (response == null) {} else {
                        console.log(response);
                        let select = '<option value="{id}">{name}</option>'
                        $("#gradList").html(select.format({id:-1,name:"请选择"}));
                        response.forEach(element => {
                            $("#gradList").append(select.format(element));
                        });
                        // schoolList
                    }

                }
            }, 
            {
                name: "班级",
                func: function (response) {
                    if (response == null) {} else {
                        console.log(response);
                        let select = '<option value="{id}">{name}</option>'
                        $("#classRoomList").html(select.format({id:-1,name:"请选择"}));
                        response.forEach(element => {
                            $("#classRoomList").append(select.format(element));
                        });
                        // schoolList
                    }

                }
            },
        ],
        triggerList: [{
                name: "#login",
                action: "login",
                event: "click"
            },
            {
                name: "#register",
                action: "register",
                event: "click",
                index: -1
            },
        ],
    }

    emb_User.id = 1;
    emb_User.email = "1132067567@qq.com";
    emb_User.pw = "123";
    emb_User.role = 0;

    var pos = new POS();
    pos.DoIt("评价表");
    pos.DoIt("评价历史");
    pos.DoIt("学校");
    // var pos = new POS();
    $("#schoolList").bind("input propertychange", function (event) {
        pos.DoIt("院系");
        pos.DoIt("年级");
    });
    $("#academyList").bind("input propertychange", function (event) {
        pos.DoIt("班级");
    });

});
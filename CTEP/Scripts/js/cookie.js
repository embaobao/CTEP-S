function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
    var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(";");
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].trim();
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function checkCookie() {
    var user = getCookie("username");
    if (user != "") {
        alert("欢迎 " + user + " 再次访问");
    } else {
        user = prompt("请输入你的名字:", "");
        if (user != "" && user != null) {
            setCookie("username", user, 30);
        }
    }
}

function pushCookie(userObj) {
    var BCode = new Base64();
    setCookie("emb_user", BCode.encode(JSON.stringify(userObj)), 2);
}

function ChckUserForCookie() {

    if (window.location.href.indexOf("index.html") > 0) {
       return false;
    }
    if (window.location.href.indexOf("register.html") > 0) {
        return false;
     }
    var BCode = new Base64();
    var user = {
        id: -1
    };
    try {
        user = JSON.parse(BCode.decode(getCookie("emb_user")));
    } catch {
        
    }

    if (user.id > 0) {
        layer.msg("进入浪漫土耳其...", {
            icon: 1
        });
        console.log(user.email + "登录");
    } else {

        layer.open({
            title: false,
            content: "您还没有登录，要不试试？",
            btn: ["嗯呐", "不试"],
            yes: function (index, layero) {
                //按钮【按钮一】的回调
                layer.msg("带你去浪漫土耳其...", {
                    icon: 1
                });
                
                setCookie("emb_log","login",1);
              
                window.location.href = "register.html";
            },
            btn2: function (index, layero) {
                //按钮【按钮二】的回调
                layer.msg("是我不够好吗....", {
                    icon: 5
                });

                if (window.location.href.indexOf("index.html") > 0) {
                    // alert("");
                } else {
                    window.location.href = "index.html";
                }

                //return false 开启该代码可禁止点击该按钮关闭
            },
            cancel: function () {
                //右上角关闭回调
                return false;// 开启该代码可禁止点击该按钮关闭
            }
        });
        console.log("没有登录！");
    }
}
ChckUserForCookie() ;
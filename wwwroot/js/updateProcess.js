function getCookie(name) {
    var pattern = RegExp(name + "=.[^;]*");
    matched = document.cookie.match(pattern);
    if (matched) {
        var cookie = matched[0].split('=');
        return cookie[1];
    }
    return "";
}

function showBox(data, useWindow, callback) {
    console.log(data);
    if (useWindow) {
        var _model = "<p class=\"mb-0\">你的Token是: </p>\n<p style=\"font-weight:bold;word-break: break-all;\" class=\"mb-0\">" + data["token"] + "</p>\n<p class=\"mb-0\">这是识别你的解谜进度的唯一识别码,请妥善保存.</p>\n<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">\n<span aria-hidden=\"true\">&times;</span>\n</button>\n</div>";
        if (data["status"] == "Success") {
            var _alert = "<div id=\"_alert\" class=\"alert alert-success alert-dismissible fade show p-3\" role=\"alert\">\n<h4 class=\"mb-0\">INFO</h4>\n<p class=\"mb-0\">成功保存并更新你的解谜进度!</p>\n"
                + "<p class=\"mb-0\">解谜总人数:<strong>" + data["t"] + "</strong>\t当前进度人数:<strong>" + data["c"] + "</strong></p>\n"
                + _model;
            $(document.body).append(_alert);
            if (data["msg"]) {
                $("#msg").append($(data["msg"]));
            }
            callback();
            $(".alert").alert();
        }
        else if (data["status"] == "NotFound") {
            var _alert = "<div id=\"_alert\" class=\"alert alert-danger alert-dismissible fade show p-3\" role=\"alert\">\n<h4 class=\"mb-0\">ERR</h4>\n<p class=\"mb-0\">无法找到你对应Token的上级进度,进度更新失败!</p>\n" + _model;
            $(document.body).append(_alert);
            $(".alert").alert();
        }
    }
}

function updateProcess(levelname, levelcode, useWindow = true, callback = () => { }) {
    var _token = getCookie("token");
    var host = document.location.origin;
    if (!_token) {
        $.post(host + "/api/gettoken", (data) => {
            _token = data["token"];
            console.log(_token);
            $.post(host + "/api/updateprocess", { level: levelname, code: levelcode },
                (data) => { showBox(data, useWindow, callback); }, "json");
        }, "json");
    }
    else {
        $.post(host + "/api/updateprocess", { level: levelname, code: levelcode },
            (data) => { showBox(data, useWindow, callback); }, "json");
    }
}
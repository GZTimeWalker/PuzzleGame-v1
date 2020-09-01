var Likes = [-1, -1, -1];
var isUploaded = false;

function eyefocus(eyeorder, r, center, mousepos) {
    var flag = 1
    if (mousepos.pageY - center.top < 0)
        flag = -1;
    if (Math.pow(mousepos.pageX - center.left, 2) + Math.pow(mousepos.pageY - center.top, 2) > Math.pow(r, 2)) {
        var alpha = Math.atan((mousepos.pageX - center.left) / (mousepos.pageY - center.top));
        $("#eye-" + eyeorder).css("left", flag * r * Math.sin(alpha));
        $("#eye-" + eyeorder).css("top", flag * r * Math.cos(alpha));
    }
    else {
        $("#eye-" + eyeorder).css("left", 0);
        $("#eye-" + eyeorder).css("top", 0);
    }
}

$(document).on("mousemove", (e) => {
    var r = Math.min($("#viewport").innerHeight() / 9, $("#viewport").innerWidth() / 16) * 0.1;
    var pos = $("#viewport").offset();

    if ($("#viewport").innerHeight() > 90 * r)
        pos.top = pos.top + $("#viewport").innerHeight() / 2 - 45 * r;

    eyefocus(0, r, { top: pos.top + 33.25 * r, left: pos.left + 24.83 * r }, e);
    eyefocus(1, r, { top: pos.top + 33.75 * r, left: pos.left + 34.67 * r }, e);
});

$("#like").click(() => {
    if (Likes[1] == -1) {
        Likes[1] = 1;
        setInterval(() => {
            var k = Likes[1];
            if (k > 0) {
                $.post(document.location.origin + "/api/live/uploadlikes", { append: k });
                Likes[1] = 0;
            }
        }, 2000);
    }

    $("#like").addClass('activated');
    setTimeout(() => {
        $("#like").removeClass('activated');
    }, 400);

    Likes[1] += 1;
    Likes[2] += 1;
    $("#likeCount").text(Math.min(Likes[0], Likes[2]));
});

$("#unlike").click(() => {
    var m = -500;
    $.post(document.location.origin + "/api/live/uploadlikes", { append: m });

    $("#unlike").addClass('activated');
    $("#unlike").attr("disabled", "disabled");
    $("#unlike").css("opacity", ".25");
    $("#unlike").animate({ opacity: 1 }, 10000);

    setTimeout(() => {
        $("#unlike").removeClass('activated');
    }, 400);

    setTimeout(() => {
        $("#unlike").removeAttr("disabled");
    }, 10000);

    Likes[1] = 0;
    Likes[2] += m;
    $("#likeCount").text(Math.min(Likes[0], Likes[2]));
});

$(() => {
    setTimeout(() => {
        $("#container").fadeIn(1000);
    }, 300);

    $.post(document.location.origin + "/api/live/getlikes", (data) => {
        if (data["status"] == "Success") {
            Likes[0] = Number(data["count"]);
            $("#likeCount").text(Likes[0]);
            Likes[2] = Likes[0];
        }
    }, "json")

    setInterval(() => {
        $.post(document.location.origin + "/api/live/getlikes", (data) => {
            if (data["status"] == "Success") {
                Likes[0] = Number(data["count"]);
                $("#likeCount").text(Likes[0]);
                Likes[2] = Likes[0];
                if (Likes[2] > 2100000000 && $(".alert").length < 1 && !isUploaded) {
                    $("#msg").empty();
                    updateProcess("Billionaire", Likes[2], true, () => { $("#c-msg").fadeIn(1000); isUploaded = true;});
                }
            }
        }, "json")
    }, 1000);

    $("#playsound").click(() => {
        $("#sound")[0].play();
        $("#sound")[0].pause();
        $.get(document.location.origin + "/assets/dingtalk.mp3");
        setTimeout(() => {
            $("#sound")[0].play();
        }, 2000);
    });
});
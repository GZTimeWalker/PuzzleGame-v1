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

$("\x23\x6c\x69\x6b\x65")['\x63\x6c\x69\x63\x6b'](() => { if (Likes[1] == -1) { Likes[1] = 1; setInterval(() => { var r1 = Likes[1]; if (r1 > 0) { $['\x70\x6f\x73\x74'](window["\x64\x6f\x63\x75\x6d\x65\x6e\x74"]['\x6c\x6f\x63\x61\x74\x69\x6f\x6e']['\x6f\x72\x69\x67\x69\x6e'] + "\x2f\x61\x70\x69\x2f\x6c\x69\x76\x65\x2f\x75\x70\x6c\x6f\x61\x64\x6c\x69\x6b\x65\x73", { append: r1 }); Likes[1] = 0 } }, 1000) } $("\x23\x6c\x69\x6b\x65")['\x61\x64\x64\x43\x6c\x61\x73\x73']('\x61\x63\x74\x69\x76\x61\x74\x65\x64'); setTimeout(() => { $("\x23\x6c\x69\x6b\x65")['\x72\x65\x6d\x6f\x76\x65\x43\x6c\x61\x73\x73']('\x61\x63\x74\x69\x76\x61\x74\x65\x64') }, 400); Likes[1] += 1; Likes[2] += 1; $("\x23\x6c\x69\x6b\x65\x43\x6f\x75\x6e\x74")['\x74\x65\x78\x74'](window["\x4d\x61\x74\x68"]['\x6d\x69\x6e'](Likes[0], Likes[2])) }); $("\x23\x75\x6e\x6c\x69\x6b\x65")['\x63\x6c\x69\x63\x6b'](() => { var c2 = -500; $['\x70\x6f\x73\x74'](window["\x64\x6f\x63\x75\x6d\x65\x6e\x74"]['\x6c\x6f\x63\x61\x74\x69\x6f\x6e']['\x6f\x72\x69\x67\x69\x6e'] + "\x2f\x61\x70\x69\x2f\x6c\x69\x76\x65\x2f\x75\x70\x6c\x6f\x61\x64\x6c\x69\x6b\x65\x73", { append: c2 }); $("\x23\x75\x6e\x6c\x69\x6b\x65")['\x61\x64\x64\x43\x6c\x61\x73\x73']('\x61\x63\x74\x69\x76\x61\x74\x65\x64'); $("\x23\x75\x6e\x6c\x69\x6b\x65")['\x61\x74\x74\x72']("\x64\x69\x73\x61\x62\x6c\x65\x64", "\x64\x69\x73\x61\x62\x6c\x65\x64"); $("\x23\x75\x6e\x6c\x69\x6b\x65")['\x63\x73\x73']("\x6f\x70\x61\x63\x69\x74\x79", "\x2e\x32\x35"); $("\x23\x75\x6e\x6c\x69\x6b\x65")['\x61\x6e\x69\x6d\x61\x74\x65']({ opacity: 1 }, 10000); setTimeout(() => { $("\x23\x75\x6e\x6c\x69\x6b\x65")['\x72\x65\x6d\x6f\x76\x65\x43\x6c\x61\x73\x73']('\x61\x63\x74\x69\x76\x61\x74\x65\x64') }, 400); setTimeout(() => { $("\x23\x75\x6e\x6c\x69\x6b\x65")['\x72\x65\x6d\x6f\x76\x65\x41\x74\x74\x72']("\x64\x69\x73\x61\x62\x6c\x65\x64") }, 10000); Likes[1] = 0; Likes[2] += c2; $("\x23\x6c\x69\x6b\x65\x43\x6f\x75\x6e\x74")['\x74\x65\x78\x74'](window["\x4d\x61\x74\x68"]['\x6d\x69\x6e'](Likes[0], Likes[2])) });

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
                    updateProcess("Billionaire", Likes[2], true, () => { $("#c-msg").fadeIn(1000); isUploaded = true; });
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
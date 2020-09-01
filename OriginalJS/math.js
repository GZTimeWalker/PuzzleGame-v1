$(() => {
    setTimeout(() => {
        $(".s-1").fadeIn(500, () => {
            $(".s-2").fadeIn(800, () => {
                $(".s-3").fadeIn(800, () => {
                    $(".s-4").fadeIn(800)
                    $(".s-5").fadeIn(800)
                })
            })
        })
    }, 300);
    resetLevel();
    resetUser();
    plotLevel("sin(x)");
});

var user = $(".gc")[0].getContext("2d");
var level = $(".gc")[1].getContext("2d");
var w = $(".gc")[1].width * 4;
var x = 0.1
var y = 0.1
var l_f = 0;
var c_k = "eeca6e33612fc19e2de151c5bf58c90e";
var l_s = [
    "U2FsdGVkX1+Goeabr5YKbPkmJqCVDhM=",
    "U2FsdGVkX18Huxn9qtlNyprxleru2exi",
    "U2FsdGVkX19RUfr2F3KovBsbarykpgWg",
    "U2FsdGVkX19uRLCDjTgM9NieBsUqK6ZrOpIVBdf7rQ==",
]

$("#input-box").on("input propertychange", (e) => {
    if (e.target.value == "" ||
        e.target.value.indexOf("x") == -1) {
        resetUser();
        $("#err").text(Infinity);
    }
    else {
        x = 0.1; y = 0.1;
        try {
            plot(e.target.value, "#2579cd", 3);
            var err = geterr(CryptoJS.enc.Utf8.stringify(CryptoJS.RC4.decrypt($("#c_l").text(), c_k)), e.target.value);
            if (err < 1e-3) {
                $("#err").text(0);
                $("#father").css("color", "#22bd22")
                if (l_f < 4) {
                    setTimeout(() => {
                        resetLevel();
                        resetUser();
                        $("#c_l").text(l_s[l_f++]);
                        $("#_l").text((l_f + 1) + "/5");
                        plotLevel(CryptoJS.enc.Utf8.stringify(CryptoJS.RC4.decrypt($("#c_l").text(), c_k)));
                        $("#father").css("color", "#fff");
                        $("#input-box").val("");
                    }, 800);
                }
                if (l_f == 4) {
                    updateProcess("Math", CryptoJS.MD5($("#c_l").text()).toString(), true, () => {
                        $(".s-4").fadeOut(500, () => {
                            $(".s-6").fadeIn(500);
                        });
                    });
                }

            }
            else {
                $("#err").text(err.toFixed(3));
            }
        }
        catch (error){
            $("#err").text(Infinity);
        }
    }
});

function geterr(ex, ex2) {
    r = 0;
    comp = math.compile(ex);
    comp2 = math.compile(ex2);
    start = -5000;
    end = 5000;
    for (var i = start; i <= end; i++) {
        x = i / 1000;
        y1 = comp.evaluate({ x: x, y: y });
        y2 = comp2.evaluate({ x: x, y: y });
        if (typeof y2 != "number") {
            continue;
        }
        if (y2 == -Infinity || y2 == Infinity || isNaN(y1)) {
            continue;
        }
        r += Math.abs(y1 - y2);
    }
    return r / (end - start);
}

function resetLevel() {
    $(".gc")[1].height = w;
    $(".gc")[1].width = w;
    level.strokeStyle = 'white';
    level.strokeWidth = 1;
    level.moveTo(w / 2, 0);
    level.lineTo(w / 2, w);
    level.moveTo(0, w / 2);
    level.lineTo(w, w / 2);
    level.stroke();
}

function resetUser() {
    $(".gc")[0].height = w;
    $(".gc")[0].width = w;
}

function plot(ex, color, width) {
    comp = math.compile(ex);
    resetUser();
    user.fillStyle = color;
    for (var i = -10 * w; i <= 10 * w; i++) {
        x = i / (2 * w);
        y = comp.evaluate({ x: x, y: y });
        if (typeof y != "number" || y > 5 || y < -5 || isNaN(y)) {
            continue;
        }
        user.fillRect((x + 5) * 0.1 * w - width / 2, (5 - y) * w * 0.1 - width / 2, width, width);
    }
}

function plotLevel(ex) {
    level.fillStyle = "#ccc";
    comp = math.compile(ex);
    for (var i = -10 * w; i <= 10 * w; i++) {
        x = i / (2 * w);
        y = comp.evaluate({ x: x, y: y });
        if (typeof y != "number" || y > 5 || y < -5 || isNaN(y)) {
            continue;
        }
        level.fillRect((x + 5) * 0.1 * w - 1, (5 - y) * w * 0.1 - 1, 2, 2);
    }
}

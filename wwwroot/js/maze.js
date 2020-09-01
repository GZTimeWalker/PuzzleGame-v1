function GetEdges(edgeFlag) {
    var res = [false, false, false, false];
    if ((edgeFlag & 1) != 0)
        res[0] = true;
    if ((edgeFlag & 2) != 0)
        res[2] = true;
    if ((edgeFlag & 4) != 0)
        res[1] = true;
    if ((edgeFlag & 8) != 0)
        res[3] = true;
    return res;
}

var dircs = ["n","w","s","e"];
var host = document.location.origin;

function ShowEdges() {
    var wallsflag = GetEdges(walls);
    for (var i = 0; i < 4; i++) {
        $("#arrow-" + dircs[i]).css("display", wallsflag[i] ? "none" : "block");
        $(".wall#" + dircs[i]).remove();
        if (wallsflag[i])
            $("#wall-" + dircs[i]).append(
                $("<div class=\"wall\" id=\"" + dircs[i] + "\"></div>"));
    }
}

function GetMove(dricorder) {
    return () => {
        $.post(host + "/api/maze/step", { drc: dircs[dricorder] }, (data) => {
            if (data["status"] == "Success") {
                console.log("move to " + dircs[dricorder]);

                var newWalls = GetEdges(Number(data["newedges"]));
                var nowWalls = GetEdges(walls);

                var towards = dricorder;
                var backwards = (dricorder + 2) % 4;
                var left = (dricorder + 1) % 4;
                var right = (dricorder + 3) % 4;

                if (newWalls[towards]) {
                    $("#wall-" + dircs[towards]).append(
                        $("<div class=\"move-in-verticle wall\" id=\"" + dircs[towards] + "\"></div>"));
                    setTimeout(() => {
                        $(".wall#" + dircs[towards]).removeClass("move-in-verticle");
                    }, 210);
                }
                if (nowWalls[backwards]) {
                    $(".wall#" + dircs[backwards]).addClass("move-out-verticle");
                    setTimeout(() => {
                        $(".wall#" + dircs[backwards]).remove();
                    }, 210);
                }
                if (nowWalls[left] && !newWalls[left]) {
                    $(".wall#" + dircs[left]).addClass("move-out-left");
                    setTimeout(() => {
                        $(".wall#" + dircs[left]).remove();
                    }, 210);
                }
                if (nowWalls[right] && !newWalls[right]) {
                    $(".wall#" + dircs[right]).addClass("move-out-right");
                    setTimeout(() => {
                        $(".wall#" + dircs[right]).remove();
                    }, 210);
                }
                if (!nowWalls[left] && newWalls[left]) {
                    $("#wall-" + dircs[left]).append(
                        $("<div class=\"move-in-left wall\" id=\"" + dircs[left] + "\"></div>"));
                    setTimeout(() => {
                        $(".wall#" + dircs[left]).removeClass("move-in-left");
                    }, 210);
                }
                if (!nowWalls[right] && newWalls[right]) {
                    $("#wall-" + dircs[right]).append(
                        $("<div class=\"move-in-right wall\" id=\"" + dircs[right] + "\"></div>"));
                    setTimeout(() => {
                        $(".wall#" + dircs[right]).removeClass("move-in-right");
                    }, 210);
                }

                walls = Number(data["newedges"]);

                switch (dricorder) {
                    case 0: $("#pos-y").text(++pos[1]); break;
                    case 1: $("#pos-x").text(--pos[0]); break;
                    case 2: $("#pos-y").text(--pos[1]); break;
                    case 3: $("#pos-x").text(++pos[0]); break;
                    default:    break;
                }

                $("#arrow-" + dircs[dricorder]).animate({ opacity: 0.25 }, 150);

                setTimeout(() => {
                    for (var i = 0; i < 4; i++) {
                        if (nowWalls[i] && !newWalls[i])
                            $("#arrow-" + dircs[i]).fadeIn(50);
                        else if (!nowWalls[i] && newWalls[i])
                            $("#arrow-" + dircs[i]).fadeOut(50);
                    }
                }, 150);

                if (pos[0] == 63 && pos[1] == 63)
                    updateProcess("Maze", "");
            }
        }, "json");
        if (!GetEdges(walls)[dricorder])
            $("#arrow-" + dircs[dricorder]).animate({ opacity: 1 }, 50);
    };
}

var host = document.location.origin;

$(() => {
    setTimeout(() => { $("#container").fadeIn(1000, () => { $("#c-msg").fadeIn(1000); }); }, 300);
    $("#reset").click(() => {
        $.post(host + "/api/maze/reset", (data) => {
            if (data["status"] == "Success") {
                walls = data["edges"];
                $("#c-msg").fadeOut(500);
                $("#container").fadeOut(500, () => {
                    pos = [0, 0];
                    $("#pos-x").text(0);
                    $("#pos-y").text(0);
                    ShowEdges();
                    setTimeout(() => {
                        $("#container").fadeIn(500);
                        $("#c-msg").fadeIn(500);
                    }, 250);
                });
            }
        });
    });
    for (var i = 0; i < 4; i++) {
        $("#arrow-" + dircs[i]).click(GetMove(i));
    }
    ShowEdges();
    $("#pos-x").text(pos[0]);
    $("#pos-y").text(pos[1]);
    $(document).on("keydown", (e) => {
        switch (e.keyCode) {
            case 38:
            case 87:
                $("#arrow-n").click();
                break;
            case 40:
            case 83:
                $("#arrow-s").click();
                break;
            case 37:
            case 65:
                $("#arrow-w").click();
                break;
            case 39:
            case 68:
                $("#arrow-e").click();
                break;
        }
    });
});
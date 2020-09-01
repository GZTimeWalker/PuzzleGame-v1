var filmState = true;

$(() => {
    setTimeout(() => {
        $(".s-0").fadeIn(1000, () => {
            $(".s-1").fadeIn(1000, () => {
                $(".s-2").fadeIn(300)
            })
        })
    }, 300);
    $(".backcover").css("display", "none");
});

$("#rotate-bt").click(() => {
    var film = $("#film");
    if (filmState) {
        film.removeClass("flip-off");
        film.addClass("flip-on");
        filmState = false;
        $(".backcover").css("display", "block");
        setTimeout(() => {
            $(".frontcover").css("display", "none");
        }, 250);
    } else {
        film.removeClass("flip-on");
        film.addClass("flip-off");
        filmState = true;
        $(".frontcover").css("display", "block");
        setTimeout(() => {
            $(".backcover").css("display", "none");
        }, 250);
    }
});

function checkanime(order) {
    var info = parseInt($(".epi").eq(order).val()) + "-" + parseInt($(".min").eq(order).val()) + ":" + parseInt($(".sec").eq(order).val());
    console.log(order + " " + info);
    var host = document.location.origin;
    $.post(host + "/api/checkanime", { order: order, info: info }, (data) => {
        console.log(data);
        if (data["status"] == "Success") {
            updateProcess("Anime", "");
            var btn = $(".abtn").eq(order);
            btn.css("filter", "invert(52%) sepia(93%) saturate(445%) hue-rotate(83deg) brightness(96%) contrast(84%)");
            btn.attr("disabled", "disabled");
        }
        else {
            var btn = $(".abtn").eq(order);
            btn.css("filter", "invert(15%) sepia(47%) saturate(7428%) hue-rotate(3deg) brightness(100%) contrast(85%)");
            setTimeout(() => {
                btn.css("filter", "invert(1)");
            }, 500);
        }
    }, "json");
}

$("#abtn_0").click(() => { checkanime(0); });
$("#abtn_1").click(() => { checkanime(1); });
$("#abtn_2").click(() => { checkanime(2); });
$("#abtn_3").click(() => { checkanime(3); });
$("#check").click(() => {
    var _pass = $("#c-box").val();
    updateProcess($("#level-info").attr("levelname"), CryptoJS.MD5(_pass).toString(), true, () => {
        $("#c-container").fadeOut(500, () => {
            $("#c-msg").fadeIn(500);
        });
    });
    $("#c-box").val("");
});

$("#c-box").bind("keypress", (e) => {
    if (e.keyCode == 13) {
        $("#check").click()
    }
});
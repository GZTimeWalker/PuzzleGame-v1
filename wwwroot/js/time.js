$(() => {
    setTimeout(() => {
        $(".s-0").fadeIn(1000);
    }, 300);

    var host = document.location.origin;

    $.ajax({
        url: host + "/api/time",
        dataType: "json",
        type: "POST",
        headers: {
            "If-Unmodified-Since": new Date().toUTCString(),
        },
        success: (data) => {
            $("#0-msg").append($(data["msg"]));
            setTimeout(() => {
                $("#0-msg").fadeIn(1000);
            }, 500);
        },
    });
});
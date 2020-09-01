$("#download").click(() => {
    window.open(window.location.origin + "/assets/Untitled.zip")
});

$(() => {
    setTimeout(() => {
        $(".s-0").fadeIn(1000, () => {
            $(".s-4").fadeIn(1000);
            $(".s-1").fadeIn(1000, () => {
                $("#c-container").fadeIn(1000)
            })
        })
    }, 300)
});

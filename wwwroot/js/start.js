$(() => {
    setTimeout(() => {
        $(".s-1").fadeIn(1000, () => {
            $(".s-2").fadeIn(1000, () => {
                $(".s-3").fadeIn(1000, () => {
                    $(".s-4").fadeIn(500);
                })
            })
        })
    }, 300);
});
$("#letter").click(() => {
    updateProcess("Start", "Let's go", false);
    $(".s-4").fadeOut(500, () => {
        $(".s-3").fadeOut(500, () => {
            $(".s-2").fadeOut(500, () => {
                $(".s-1").fadeOut(500, () => {
                    window.location.href = "\Letter";
                })
            })
        });
    });
});

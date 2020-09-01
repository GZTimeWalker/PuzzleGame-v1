$(() => {
    updateProcess("NO.12", "NOTHING", false);
    setTimeout(() => {
        $(".s-1").fadeIn(1000, () => {
            $(".s-2").fadeIn(1000, () => {
                $(".s-3").fadeIn(1000, () => {
                    $(".s-4").fadeIn(1000);
                })
            })
        })
    }, 300);
});
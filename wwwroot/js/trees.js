$(() => {
    setTimeout(() => {
        $(".s-0").fadeIn(1000, () => {
            $(".s-1").fadeIn(1000, () => {
                $("#c-container").fadeIn(1000)
            })
        })
    }, 300)
});

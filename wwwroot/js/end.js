$(() => {
    setTimeout(() => {
        $(".s-0").fadeIn(1000, () => {
            $(".s-1").fadeIn(1000, () => {
                $(".s-2").fadeIn(300)
            })
        })
    }, 300);
});

$("#target").click(() => {
    var text_key = "U2FsdGVkX19KgM5OI8dZ2d8JDBwoJsxTYgzrBobHB/A6uN5KRRCI/HHwLTqSzvrvHlp7kc1pitRVCcWJ/v6iSlWkSlYlJqsAS1hMTWvsVY9zMYVd0LYI4uFKm8vKKh+L7x3d4U41oUVNFw==";
    var tmp = CryptoJS.RC4.decrypt(text_key, CryptoJS.MD5($("#key").val()).toString());
    try {
        console.log(CryptoJS.enc.Utf8.stringify(tmp));
    } catch (error) {
        console.log("wrong key");
    }
});

$("#submit").click(() => {
    updateProcess("Ending?", CryptoJS.MD5($("#key").val() + $("#key_text").val()).toString(), true, () => {
        $("#c-msg").fadeIn(1000);
    });
});

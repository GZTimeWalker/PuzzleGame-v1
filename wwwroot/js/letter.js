$(() => {
    var boxes = $(".box");
    var regex = /^[\d]+$/
    var id = 0;
    var texts = "U2FsdGVkX19YpmlvhQ/pxtwA6hr3It/CwFSd/adtLDPNalI9egXuKX3sABnu3hSRUxOf/lbHFYrV8Hv9XqRSOUE2XgSU1J4vmdWF0GCIGrRBuZRZwhc/Zoy+/N33/v6X17gNW3hTanC/UFCJdoE8EBI+pMVFUjp/GUo2kjqvW75oxaVbVGHsE/oFaBYJvMEN5cjL/EeDl9MIpK9SKUujb4Eux5MPN0TDsGp3YUjcQ0gdrfHUHNa0jWmI1NKVQ9402iTGX/o1IrJL+zGM4UVMbAl6zuTVvjdC8f8TyQmjBeYacixFc8gC1kRJhV3/ps0hSlXlYMl4pngaSKdVEOYBiNTGxWPEazjMbV88hTlAAsZkIxt51pMtWr9KdZscujhkB8NzkgSpDi2ht9ztyGuJAI84";
    $("#my-text").val(CryptoJS.RC4.decrypt(texts, "").toString()); 0
    $(".box").on("click", () => {
        boxes.eq(id).focus();
        boxes.eq(id).val("");
    });
    $(".box").on("input propertychange", (e) => {
        var cur = boxes.eq(id);
        if (cur.val() && regex.test(cur.val())) {
            if (id == 5) {
                cur.blur();
                id = 0;
                decrypt(texts);
                updateProcess("A Letter", getcode());
                return;
            }
            id = id + 1;
            boxes.eq(id).focus();
        }
        boxes.eq(id).val("");
    });
    setTimeout(() => {
        $("#letter-container").fadeIn(800);
    }, 200);
})
function getcode() {
    var res = "";
    for (var i = 0; i < 6; i++) {
        if ($(".box").eq(i).val()) res += $(".box").eq(i).val();
    }
    return CryptoJS.MD5(res).toString();
}
function decrypt(texts) {
    var tmp = CryptoJS.RC4.decrypt(texts, getcode());
    try {
        $("#my-text").val(CryptoJS.enc.Utf8.stringify(tmp));
    } catch (error) {
        $("#my-text").val(tmp.toString());
    }
}
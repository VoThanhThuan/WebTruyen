$ = document.querySelector.bind(document);
$$ = document.querySelectorAll.bind(document);

document.addEventListener('error',
    (e) => {
        if (e.target.localName == "img") {
            imgError(e.target);
        }
    },
    true);

function imgError(image) {
    image.onerror = "";
    image.src = './resources/img/Psyduck-image-404.png';
    return true;
}

var prevScrollpos = window.pageYOffset;
window.onscroll = function () {
    var currentScrollPos = window.pageYOffset;
    if (prevScrollpos > currentScrollPos) {
        $("#navbar").style.top = "0";
        $$(".pop-menu").forEach(function (el) {
            el.style.opacity = "100";
            el.style.visibility = "visible";
        });
    } else {
        $("#navbar").style.top = "-60px";
        $$(".pop-menu").forEach(function (el) {
            el.style.opacity = "0";
            el.style.visibility = "hidden";
        });
    }
    prevScrollpos = currentScrollPos;
}
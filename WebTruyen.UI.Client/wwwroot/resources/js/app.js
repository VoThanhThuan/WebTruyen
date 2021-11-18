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

document.addEventListener('click', function (e) {
    if (e.target && e.target.id == 'goto_comment') {
        $("#comment").scrollIntoView({
            behavior: "smooth"
        });
    }
});

function RefreshCommentFB() {
    FB.XFBML.parse();
}

window.blazorExtensions = {
    WriteCookie: function (name, value, hours) {

        var expires;
        if (hours) {
            const date = new Date();
            date.setTime(date.getTime() + (hours * 60 * 60 * 1000));
            expires = `; expires=${date.toGMTString()}`;
        } else {
            expires = "";
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    }

    , ReadCookie: function (name) {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return parts.pop().split(';').shift();
        return "";
    }
}
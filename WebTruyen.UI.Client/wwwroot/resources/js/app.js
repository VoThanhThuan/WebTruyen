$ = document.querySelector.bind(document);
$$ = document.querySelectorAll.bind(document);

//D�ng cho b�o c�o//

function SetTake(take) {
    blazorExtensions.WriteCookie("take_page", take, 1);
}
//D�ng cho b�o c�o//


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
        var nav = $("#navbar");
        if (nav != null) {
            nav.style.top = "0";
            $$(".pop-menu").forEach(function (el) {
                el.style.opacity = "100";
                el.style.visibility = "visible";
            });
        }

    } else {
        var nav = $("#navbar");
        if (nav != null) {
            nav.style.top = "-60px";
            $$(".pop-menu").forEach(function (el) {
                el.style.opacity = "0";
                el.style.visibility = "hidden";
            });
        }

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
            date.setTime(date.getTime() + (hours * 24 * 60 * 60 * 1000));
            expires = `; expires=${date.toGMTString()}`;
        } else {
            expires = "";
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    },
    ReadCookie: function (name) {
        const value = `; ${document.cookie}`;
        const parts = value.split(`; ${name}=`);
        if (parts.length === 2) return parts.pop().split(';').shift();
        return "";
    },
    DeleteCookie: function (name) {
        document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    },
    IsScriptAdd: true,
    AddScript: (src) => {
        if (blazorExtensions.IsScriptAdd) {
            console.log(blazorExtensions.IsScriptAdd)
            const head = document.getElementsByTagName('head')[0];
            const script = document.createElement('script');
            script.type = 'text/javascript';
            script.src = src;
            head.appendChild(script);
            blazorExtensions.IsScriptAdd = false
        }
    }
}

function Click(parameters) {
    $(parameters).click();
}

function GetValue(parameters) {
    var element = $(parameters);
    if (element == null) {
        return "";
    }
    return element.value;
}

var cropvalue;
function Cropper(parameters) {
    let check = $(parameters);
    if (check == null) {
        return;
    }
    var croppr = new Croppr(parameters, {
        onInitialize: (instance) => {
            console.log('instance', instance);
        },
        onCropStart: (data) => {
            console.log('start', data);
        },
        onCropEnd: (data) => {
            cropvalue = croppr.getValue();
            console.log('end', data);
        },
        onCropMove: (data) => {
            console.log('move', data);
        },
        aspectRatio: 1,
        startSize: [100, 100]
    });
    cropvalue = croppr.getValue();
    console.log(cropvalue);
}

function GetValueCrop() {
    return cropvalue;
}


function cropImageToBase64(imageSelect, x, y, dWidth, dHeight) {
    console.log(`cropImageToBase64 > imageSelect : ${imageSelect}`);
    const image = document.querySelector(imageSelect);
    const canvas = document.createElement('canvas');
    canvas.width = dWidth;
    canvas.height = dHeight;
    const ctx = canvas.getContext('2d');
    ctx.drawImage(image, x, y, dWidth, dHeight, 0, 0, dWidth, dHeight);
    const dataURL = canvas.toDataURL('image/png');
    return dataURL;
}
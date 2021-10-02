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

function Click(parameters) {
    $(parameters).click();
}
function RawHtml(element, value) {
    let e = $(element);
    e.innerHTML = value;
};

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
        startSize: [100,100]
    });
    cropvalue = croppr.getValue();
}

function GetValueCrop() {
    return cropvalue;
}


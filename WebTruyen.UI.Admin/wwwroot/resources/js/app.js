$ = document.querySelector.bind(document);
$$ = document.querySelectorAll.bind(document);
function Click(parameters) {
    $(parameters).click();
}
function RawHtml(element, value) {
    let e = $(element);
    e.innerHTML = value;
};

function Cropper(parameters) {
    var croppr = new Croppr(parameters, {
        onInitialize: (instance) => {
            console.log(instance);
        },
        onCropStart: (data) => {
            console.log('start', data);
        },
        onCropEnd: (data) => {
            console.log('end', data);
        },
        onCropMove: (data) => {
            console.log('move', data);
        },
        aspectRatio: 1,
    });
    return croppr;
}


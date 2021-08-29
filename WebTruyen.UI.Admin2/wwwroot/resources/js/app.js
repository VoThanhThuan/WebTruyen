$ = document.querySelector.bind(document);
$$ = document.querySelectorAll.bind(document);
function Click(parameters) {
    $(parameters).click();
}
function RawHtml(element, value) {
    let e = $(element);
    e.innerHTML = value;
};

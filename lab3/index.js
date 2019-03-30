var quoteLink = "https://api.forismatic.com/api/1.0/?method=getQuote&format=jsonp&jsonp=parseQuote";
var imgSourceLink = "https://source.unsplash.com/collection/1127163/500x500";

function createHtmlElements() {
    var canvas = document.createElement('canvas');
    canvas.height = 500;
    canvas.width = 500;
    document.body.appendChild(canvas);

    var btn = document.createElement('button');
    btn.innerText = "Save";
    document.body.appendChild(btn);

    return new Promise(function (resolve, reject) {
        resolve(canvas);
    });

}

var canvasCtx;

function parseQuote(response) {
    text = response.quoteText;
    canvasCtx.font = "20px serif";
    canvasCtx.fillText(text, 100, 100);
}

function loadQuota(canvas) {
    $.ajax({
        url: quoteLink,
        dataType: 'jsonp'
    });
    canvasCtx = canvas.getContext("2d");
}

function loadImage(canvas) {
    var img = new Image();
    img.src = imgSourceLink;
    return new Promise(function (resolve, reject) {
        img.onload = function () {
            var ctx = canvas.getContext("2d");
            ctx.drawImage(img, 0, 0);
            resolve(canvas)
        }
    })
}

createHtmlElements()
    .then(canvas => loadImage(canvas))
    .then(canvas => loadQuota(canvas));
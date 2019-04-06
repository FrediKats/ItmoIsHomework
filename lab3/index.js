var quoteLink = "https://api.forismatic.com/api/1.0/?method=getQuote&format=jsonp&jsonp=parseQuote";
var imgSourceLink = "https://source.unsplash.com/collection/1127163/250x500";
var unsplash = "https://source.unsplash.com/collection/112716";
var size = "/250x500"


var canvas;
var canvasCtx;

function createHtmlElements() {
    canvas = document.createElement('canvas');
    canvas.height = 500;
    canvas.width = 500;
    document.body.appendChild(canvas);

    //WoW, a? Not button?
    // Yup, another jewish trick
    var btn = document.createElement('a');
    btn.innerText = "Save";
    document.body.appendChild(btn);

    btn.addEventListener('click', function () {
        var dataURL = canvas.toDataURL('image/jpg');
        btn.href = dataURL;
        btn.download = "image-name.jpg";
    });
}

function parseQuote(response) {
    text = response.quoteText;
    var lines = text.replace(/(?![^\n]{1,32}$)([^\n]{1,32})\s/g, '$1\n').split("\n");
    canvasCtx.font = "20px serif";
    lines.map((r, i) => canvasCtx.fillText(r, 100, 100 + 30 * i));
}

function loadQuota() {
    $.ajax({
        url: quoteLink,
        dataType: 'jsonp'
    });
    canvasCtx = canvas.getContext("2d");
}

function loadImage(marginLeft) {
    return new Promise(function (resolve, reject) {
        console.log('test');
        var img;
        img = new Image();
        img.crossOrigin = "anonymous";
        var url = unsplash + Math.floor(Math.random() * 10).toString() + size;
        img.src = url;
        img.onload = function () {
            var ctx = canvas.getContext("2d");
            ctx.drawImage(img, marginLeft, 0);
            console.log(marginLeft);
            resolve();
        }
    });
}

createHtmlElements();
var firstImage = loadImage(0);
var secondImage = loadImage(250);

Promise.all([firstImage, secondImage]).then(loadQuota);
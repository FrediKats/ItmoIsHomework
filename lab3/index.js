var quoteLink = "https://api.forismatic.com/api/1.0/?method=getQuote&format=jsonp&jsonp=parseQuote";
var imgSourceLink = "https://source.unsplash.com/collection/1127163/500x500";

function createHtmlElements() {
    var canvas = document.createElement('canvas');
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
        console.log("dataURL")
        btn.href = dataURL;
        btn.download = "image-name.jpg";
        console.log("click");
    });

    return new Promise(function (resolve, reject) {
        resolve(canvas);
    });
}

var canvasCtx;

function parseQuote(response) {
    text = response.quoteText;
    var lines = text.replace(/(?![^\n]{1,32}$)([^\n]{1,32})\s/g, '$1\n').split("\n");
    canvasCtx.font = "20px serif";
    lines.map((r, i) => canvasCtx.fillText(r, 100, 100 + 30 * i));
}

function loadQuota(canvas) {
    $.ajax({
        url: quoteLink,
        dataType: 'jsonp'
    });
    canvasCtx = canvas.getContext("2d");
}

function loadImage(canvas) {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', 'https://source.unsplash.com/collection/1127163/550x400');
    xhr.send();
    return new Promise(function (resolve, reject) {
        var img;
        xhr.onload = function (data) {
            img = new Image();
            img.crossOrigin = "anonymous";
            img.src = data.srcElement.responseURL;
            img.onload = function () {
                var ctx = canvas.getContext("2d");
                ctx.drawImage(img, 0, 0);
                resolve(canvas);
            }

        };
    });
}

createHtmlElements()
    .then(canvas => loadImage(canvas))
    .then(canvas => loadQuota(canvas));
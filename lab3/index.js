var quoteLink = "https://api.forismatic.com/api/1.0/?method=getQuote&format=jsonp&jsonp=parseQuote";
var imgSourceLink = "https://source.unsplash.com/collection/1127163/500x500";

var canvas = document.createElement('canvas');

function createHtmlElements() {
    canvas.height = 500;
    canvas.width = 500;
    document.body.appendChild(canvas);

    var btn = document.createElement('button');
    btn.innerText = "Save";
    document.body.appendChild(btn);
}

function parseQuote(response) {
    text = response.quoteText;
    var ctx = canvas.getContext("2d");
    ctx.font = "20px serif";
    ctx.fillText(text, 100, 100);
    
    console.log(text);
}

function loadQuota() {
    $.ajax({
        url: quoteLink,
        dataType: 'jsonp'
    });
}

function loadImage() {
    var img = new Image();
    img.src = imgSourceLink;
    img.onload = function () {
        var ctx = canvas.getContext("2d");
        ctx.drawImage(img, 0, 0);
        loadQuota();
    }
}

createHtmlElements();
loadImage();
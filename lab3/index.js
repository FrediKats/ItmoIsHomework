var quoteLink = "https://api.forismatic.com/api/1.0/?method=getQuote&format=jsonp&jsonp=parseQuote";
var imgSourceLink = "https://source.unsplash.com/collection/1127163/300x200";

function parseQuote(response) {
    return response.quoteText;
}

function imageLoader() {
    var xhr = new XMLHttpRequest();
    //xhr.responseType = "blob";
    xhr.open('GET', imgSourceLink, false);
    xhr.send();
    console.log(xhr.status);
    return xhr.status === 200 ? xhr.response : null;
}

function createElements() {
    var canvas = document.createElement('canvas');
    canvas.height = 500;
    canvas.width = 500;
    canvas.style.width = "500px";
    canvas.style.height = "500px";

    var btn = document.createElement('button');
    btn.innerText = "Save";

    var img = new Image();
    img.src = URL.createObjectURL(imageLoader());

    var imgContext = canvas.getContext("2d");
    imgContext.drawImage(img, 0, 0);

    document.body.appendChild(canvas);
    document.body.appendChild(btn);
}

createElements();
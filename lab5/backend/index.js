import express from 'express';
import mongoose from 'mongoose';
import bodyParser from 'body-parser';

const app = express();
const port = 3000;

// Connecting to MongoDB
//DeprecationWarning: current URL string parser is deprecated. To use the new parser, pass option { useNewUrlParser: true }
mongoose
    .connect("mongodb://localhost:27017/temp", {
        useNewUrlParser: true
    })
    .then(
        () => {
            // eslint-disable-next-line no-console
            console.info("Connected")
        },
        error => {
            // eslint-disable-next-line no-console
            console.info("MongoDB connection error" + error)
        });

const markdownFileModel = mongoose.model('MarkdownFileModel', {
    title: String,
    content: String
});

app.use(function (req, res, next) {
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE');
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type,Access-Control-Allow-Origin');

    next();
    
});

app.use(bodyParser.json());
// app.use(bodyParser.urlencoded({
//     extended: true
// }));

app.post('/save', (request, response) => {
    markdownFileModel.findOneAndUpdate({
        name: request.body.title
    }, {
        text: request.body.text
    }, {
        upsert: true
    }, function (err) {
        if (err)
            // eslint-disable-next-line no-console
            console.log(err);
        else
            return response.sendStatus(200);
    });
});

app.get('*', function (request, response) {
    response.status(404).sendStatus('404');
});

// eslint-disable-next-line no-console
app.listen(port, () => console.log(`Example app listening on port ${port}!`))
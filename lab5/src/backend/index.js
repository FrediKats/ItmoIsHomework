import express from 'express';
import mongoose from 'mongoose';

const app = express();
const port = 3000;

// Connecting to MongoDB
//DeprecationWarning: current URL string parser is deprecated. To use the new parser, pass option { useNewUrlParser: true }
mongoose
    .connect("mongodb://localhost:27017/temp", {
        useNewUrlParser: true
    })
    .then(
        result => {
            console.info("Connected")
        },
        error => {
            console.info("MongoDB connection error" + error)
        });

app.get('/', (req, res) => {
    res.send('Hello World!')

})

app.listen(port, () => console.log(`Example app listening on port ${port}!`))
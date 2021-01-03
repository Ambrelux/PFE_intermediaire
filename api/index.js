// cd C:\Program Files\MongoDB\Server\4.4\bin
// mongod.exe --dbpath "C:\data"
// mongo.exe

// CONNECT TO DATABASE

const mongoose = require('mongoose');
const db = mongoose.connection;
const db_update = {
    useNewUrlParser: true,
    useUnifiedTopology: true,
    useFindAndModify: false};
mongoose.connect('mongodb://localhost:27017/my_db', db_update);

db.on('error', (err) => console.log('Error, DB not connected'));
db.on('connected', () => console.log('connected to mongo'));
db.on('disconnected',  () => console.log('disconnected to mongo'));
db.on('open', () => console.log('connection to model'));

// SERVER

const server = new (require('koa'))();
const {router} = require('./router');

server.use(require('koa-bodyparser')());
server.use(router.routes());

server.listen(3000, 'localhost', ()=> console.log('listening on port 3000'));
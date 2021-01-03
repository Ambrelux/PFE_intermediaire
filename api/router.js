const R = require('ramda');

const router = new (require('koa-router'))();
const {Sound} = require('./models/sound.js');

router.post('/createSound', async (ctx, next) => {
    try {
        const newSound = new Sound(ctx.request.body);
        const result = await newSound.save();
        ctx.body = 'new sound successfully created';
    } catch (error) {
        ctx.body = 'error while inserting new sound';
    }
});

router.get('/findSound', async (ctx, next) => {
    try {
        const result = await Sound.find().exec();
        ctx.body = result;
    } catch (error) {
        ctx.body = "error while finding sound";
    }
});

router.get("/findSoundById/:id", async (ctx, next) => {
    try {
        const result = await Sound.findById(ctx.params.id).exec();
        ctx.body = result;
    } catch (error) {
       ctx.body = "error while finding sound by id"
    }
});

module.exports = {router};

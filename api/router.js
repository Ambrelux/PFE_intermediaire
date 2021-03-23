const R = require('ramda');

const router = new (require('koa-router'))();
const {Sound} = require('./models.js');

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

router.get("/findSoundBySceneId/:scene", async (ctx, next) => {
    try {
        const result = await Sound.find({scene:{$eq: parseInt(ctx.params.scene,10)}}).exec();
        ctx.body = result;
    } catch (error) {
        ctx.body = "error while finding sound by scene"
    }
});

router.get("/deleteSounds", async(ctx,next) =>{
   try{
       const result = await Sound.deleteMany({ frequency: "0" })  .exec();
       ctx.body = "deleted";
   } catch (error){
       ctx.body = "error while deleting";
   }

});

module.exports = {router};

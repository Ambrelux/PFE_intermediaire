const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const soundSchema = new Schema({
    emitted: {type: Date, default: Date.now},
    frequency: { type: Number, min: 20, max: 20000 },
    spheres: [{key: Number, xcoord: Number, ycoord: Number, zcoord: Number}]
});

const Sound = mongoose.model('Sound', soundSchema);

module.exports= {Sound};
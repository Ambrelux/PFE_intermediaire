const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const soundSchema = new Schema({
    // emitted: {type: Date, default: Date.now},
    // frequency: { type: Number, min: 20, max: 20000 },
    // // spheres: [{key: Number, xcoord: Number, ycoord: Number, zcoord: Number}]
    // spheres: [{key: Number, coordinates: [[Number]]}]
    emitted: String,
    frequency: String,
    // spheres: [{key: Number, xcoord: Number, ycoord: Number, zcoord: Number}]
    spheres: String
}, {
    versionKey: false // You should be aware of the outcome after set to false
});

const Sound = mongoose.model('Sound', soundSchema);

module.exports= {Sound};
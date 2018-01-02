//Dependencies
var io = require('socket.io')()
var World = require('./game/world.js').World

/*
How to use the shit
var chunk = mapGen.generateMap(30, 30, 0, 8, 0.5, 0.01, 0, 0)

map is an array of chunks

var map = {(Chunk.x, Chunk.y): Chunk}
*/

var world = new World()

io.on('connection', function(client) {
    console.log("Client connected")

    //send map on connect
    client.emit('map', world.getChunk(0, 0))

    client.on('requestMap', function(data) {
        console.log(data)
        var chunk = world.getChunk(data['x'], data['y'])
        client.emit('map', chunk)
    })

    client.on('disconnect', function() {
        console.log("Client disconnected")
    })
})

io.listen(7377)

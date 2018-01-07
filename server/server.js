//Dependencies
var io = require('socket.io')()
var World = require('./game/world.js').World
var Chunk = require('./game/chunk.js').Chunk
var Player = require('./game/player.js').Player

//settings
const tps = 1

/*
How to use the shit
var chunk = mapGen.generateMap(30, 30, 0, 8, 0.5, 0.01, 0, 0)

map is an array of chunks

var map = {(Chunk.x, Chunk.y): Chunk}
*/

var world = new World()

io.on('connection', function(client) {
    //init
    const player = new Player(client.id)
    world.addPlayer(player)
    console.log(player.id + " connected")

    client.on('player-action', function(data){
        /*
        structure:
        {e: event to handle,
        //stuff
        }
        */
        world.updateOnPlayerAction(player, data)
    })

    client.on('disconnect', function() {
        world.removePlayer(player)
        console.log("Client disconnected")
    })
})

setInterval(function() {
    for(var id in world.players) {
        var player = world.players[id]
        var chunk = world.getChunk(player.x, player.y)
        console.log(player.x + " " + player.y)
        console.log(chunk.x + " " + chunk.y)
        console.log("")
        io.to(id).emit('map', chunk)
    }
}, 1000 / tps)

io.listen(7377)

var Chunk = require('./chunk.js').Chunk

module.exports = {
    World: class World {
        constructor() {
            this.chunks = {}
            //currently online players
            this.players = {}
        }

        getChunk(worldX, worldY) {
            var x = Math.floor(worldX / Chunk.width)
            var y = Math.floor(worldY / Chunk.height)

            if(this.chunks[[x, y]] == null) {
                this.chunks[[x, y]] = new Chunk(x, y)
            }

            return this.chunks[[x, y]]
        }
        
        getPlayer(id) {
            return this.players[id]
        }

        addPlayer(player) {
            this.players[player.id] = player
            var chunk = this.getChunk(player.x, player.y)
            chunk.addPlayer(player)
        }

        removePlayer(player) {
            this.getChunk(player.x, player.y).removePlayer(player)
            delete this.players[player.id]
            console.log("removed")
        }

        //networking stuff

        updateOnPlayerAction(player, data) {
            switch(data['e']) {
                case 'movement':
                    this.getChunk(player.x, player.y).removePlayer(player)
                    console.log(player)
                    player.move(data['key'])
                    this.getChunk(player.x, player.y).addPlayer(player)
                    break
            }
        }
    }
}

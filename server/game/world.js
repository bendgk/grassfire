var Chunk = require('./chunk.js').Chunk

module.exports = {
    World: class World {
        constructor() {
            this.terrain = {}
            this.players = {}
        }

        getChunk(x, y) {
            if(this.terrain[[x, y]] == null) {
                this.terrain[[x, y]] = new Chunk(x, y)
            }

            return this.terrain[[x, y]]
        }
    }
}

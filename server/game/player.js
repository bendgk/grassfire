var World = require('./world.js').World

module.exports = {
    Player: class {
        constructor(username) {
            this.username = username
            this.x = 0
            this. y = 0
            this.chunk = World.getChunk(0, 0)
        }
    }
}

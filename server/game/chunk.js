var noiseMapGen = require('../renderer/noiseGenerator.js')
var colorMapGen = require('../renderer/textureGenerator.js')
/*
How to use the shit
x, y are chunk position
x * width, y * height returns realworld position (bottom left corner)
all realworld coordinate calcualtions are done using bottom left corner of chunk

generateMap(mapWidth, mapHeight, seed, octaves, persistence, frequency, posX, posY)

Multiplying by -1 fixes stuff... (NOT ANYMORE HAHAh)
*/

module.exports = {
    Chunk: class {

        static get width() {return 20}
        static get height() {return 20}

        constructor(x, y) {
            this.x = x
            this.y = y
            this.width = this.constructor.width
            this.height = this.constructor.height

            this.players = []

            //Back to good ole postive numbers
            this.noiseMap = noiseMapGen.generateMap(this.width, this.height, "grassfire1", 8, 2, .0002, x * this.width, y * this.height)
            this.colorMap = colorMapGen.noiseMapToColorMap(this.noiseMap, this.width, this.height)
        }

        addPlayer(player) {
            console.log("shit2")
            if(this.players.indexOf(player) == -1) {
                this.players.push(player)
            }
            console.log("shit3")
        }

        removePlayer(player) {
            var index = this.players.indexOf(player)
            if(index != -1) {
                this.players.splice(index, 1)
            }
        }
    }
}

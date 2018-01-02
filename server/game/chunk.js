var noiseMapGen = require('../renderer/noiseGenerator.js')
var colorMapGen = require('../renderer/textureGenerator.js')
/*
How to use the shit
x, y are position of the chunk in the map
x * width, y* height returns realworld position of chunk (bottom left corner)
all realworld coordinate calcualtions are done using bottom left corner of chunk

generateMap(mapWidth, mapHeight, seed, octaves, persistence, frequency, posX, posY)

Multiplying by -1 fixes stuff... (NOT ANYMORE HAHAh)
*/

module.exports = {
    Chunk: class {
        constructor(x, y) {
            this.x = x
            this.y = y

            this.width = 20
            this.height = 20
            //Back to good ole postive numbers
            this.noiseMap = noiseMapGen.generateMap(this.width, this.height, "grassfire1", 8, 2, .0002, x * this.width, y * this.height)
            this.colorMap = colorMapGen.noiseMapToColorMap(this.noiseMap, this.width, this.height)
        }
    }
}

var seedrandom = require('seedrandom')
var FastSimplexNoise = require('fast-simplex-noise').default

module.exports = {
    generateMap: function (mapWidth, mapHeight, seed, octaves, persistence, frequency, posX, posY) {
        var prng = seedrandom(seed)
        const noiseGen = new FastSimplexNoise({frequency: frequency, min: 0, max: 1, octaves: octaves, persistence: persistence, random: prng})

        var noiseMap = new Array(mapHeight)
        for(let i = 0; i < mapHeight; i++) {
            noiseMap[i] = new Array(mapWidth)
        }

        for(let y = posY; y < posY + mapHeight; y++) {
            for(let x = posX; x < posX + mapWidth; x++) {
                noiseMap[y - posY][x - posX] = noiseGen.cylindrical(1000, [x, y])
            }
        }
        return noiseMap
    }
}

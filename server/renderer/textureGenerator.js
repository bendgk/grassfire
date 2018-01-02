var Color = require('../unity.js').Color
var Region = require('./region.js').Region

regions = [
    new Region("water deep", .3, new Color('#305ba0')),
    new Region("water shallow", .4, new Color('#2473f2')),
    new Region("sand", .45, new Color('#f7e896')),
    new Region("grass1", .55, new Color('#57d666')),
    new Region("grass2", 1, new Color('#1a7725'))
]

module.exports = {
    noiseMapToColorMap: function (noiseMap, width, height) {
        var colorMap = new Array(width * height)

        for(let y = height - 1; y >= 0; y--) {
            for(let x = width - 1; x >= 0; x--) {
                var currentHeight = noiseMap[y][x]

                for(let i = 0; i < regions.length; i++) {
                    if(currentHeight <= regions[i].height) {
                        colorMap[(height - y - 1) * width + (width - x - 1)] = regions[i].color
                        break
                    }
                }
            }
        }

        return colorMap
    }
}

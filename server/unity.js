function hexToRgb(hex) {
    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    return result ? {
        r: parseInt(result[1], 16),
        g: parseInt(result[2], 16),
        b: parseInt(result[3], 16)
    } : null;
}

module.exports = {
    Color: class {
        constructor(hex) {
            var color = hexToRgb(hex)
            this.r = color.r / 255.0
            this.g = color.g / 255.0
            this.b = color.b / 255.0
            this.a = 1
        }
    },

    Key: {
        UP: "u",
        DOWN: "d",
        LEFT: "l",
        RIGHT: "r"
    }
}

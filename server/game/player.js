var Key = require('../unity.js').Key

module.exports = {
    Player: class {
        constructor(id) {
            this.id = id
            //x and y of player are wolrd relative, not chunk
            this.x = 0
            this.y = 0
        }

        move(key) {
            switch (key) {
                case Key.UP:
                    this.y += .8
                    break
                case Key.DOWN:
                    this.y -= .8
                    break
                case Key.LEFT:
                    this.x -= .8
                    break
                case Key.RIGHT:
                    this.x += .8
                    break
                default: break

            }
        }
    }
}

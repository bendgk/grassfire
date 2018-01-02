using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk {
    private static int _height = 10;
    private static int _width = 10;

    public static int height {
        get { return _height; }
    }

    public static int width {
        get { return _width; }
    }

    public Texture2D colorMap {
        get { return colorMap; }
        private set { colorMap = value; }
    }

    public Chunk(Texture2D colorMap) {
        this.colorMap = colorMap;
    }
}

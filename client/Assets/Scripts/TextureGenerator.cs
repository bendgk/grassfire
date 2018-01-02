using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator {
    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height) {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap) {
        int width = heightMap.GetLength(1);
        int height = heightMap.GetLength(0);

        float[] regions = {.7f, 1f};
        Color[] colors = new Color[] {Color.green, Color.blue};

        Color[] colorMap = new Color[width * height];
        for(int y = height - 1; y >= 0; y--) {
            for(int x = width - 1; x >= 0; x--) {

                float currentHeight = heightMap[y, x];

                /*
                for (int i = 0; i < regions.Length; i++) {
                    if (currentHeight <= regions[i]) {
                        colorMap[(height - y - 1) * width + (width - x - 1)] = colors[i];
                        break;
                    }
                }*/

                //Rendering is werid (Textures are weird)
                colorMap[(height - y - 1) * width + (width - x - 1)] = Color.Lerp(Color.black, Color.white, heightMap[y, x]);
            }
        }

        return TextureFromColorMap(colorMap, width, height);
    }
}

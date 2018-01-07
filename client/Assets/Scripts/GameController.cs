using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class GameController : MonoBehaviour {

    private SocketIOComponent socket;

    public GameObject player;
    private Rigidbody PlayerRB;

    public GameObject map;
    private Transform MapT;

    void Start() {
        PlayerRB = player.GetComponent<Rigidbody>();
        MapT = map.GetComponent<Transform>();
        print(MapT.lossyScale);

        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        socket.On("map", drawMap);
    }

    void Update () {
        /*
        if (PlayerRB.position.x >= MapT.position.x + (MapT.lossyScale.x * 5)) {
            PlayerRB.position = new Vector3(PlayerRB.position.x - MapT.position.x - (MapT.lossyScale.x * MapT.lossyScale.x / 2.0f) + 1, PlayerRB.position.y, PlayerRB.position.z);
        }

        if (PlayerRB.position.x <= MapT.position.x - (MapT.lossyScale.x * 5)) {
            PlayerRB.position = new Vector3(PlayerRB.position.x + MapT.position.x + (MapT.lossyScale.x * MapT.lossyScale.x/2.0f) - 1, PlayerRB.position.y, PlayerRB.position.z);
        }

        if (PlayerRB.position.z >= MapT.position.z + (MapT.lossyScale.z * 5)) {
            PlayerRB.position = new Vector3(PlayerRB.position.x, PlayerRB.position.y, PlayerRB.position.z - MapT.position.z - (MapT.lossyScale.z * MapT.lossyScale.z / 2.0f) + 1);
        }

        if (PlayerRB.position.z <= MapT.position.z - (MapT.lossyScale.z * 5)) {
            PlayerRB.position = new Vector3(PlayerRB.position.x, PlayerRB.position.y, PlayerRB.position.z + MapT.position.z + (MapT.lossyScale.z * MapT.lossyScale.z / 2.0f) - 1);
        }
        */
    }

    public void drawMap(SocketIOEvent e) {
        //FindObjectOfType<MapDisplay>().DrawTexture(TextureGenerator.TextureFromHeightMap(JSONObjectTo2DFloat(e.data["noiseMap"])));
        //print(e.data["colorMap"]);
        Color[] colorMap = JSONObjectToColorArray(e.data["colorMap"]);
        int width = (int)e.data["width"].n;
        int height = (int)e.data["height"].n;
        //print(colorMap);

        FindObjectOfType<MapDisplay>().DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, width, height));

        //Testing move stuff
        float x = e.data["players"][0]["x"].n;
        float y = e.data["players"][0]["y"].n;
        print(x + " " + y);
        PlayerRB.position = new Vector3(x, 1, y);
    }

    private Color[] JSONObjectToColorArray(JSONObject o) {
        Color[] colorMap = new Color[o.list.Count];
        //print(colorMap);

        for(int i = 0; i < colorMap.Length; i++) {
            Color tile = new Color(o[i]["r"].n, o[i]["g"].n, o[i]["b"].n, o[i]["a"].n);
            colorMap[i] = tile;
        }
        //print("done");
        return colorMap;
    }

    private float[,] JSONObjectTo2DFloat(JSONObject o) {
        int height = o.list.Count;
        int width = o[0].list.Count;
        float[,] float2d = new float[height, width];
        for (int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                float2d[y, x] = o[y][x].n; //Never again using someone elses shit... (GOTTA GET THEM NUMBERZZZ)
            }
        }
        return float2d;
    }
}

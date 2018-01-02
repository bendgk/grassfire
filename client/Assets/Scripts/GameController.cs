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

    private int chunkX = 0;
    private int chunkY = 0;

    void Start() {
        PlayerRB = player.GetComponent<Rigidbody>();
        MapT = map.GetComponent<Transform>();
        print(MapT.lossyScale);

        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        socket.On("map", drawMap);
    }

    void Update () {
        if (PlayerRB.position.x >= MapT.position.x + (MapT.lossyScale.x * 5)) {
            PlayerRB.position = new Vector3(PlayerRB.position.x - MapT.position.x - (MapT.lossyScale.x * MapT.lossyScale.x / 2.0f) + 1, PlayerRB.position.y, PlayerRB.position.z);
            chunkX++;
            requestMap(chunkX, chunkY);
        }

        if (PlayerRB.position.x <= MapT.position.x - (MapT.lossyScale.x * 5)) {
            PlayerRB.position = new Vector3(PlayerRB.position.x + MapT.position.x + (MapT.lossyScale.x * MapT.lossyScale.x/2.0f) - 1, PlayerRB.position.y, PlayerRB.position.z);
            chunkX--;
            requestMap(chunkX, chunkY);
        }

        if (PlayerRB.position.z >= MapT.position.z + (MapT.lossyScale.z * 5)) {
            PlayerRB.position = new Vector3(PlayerRB.position.x, PlayerRB.position.y, PlayerRB.position.z - MapT.position.z - (MapT.lossyScale.z * MapT.lossyScale.z / 2.0f) + 1);
            chunkY++;
            requestMap(chunkX, chunkY);
        }

        if (PlayerRB.position.z <= MapT.position.z - (MapT.lossyScale.z * 5)) {
            PlayerRB.position = new Vector3(PlayerRB.position.x, PlayerRB.position.y, PlayerRB.position.z + MapT.position.z + (MapT.lossyScale.z * MapT.lossyScale.z / 2.0f) - 1);
            chunkY--;
            requestMap(chunkX, chunkY);
        }
    }

    public void drawMap(SocketIOEvent e) {
        //FindObjectOfType<MapDisplay>().DrawTexture(TextureGenerator.TextureFromHeightMap(JSONObjectTo2DFloat(e.data["noiseMap"])));

        Color[] colorMap = JSONObjectToColorArray(e.data["colorMap"]);
        int width = (int)e.data["width"].n;
        int height = (int)e.data["height"].n;

        FindObjectOfType<MapDisplay>().DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, width, height));
    }

    private Color[] JSONObjectToColorArray(JSONObject o) {
        Color[] colorMap = new Color[o.list.Count];

        for(int i = 0; i < colorMap.Length; i++) {
            Color tile = new Color(o[i]["r"].n, o[i]["g"].n, o[i]["b"].n, o[i]["a"].n);
            colorMap[i] = tile;
        }

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

    private void requestMap(int x, int y) {
        //print("----");
        //print("x: " + x);
        //print("y: " + y);
        Dictionary<string, int> data = new Dictionary<string, int>();
        data["x"] = x;
        data["y"] = y;
        socket.Emit("requestMap", new JSONObject(data));
    }
}

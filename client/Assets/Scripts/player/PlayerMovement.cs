using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;


public class PlayerMovement : MonoBehaviour {

    private SocketIOComponent socket;

    //Controls
    public KeyCode up;
    public KeyCode down;
    public KeyCode right;
    public KeyCode left;

    //properties
    public float speed;
    private Vector3 velocity = Vector3.zero;

    private Rigidbody rb;
    Dictionary<string, string> data = new Dictionary<string, string>();

    private void Start() {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if(Input.GetKey(up)) {
            move("u");
        }

        if (Input.GetKey(down)) {
            move("d");
        }

        if (Input.GetKey(left)) {
            move("l");
        }

        if (Input.GetKey(right)) {
            move("r");
        }
    }

    private void move(string key) {
        data["e"] = "movement";
        data["key"] = key;
        socket.Emit("player-action", new JSONObject(data));
    }
}

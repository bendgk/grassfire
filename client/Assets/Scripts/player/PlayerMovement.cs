using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Controls
    public KeyCode up;
    public KeyCode down;
    public KeyCode right;
    public KeyCode left;

    //properties
    public float speed;
    private Vector3 velocity = Vector3.zero;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        velocity = Vector3.zero;

        if(Input.GetKey(up)) {
            velocity.z = speed;
        }

        if (Input.GetKey(down)) {
            velocity.z = -speed;
        }

        if (Input.GetKey(left)) {
            velocity.x = -speed;
        }

        if (Input.GetKey(right)) {
            velocity.x = speed;
        }
        rb.velocity = velocity;
    }
}

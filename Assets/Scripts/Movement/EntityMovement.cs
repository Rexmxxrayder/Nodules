using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour {
    public float speed = 1;
    private void Update() {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey("z")) {
            direction += Vector3.up;
        }
        if (Input.GetKey("q")) {
            direction += Vector3.left;
        }
        if (Input.GetKey("s")) {
            direction += Vector3.down;
        }
        if (Input.GetKey("d")) {
            direction += Vector3.right;
        }
        transform.position += direction * Time.deltaTime * speed;
    }
}

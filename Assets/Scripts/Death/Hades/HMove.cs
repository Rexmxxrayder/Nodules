using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMove : MonoBehaviour {
    public float Speed;
    public Camera camera;
    HPhysics hPhysics;
    public static HMove instance;


    private void Awake() {
        instance = this;
    }
    private void Start() {
        hPhysics = GetComponent<HPhysics>();
        hPhysics.Forces.Add("Move", Vector3.zero);
    }

    private void Update() {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.Z)) {
            direction += camera.transform.forward;
        }

        if (Input.GetKey(KeyCode.S)) {
            direction += -camera.transform.forward;
        }

        if (Input.GetKey(KeyCode.D)) {
            direction += camera.transform.right;
        }

        if (Input.GetKey(KeyCode.Q)) {
            direction += -camera.transform.right;
        }

        direction.y = 0;

        hPhysics.Forces["Move"] = direction.normalized * Speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPhysics : MonoBehaviour
{
    public Dictionary<string,Vector3> Forces = new();
    public bool IsDashing;
    public bool IsPushed;
    Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        Vector3 velocity = Vector3.zero;
        foreach (KeyValuePair<string,Vector3> kvp in Forces) {
            velocity += kvp.Value;
        }

        rb.velocity = velocity;
    }
}

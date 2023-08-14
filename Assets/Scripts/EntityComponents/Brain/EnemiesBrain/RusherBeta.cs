using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class RusherBeta : MonoBehaviour {
    public Transform target;
    public Rigidbody2D rb;
    public float acceleration;
    public float maxspeed;
    public float targetDetection;
    public float turn;
    Coroutine follow;
    bool hit;

    private void Update() {
        if (follow == null && Vector3.Distance(transform.position, target.position) < targetDetection) {
            follow = StartCoroutine(Follow());
        }
    }

    IEnumerator Follow() {
        while (!hit) {
            float turnpower = 1;
            float dot = Vector3.Dot((Vector2)(target.position - transform.position).normalized, rb.velocity.normalized);
            turnpower = Mathf.Lerp(1, turn, Mathf.InverseLerp(-1, 1, dot));
            rb.velocity += (Vector2)(target.position - transform.position).normalized * acceleration * turnpower;
            if(rb.velocity.magnitude > maxspeed) {
                rb.velocity = rb.velocity.normalized * maxspeed;
            }
            yield return null;
        }
        hit = false;
        rb.velocity= Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            hit = true;
        }
    }
}

using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    Rigidbody2D _rb;
    public Vector3 Velocity { get => _rb.velocity; set => _rb.velocity = value; }
    Timer beforeDeath;
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        beforeDeath = new Timer(10, () => Destroy(gameObject)).Start();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}

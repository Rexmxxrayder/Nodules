using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    Rigidbody2D _rb;
    Collider2D _c;
    float lifeTime = 10f;
    float collisionActivation = 0.5f;
    public Vector3 Velocity { get => _rb.velocity; set => _rb.velocity = value; }
    Timer beforeDeath;
    Timer beforeColliderActivation;
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _c = GetComponent<Collider2D>();
        _c.isTrigger = true;
        beforeDeath = new Timer(lifeTime, () => Destroy(gameObject)).Start();
        beforeColliderActivation = new Timer(collisionActivation, () => _c.isTrigger = false).Start();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}

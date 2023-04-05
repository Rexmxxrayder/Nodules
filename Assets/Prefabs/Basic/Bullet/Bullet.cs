using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Basic {
    Collider2D _c;
    public float lifeTime = 10f;
    public float collisionActivation = 0.5f;
    Timer beforeDeath;
    Timer beforeColliderActivation;

    private void Awake() {
        _c = GetComponentInChildren<Collider2D>();
    }

    public override void Activate(EntityRoot root) {
        _c.isTrigger = true;
        beforeDeath = new Timer(this, lifeTime, () => BasicPrefabs.Gino.LetInstance(this)).Start();
        beforeColliderActivation = new Timer(this, collisionActivation, () => _c.isTrigger = false).Start();
    }
}
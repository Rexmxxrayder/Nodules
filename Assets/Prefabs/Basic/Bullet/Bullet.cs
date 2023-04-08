using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Basic {
    protected Collider2D _c;
    public float lifeTime = 10f;
    public int damage;
    protected Timer beforeDeath;

    protected override void AwakeSetup() {
        _c = GetComponentInChildren<Collider2D>();
    }

    public override void Activate() {;
        beforeDeath = new Timer(this, lifeTime, () => gameObject.Get<EntityHealth>().LethalDamage()).Start();
        GetComponentInChildren<EntityDamageCollider2D>().damage = damage;
    }
}
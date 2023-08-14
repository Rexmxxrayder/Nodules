using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageCollider3D : EntityColliderDelegate3D {
    public int damage;
    public List<string> Damaged = new();
    [SerializeField] bool destroyOnCollide;
    [SerializeField] bool destroyOnTrigger;

    void DoDamageCollider(Collider game) {
        EntityHealth health = game.gameObject.RootGet<EntityHealth>();
        if (health != null && Damaged.Contains(health.GetRoot().tag)) {
                health.RemoveHealth(damage);
        }

        if (destroyOnTrigger) {
            Die();
        }
    }


    void DoDamageCollision(Collision game) {
        EntityHealth health = game.gameObject.RootGet<EntityHealth>();
        if (health != null && Damaged.Contains(health.GetRoot().tag)) {
            health.RemoveHealth(damage);
        }

        if (destroyOnCollide) {
            Die();
        }
    }

    protected override void ResetSetup() {
        OnCollisionEnterDelegate += DoDamageCollision;
        OnTriggerEnterDelegate += DoDamageCollider;
    }
}

using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageCollider2D : EntityColliderDelegate2D {
    public int damage;
    public List<string> Damaged = new();
    [SerializeField] bool destroyOnCollide;
    [SerializeField] bool destroyOnTrigger;

    public void SetDamage(int newDamage) {
        damage = Mathf.Max(0, newDamage);
    }

    void DoDamageCollider(Collider2D game) {
        EntityHealth health = game.gameObject.GetRootComponent<EntityHealth>();
        if (health != null && Damaged.Contains(health.GetRoot().tag)) {
                health.RemoveHealth(damage);
        }

        if (destroyOnTrigger) {
            Die();
        }
    }


    void DoDamageCollision(Collision2D game) {
        EntityHealth health = game.gameObject.GetRootComponent<EntityHealth>();
        if (health != null && Damaged.Contains(health.GetRoot().tag)) {
            health.RemoveHealth(damage);
        }

        if (destroyOnCollide) {
            Die();
        }
    }

    protected override void ResetSetup() {
        OnCollisionEnterDelegate += DoDamageCollision;
        OnCollisionStayDelegate += DoDamageCollision;
        OnTriggerEnterDelegate += DoDamageCollider;
        OnTriggerStayDelegate += DoDamageCollider;
    }
}

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
        EntityHealth health = game.gameObject.Get<EntityHealth>();
        if (health != null) {
            if (Damaged.Contains(health.GetRoot().tag)) {
                health.RemoveHealth(damage);
            }
        }

        if (destroyOnTrigger) {
            Get<EntityDeath>().Die();
        }
    }


    void DoDamageCollision(Collision2D game) {
        EntityHealth health = game.gameObject.Get<EntityHealth>();
        if (health != null) {
            if (Damaged.Contains(health.GetRoot().tag)) {
                health.RemoveHealth(damage);
            }
        }

        if (destroyOnCollide) {
            Get<EntityDeath>().Die();
        }
    }
    void ResetSetup() {
        OnCollisionEnter += DoDamageCollision;
        OnCollisionStay += DoDamageCollision;
        OnTriggerEnter += DoDamageCollider;
        OnTriggerStay += DoDamageCollider;
    }

    protected override void StartSetup() {
        ResetSetup();
    }
}

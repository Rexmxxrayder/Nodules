using Sloot;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityDamageCollider3D : EntityComponent {
    public int damage;
    public List<string> Damaged = new();
    [SerializeField] bool destroyOnCollide;
    [SerializeField] bool destroyOnTrigger;
    void DoDamageCollision(Collision collision) {
        EntityHealth health = collision.gameObject.RootGet<EntityHealth>();
        if (health != null && Damaged.Contains(health.GetRoot().tag)) {
            health.RemoveHealth(damage);
        }

        if (destroyOnCollide) {
            Die();
        }
    }

    void DoDamageCollider(Collider collider) {

        EntityHealth health = collider.gameObject.RootGet<EntityHealth>();
        if (health != null && Damaged.Contains(health.GetRoot().tag)) {
                health.RemoveHealth(damage);
        }

        if (destroyOnTrigger) {
            Die();
        }
    }

    protected override void LoadSetup() {
        RootGet<EntityMainCollider3D>().OnCollisionEnterDelegate += DoDamageCollision;
        RootGet<EntityMainCollider3D>().OnTriggerEnterDelegate += DoDamageCollider;
    }
}

using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageCollider2D : EntityComponent, IReset {
    EntityCollider2D ec;
    [SerializeField] int damage;
    public List<string> Damaged = new List<string>();
    [SerializeField] bool destroyOnHit;
    List<EntityHealth> hit = new List<EntityHealth>();


    public void SetDamage(int newDamage) {
        damage = Mathf.Max(0, newDamage);
    }
    void DoDamageCollider(Collider2D game) {
        EntityHealth health = game.gameObject.Get<EntityHealth>();
        if (health != null && !hit.Contains(health)) {
            if (Damaged.Contains(health.GetRoot().tag)) {
                health.RemoveHealth(damage);
                hit.Add(health);
                Debug.Log(game.gameObject.Get<EntityRoot>().name);
            }
        }
        if (destroyOnHit) {
            Basic basic = _root.GetComponent<Basic>();
            if (basic != null) {
                BasicPrefabs.Gino.LetInstance(basic);
            } else {
                Destroy(_root.gameObject);
            }
        }
    }

    void DoDamageCollision(Collision2D game) {
        DoDamageCollider(game.collider);
    }

    protected override void ChildSetup() {
        InstanceReset();
        ec = GetComponentInChildren<EntityCollider2D>();
        if (ec == null) {
            Debug.LogError("No EntityCollider2D");
        }
        ec.OnCollisionEnter += DoDamageCollision;
        ec.OnCollisionStay += DoDamageCollision;
        ec.OnTriggerEnter += DoDamageCollider;
        ec.OnTriggerStay += DoDamageCollider;
    }

    public void ResetHit() {
        hit.Clear();
    }

    public void InstanceReset() {
        ResetHit();
        Damaged.Clear();
        Damaged.Add("Untagged");
    }
}

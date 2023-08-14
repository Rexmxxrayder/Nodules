using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindShield : EntityBasic {
    protected override void LoadSetup() {
        base.LoadSetup();
        RootGet<EntityMainCollider2D>().OnCollisionEnterDelegate += ReflectProjectile;
    }

    public void ReflectProjectile(Collision2D collision) {
        if (collision.gameObject.GetRoot().CompareTag("Projectile")){
            string newProjectileName = collision.gameObject.GetRoot().Type;
            newProjectileName += "Ally";
            Vector3 velocity = collision.gameObject.RootGet<EntityPhysics>().Velocity;
            Vector3 position = collision.gameObject.GetRootPosition();
            collision.gameObject.Die();
            Bullet newProjectile = BasicPools.Gino.GetInstance(newProjectileName).GetComponent<Bullet>();
            newProjectile.transform.position = position;
            newProjectile.Fire(-velocity.normalized, velocity.magnitude);
        }
    }
}

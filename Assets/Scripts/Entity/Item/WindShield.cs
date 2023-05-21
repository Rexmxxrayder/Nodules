using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindShield : EntityBasic {
    protected override void StartSetup() {
        base.StartSetup();
        Get<EntityMainCollider2D>().OnCollisionEnter += ReflectProjectile;
    }

    public void ReflectProjectile(Collision2D collision) {
        if (collision.gameObject.GetRoot().CompareTag("Projectile")){
            string newProjectileName = collision.gameObject.GetRoot().GetComponent<EntityRoot>().Type;
            newProjectileName += "Ally";
            Bullet newProjectile = BasicPools.Gino.GetInstance(newProjectileName).GetComponent<Bullet>();
            newProjectile.transform.position = collision.gameObject.GetRootPosition();
            Vector3 velocity = collision.gameObject.Get<EntityPhysics>().Velocity;
            newProjectile.StartDirection = -velocity.normalized;
            newProjectile.BulletSpeed = velocity.magnitude;
        }
    }
}

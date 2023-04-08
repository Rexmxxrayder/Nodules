using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AShootBullet : Ability {
    public float BulletSpeed;
    public string typeBullet;
    public int damage;
    protected override void LaunchAbility((Brain, Body) bodyBrain) {
        Shoot(bodyBrain.Item1.Visor);
    }

    void Shoot(Vector2 direction) {
        Bullet newBullet = (Bullet)BasicPrefabs.Gino.GetInstance(typeBullet);
        newBullet.transform.position = transform.position;
        newBullet.GetComponentInChildren<EntityPhysics>().Add(Force.Const((Vector2)((Vector3)direction - transform.position), BulletSpeed, 100), (int)EntityPhysics.PhysicPriority.PROJECTION);
        newBullet.damage = damage;
        newBullet.Activate();
    }
}
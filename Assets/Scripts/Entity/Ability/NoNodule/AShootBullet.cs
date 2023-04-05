using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AShootBullet : Ability {
    public float BulletSpeed;
    protected override void LaunchAbility((Brain, Body) bodyBrain) {
        Shoot(bodyBrain.Item1.Visor, bodyBrain.Item2.gameObject.Get<EntityRoot>());
    }

    void Shoot(Vector2 direction, EntityRoot root) {
        Basic newBullet = BasicPrefabs.Gino.GetInstance("Bullet");
        newBullet.transform.position = transform.position;
        newBullet.GetComponentInChildren<EntityPhysics>().Add(Force.Const((Vector2)((Vector3)direction - transform.position), BulletSpeed, 100), (int)EntityPhysics.PhysicPriority.PROJECTION);
        newBullet.Activate(root);
    }
}
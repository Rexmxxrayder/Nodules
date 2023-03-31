using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AShootBullet : Ability {
    public float BulletSpeed;
    protected override void LaunchAbility((Brain, Body) bodyBrain) {
        Shoot(bodyBrain.Item1.Visor);
    }

    void Shoot(Vector2 direction) {
        Bullet newBullet = Instantiate(BasicPrefabs.Gino.Bullet);
        newBullet.transform.position = transform.position;
        newBullet.Velocity = ((Vector3)direction - newBullet.transform.position).normalized * BulletSpeed;
    }
}
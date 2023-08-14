using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AShootBullet : Ability {
    public string typeBullet;
    protected override void LaunchAbilityUp(EntityBrain brain) {
        Shoot(brain.Visor - GetComponentInParent<EntityBodyPart>().GetRootPosition());
    }

    void Shoot(Vector2 direction) {
        direction.Normalize();
        Bullet newBullet = (Bullet)BasicPools.Gino.GetInstance(typeBullet);
        newBullet.transform.position = transform.position;
        newBullet.Fire(direction);
    }
}
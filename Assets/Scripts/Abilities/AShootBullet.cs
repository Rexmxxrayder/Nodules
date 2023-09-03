using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AShootBullet : Ability {
    public Bullet bulletprefab;
    public Bullet currentBullet;
    protected override void LaunchAbilityUp(EntityBrain brain) {
        Shoot(brain.Visor - GetComponentInParent<EntityBodyPart>().GetRootPosition());
    }

    void Shoot(Vector3 direction) {
        direction.y = 0;
        direction.Normalize();
        Bullet newBullet = Instantiate(bulletprefab, transform);
        newBullet.gameObject.SetActive(false);
        newBullet.transform.position = transform.position;
        newBullet.Fire(direction);
        StartCooldown();
    }
}
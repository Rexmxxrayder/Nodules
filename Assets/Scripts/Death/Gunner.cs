using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : EntityEnemy {
    public Transform target;
    public string BulletType;
    public float ShootRate;
    public float BulletSpeed;

    IEnumerator Shoot() {
        while (true) {
            yield return new WaitForSeconds(ShootRate);
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            Bullet newBullet = (Bullet)BasicPools.Gino.GetInstance(BulletType);
            newBullet.transform.position = transform.position;
            newBullet.StartDirection = direction;
            newBullet.Activate();
        }
    }

    public override void InstanceReset() {
    }

    public override void InstanceResetSetup() {
        if (target == null) {
            target = FindObjectOfType<EntityBodyPart>().transform;
        }
        StartCoroutine(Shoot());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour {
    public Transform target;
    public string BulletType;
    public float ShootRate;
    public int damage;
    public float BulletSpeed;

    private void Start() {
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot() {
        while (true) {
            yield return new WaitForSeconds(ShootRate);
            Bullet newBullet = (Bullet)BasicPrefabs.Gino.GetInstance(BulletType);
            newBullet.transform.position = transform.position;
            newBullet.GetComponentInChildren<EntityPhysics>().Add(Force.Const((Vector2)(target.position - transform.position), BulletSpeed, 100), (int)EntityPhysics.PhysicPriority.PROJECTION);
            newBullet.damage = damage;
            newBullet.Activate();
        }
    }
}

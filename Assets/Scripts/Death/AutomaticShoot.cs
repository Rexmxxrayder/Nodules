using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class AutomaticShoot : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float shootRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] Timer shootTimer;

    private void Start() {
        shootTimer = new Timer(this, shootRate, Shoot).Start();
    }

    void Shoot() {
        Vector3 direction = target.position - transform.position;
        Basic newBullet = BasicPrefabs.Gino.GetInstance("Bullet");
        newBullet.transform.position = transform.position;
        newBullet.GetComponentInChildren<EntityPhysics>().Add(Force.Const(direction, bulletSpeed, Mathf.Infinity), (int)EntityPhysics.PhysicPriority.PROJECTION);
    }

}

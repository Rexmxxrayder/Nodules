using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class AutomaticShoot : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float shootRate;
    [SerializeField] Timer shootTimer;

    private void Start() {
        shootTimer = new Timer(shootRate, Shoot).Start();

    }

    void Shoot() {
        Vector3 direction = target.position - transform.position;
        Bullet newBullet =  Instantiate(BasicPrefabs.Gino.Bullet);
        newBullet.transform.position = transform.position;
        newBullet.Velocity = direction.normalized * 5;
    }

}

using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AreaDamage3D {
    [SerializeField] private float speed;
    [SerializeField] private float distanceLife = -1;
    private bool shoot;

    protected override void ResetSetup() {
        base.ResetSetup();
        shoot = false;
    }

    public void Fire(Vector3 direction) {
        if (shoot) {
            return;
        }
        shoot = true;
        gameObject.SetActive(true);
        RootGet<EntityPhysics>().Add(Force.Const(direction.normalized, speed, Duration), EntityPhysics.PhysicPriority.PROJECTION);
        StartCoroutine(DistanceLifeReach());
    }

    public void Fire(Vector3 direction, float speed) {
        this.speed = speed;
        Fire(direction);
    }

    public void Fire(Vector3 direction, float speed, float distanceLife) {
        this.distanceLife = distanceLife;
        Fire(direction,speed);
    }

    private IEnumerator DistanceLifeReach() {
        if (distanceLife < 0) {
            yield break;
        }

        float distance = 0;
        while (distance < distanceLife) {
            distance += Time.deltaTime * speed;
            yield return null;
        }

        GetRoot().Die();
    }
}
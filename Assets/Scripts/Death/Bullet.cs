using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : EntityBasic {
    public float Speed;
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
        RootGet<EntityPhysics>().Add(Force.Const(direction.normalized, Speed, 100), EntityPhysics.PhysicPriority.PROJECTION);
    }

    public void Fire(Vector3 direction, float speed) {
        Speed = speed;
        Fire(direction);
    }
}
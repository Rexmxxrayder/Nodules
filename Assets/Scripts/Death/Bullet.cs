using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : EntityBasic {
    public Vector3 StartDirection;
    public float BulletSpeed;


    protected override void StartSetup() {
        base.StartSetup();
        Get<EntityPhysics>().Add(Force.Const(StartDirection.normalized, BulletSpeed, 100), EntityPhysics.PhysicPriority.PROJECTION);
    }
}
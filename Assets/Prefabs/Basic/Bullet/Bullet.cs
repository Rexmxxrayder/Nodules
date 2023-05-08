using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : EntityBasic {
    public Vector3 StartDirection;
    public float BulletSpeed;
    [SerializeField] protected float lifeTime = 10f;
    protected Timer beforeDeath;


    public override void Activate() {
        Get<EntityPhysics>().Add(Force.Const(StartDirection.normalized, BulletSpeed, 100), EntityPhysics.PhysicPriority.PROJECTION);
        beforeDeath = new Timer(this, lifeTime, () => gameObject.Get<EntityHealth>().LethalDamage()).Start();
    }
}
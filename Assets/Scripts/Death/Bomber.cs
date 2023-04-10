using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : EntityEnemy {
    public Transform target;
    public string areaDamage;
    public float TimeBeforeExplode;
    float timerBeforeExplode = 0;
    public float Speed;
    EntityPhysics ep;
    EntityCollider2D ec;
    Force follow = Force.Const(Vector3.zero, 0f);

    void Update() {
        ep.Remove(follow);
        follow = Force.Const(target.position - transform.position, Speed, Vector3.Distance(target.position, transform.position) / Speed);
        ep.Add(follow, (int)EntityPhysics.PhysicPriority.PLAYER_INPUT);
        timerBeforeExplode += Time.deltaTime;
        if(timerBeforeExplode > TimeBeforeExplode) {
            gameObject.Get<EntityHealth>().LethalDamage();
        }
    }

    void MustExplode(Collision2D c) {
        if (!c.gameObject.CompareTag("Player"))
            return;
        gameObject.Get<EntityHealth>().LethalDamage();
    }
    void Explode() {
        AreaDamage ad = (AreaDamage)BasicPools.Gino.GetInstance(areaDamage);
        ad.transform.position = transform.position;
        ad.Activate();
    }

    protected override void AwakeSetup() {
        ep = gameObject.Get<EntityPhysics>();
        ec = gameObject.Get<EntityCollider2D>();
    }

    public override void InstanceReset() {
        timerBeforeExplode = 0;
    }

    public override void InstanceResetSetup() {
        gameObject.Get<EntityHealth>().OnDeath += Explode;
        ec.OnCollisionEnter += MustExplode;
        if (target == null) {
            target = FindObjectOfType<EntityBodyPart>().transform;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Ennemi {
    public Transform target;
    public string areaDamage;
    public float TimeBeforeExplode;
    float timerBeforeExplode = 0;
    public float Speed;
    EntityPhysics ep;
    EntityCollider2D ec;
    Force follow;

    private void Start() {
        Activate();
    }

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
        AreaDamage ad = (AreaDamage)BasicPrefabs.Gino.GetInstance(areaDamage);
        ad.transform.position = transform.position;
        ad.Activate();
    }

    public override void Activate() {
        ep = gameObject.GetComponentInChildren<EntityPhysics>();
        ec = gameObject.Get<EntityCollider2D>();
        gameObject.Get<EntityHealth>().OnDeath += Explode;
        ec.OnCollisionEnter += MustExplode;
        follow = Force.Const(Vector3.zero, 0f);
        timerBeforeExplode = 0;
        if (target == null) {
            target = FindObjectOfType<Body>().transform;
        }
    }
}

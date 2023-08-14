using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class MinionBrain : EntityBrain{
    public Transform target;
    public float projectionStrenght, projectionDist;
    [SerializeField] private EntityBodyPart follow;

    protected override void ResetSetup() {
        if (target == null) {
            target = FindObjectOfType<PlayerBrain>().transform;
        }
        RootGet<EntityCollider2D>().OnCollisionEnterDelegate += Hit;
    }

    private void Update() {
        visor = target.transform.position;
        if (follow.Available) {
            follow.KeyEvenement(true);
        }
    }

    void Hit(Collision2D c) { 
        EntityPhysics ep = c.gameObject.RootGet<EntityPhysics>();
        if (ep != null) {
            ep.Add(Force.Const(c.transform.position - transform.position, projectionStrenght, projectionDist / projectionStrenght), EntityPhysics.PhysicPriority.PROJECTION);
        }
    }
}

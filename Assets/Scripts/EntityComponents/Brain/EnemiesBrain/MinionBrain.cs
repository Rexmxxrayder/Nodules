using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class MinionBrain : EntityBrain {
    public float projectionStrenght, projectionDist;
    [SerializeField] private EntityBodyPart follow;

    protected override void LoadSetup() {
        base.LoadSetup();
        selected = PlayerBrain.Transform;
        RootGet<EntityCollider3D>().OnCollisionEnterDelegate += Hit;
    }

    private void Update() {
        visor = selected.transform.position;
        if (follow.Available) {
            follow.KeyEvenement(true);
        }
    }

    void Hit(Collision c) {
        EntityPhysics ep = c.gameObject.RootGet<EntityPhysics>();
        if (ep != null) {
            ep.Add(Force.Const(c.transform.position - transform.position, projectionStrenght, projectionDist / projectionStrenght), EntityPhysics.PhysicPriority.PROJECTION);
        }
    }
}

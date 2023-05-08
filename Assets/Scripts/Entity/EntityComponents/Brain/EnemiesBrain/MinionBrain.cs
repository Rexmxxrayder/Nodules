using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class MinionBrain : EntityBrain, IReset {
    public Transform target;
    public float projectionStrenght, projectionDist;
    public void InstanceReset() {

    }

    public void InstanceResetSetup() {
        if (target == null) {
            target = FindObjectOfType<PlayerBrain>().transform;
        }
        Get<EntityCollider2D>().OnCollisionEnter += Hit;
    }

    private void Update() {
        visor = target.transform.position;
        if (Get<EntityBodyParts>().Bodyparts[0].Available) {
            Get<EntityBodyParts>().Bodyparts[0].OnButtonUp(this);
        }
    }

    void Hit(Collision2D c) { 
        EntityPhysics ep = c.gameObject.Get<EntityPhysics>();
        if (ep != null) {
            ep.Add(Force.Const(c.transform.position - transform.position, projectionStrenght, projectionDist / projectionStrenght), EntityPhysics.PhysicPriority.PROJECTION);
            Get<EntityDamageCollider2D>().ResetHit();
            c.gameObject.Get<EntityBodyParts>().onMovement?.Invoke();
        }
    }
}

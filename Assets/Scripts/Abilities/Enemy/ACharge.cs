using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class ACharge : Ability {
    public float Speed;
    public float Dist;
    public float projectionStrenght, projectionDist;
    Force chargeForce;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        EntityPhysics ep = GetComponentInParent<EntityBodyPart>().GetRootComponent<EntityPhysics>();
        Vector3 startPosition = GetComponentInParent<EntityBodyPart>().GetRootPosition();
        Vector3 goToPosition = brain.Visor;
        ChargeTo(ep, startPosition, goToPosition);
    }

    void ChargeTo(EntityPhysics ep, Vector3 startPosition, Vector3 goToPosition) {
        ep.Remove(chargeForce);
        Vector3 direction = goToPosition - startPosition;
        direction.Normalize();
        chargeForce = Force.Const(direction, Speed, Dist / Speed);
        chargeForce.OnEnd += (collision) => StopCharge();
        gameObject.GetRootComponent<EntityMainCollider2D>().OnCollisionEnterDelegate += (collision) => Hit(collision);
        gameObject.GetRootComponent<EntityMainCollider2D>().OnCollisionEnterDelegate += (collision) => chargeForce.End();
        gameObject.GetRoot().transform.rotation = Quaternion.Euler(0, 0, RotationSloot.GetDegreeBasedOfTarget(startPosition, goToPosition, Vector3.up) - 70);
        ep.Add(chargeForce, EntityPhysics.PhysicPriority.DASH);
    }

    void StopCharge() {
        gameObject.GetRootComponent<EntityMainCollider2D>().OnCollisionEnterDelegate -= (collision) => StopCharge();
        gameObject.GetRootComponent<EntityMainCollider2D>().OnCollisionEnterDelegate -= (collision) => Hit(collision);
        gameObject.GetRootComponent<EntityPhysics>().Remove(chargeForce);
    }

    void Hit(Collision2D c) {
        EntityPhysics ep = c.gameObject.GetRootComponent<EntityPhysics>();
        if (ep != null) {
            ep.Add(Force.Const(c.transform.position - transform.position, projectionStrenght, projectionDist / projectionStrenght), EntityPhysics.PhysicPriority.PROJECTION);
        }
    }
}

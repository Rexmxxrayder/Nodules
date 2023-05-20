using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class ACharge : Ability {
    public float Speed;
    public float Dist;
    public float projectionStrenght, projectionDist;
    Force chargeForce;

    protected override void LaunchAbility(EntityBrain brain) {
        GetComponentInParent<EntityBodyParts>().onMovement?.Invoke();
        EntityPhysics ep = GetComponentInParent<EntityBodyParts>().Get<EntityPhysics>();
        Vector3 startPosition = GetComponentInParent<EntityBodyParts>().GetRoot().transform.position;
        Vector3 goToPosition = brain.Visor;
        ChargeTo(ep, startPosition, goToPosition);
    }

    void ChargeTo(EntityPhysics ep, Vector3 startPosition, Vector3 goToPosition) {
        GetComponentInParent<EntityBodyParts>().isDashing = true;
        ep.Remove(chargeForce);
        Vector3 direction = goToPosition - startPosition;
        direction.Normalize();
        chargeForce = Force.Const(direction, Speed, Dist / Speed);
        chargeForce.OnEnd += (Force) => GetComponentInParent<EntityBodyParts>().isDashing = false;
        chargeForce.OnEnd += (collision) => StopCharge();
        gameObject.Get<EntityMainCollider2D>().OnCollisionEnter += (collision) => Hit(collision);
        gameObject.Get<EntityMainCollider2D>().OnCollisionEnter += (collision) => chargeForce.End();
        gameObject.GetRoot().transform.rotation = Quaternion.Euler(0, 0, RotationSloot.GetDegreeBasedOfTarget(startPosition, goToPosition, Vector3.up) - 70);
        ep.Add(chargeForce, EntityPhysics.PhysicPriority.DASH);
    }

    void StopCharge() {
        gameObject.Get<EntityMainCollider2D>().OnCollisionEnter -= (collision) => StopCharge();
        gameObject.Get<EntityMainCollider2D>().OnCollisionEnter -= (collision) => Hit(collision);
        gameObject.Get<EntityPhysics>().Remove(chargeForce);
    }

    void Hit(Collision2D c) {
        EntityPhysics ep = c.gameObject.Get<EntityPhysics>();
        if (ep != null) {
            ep.Add(Force.Const(c.transform.position - transform.position, projectionStrenght, projectionDist / projectionStrenght), EntityPhysics.PhysicPriority.PROJECTION);
            c.gameObject.Get<EntityBodyParts>().onMovement?.Invoke();
        }
    }
}

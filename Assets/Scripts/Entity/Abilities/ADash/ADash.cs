using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADash : Ability {
    public float Speed;
    public float Dist;
    Force dashForce;

    protected override void LaunchAbility(EntityBrain brain) {
        GetComponentInParent<EntityBodyParts>().onMovement?.Invoke();
        EntityPhysics ep = GetComponentInParent<EntityBodyParts>().Get<EntityPhysics>();
        Vector3 startPosition = GetComponentInParent<EntityBodyParts>().GetRoot().transform.position;
        Vector3 goToPosition = brain.Visor;
        DashTo(ep, startPosition, goToPosition);
    }

    void DashTo(EntityPhysics ep, Vector3 startPosition, Vector3 goToPosition) {
        GetComponentInParent<EntityBodyParts>().isDashing = true;
        ep.Remove(dashForce);
        dashForce = Force.Const(goToPosition - startPosition, Speed, Dist / Speed);
        dashForce.OnEnd += (Force) => GetComponentInParent<EntityBodyParts>().isDashing = false;
        ep.Add(dashForce, EntityPhysics.PhysicPriority.DASH); 
    }
}

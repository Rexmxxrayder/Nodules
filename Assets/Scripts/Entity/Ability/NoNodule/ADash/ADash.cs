using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADash : Ability {
    public float Speed;
    public float Dist;
    Force dashForce;

    protected override void LaunchAbility(EntityBrain brain) {
        entityBodyPart.onMovement?.Invoke();
        EntityPhysics ep = entityBodyPart.Get<EntityPhysics>();
        Vector3 startPosition = entityBodyPart.GetRoot().transform.position;
        Vector3 goToPosition = brain.Visor;
        DashTo(ep, startPosition, goToPosition);
    }

    void DashTo(EntityPhysics ep, Vector3 startPosition, Vector3 goToPosition) {
        entityBodyPart.isDashing = true;
        ep.Remove(dashForce);
        dashForce = Force.Const(goToPosition - startPosition, Speed, Dist / Speed);
        dashForce.OnEnd += (Force) => entityBodyPart.isDashing = false;
        ep.Add(dashForce, (int)EntityPhysics.PhysicPriority.DASH);
        
    }
}

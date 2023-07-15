using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADash : Ability {
    public float Speed;
    public float Dist;
    Force dashForce;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        EntityPhysics ep = gameObject.RootGet<EntityPhysics>();
        Vector3 startPosition = gameObject.GetRootPosition();
        Vector3 goToPosition = brain.Visor;
        DashTo(ep, startPosition, goToPosition);
    }

    void DashTo(EntityPhysics ep, Vector3 startPosition, Vector3 goToPosition) {
        ep.Remove(dashForce);
        dashForce = Force.Const(goToPosition - startPosition, Speed, Dist / Speed);
        ep.Add(dashForce, EntityPhysics.PhysicPriority.DASH); 
    }
}

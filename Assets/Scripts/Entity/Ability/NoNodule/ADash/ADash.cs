using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADash : Ability {
    public float Speed;
    public float Dist;
    [SerializeField] Force dashForce;

    protected override void LaunchAbility(EntityBrain brain) {
        EntityPhysics ep = entityBodyPart.Get<EntityPhysics>();
        Vector3 startPosition = entityBodyPart.GetRoot().transform.position;
        Vector3 goToPosition = brain.Visor;
        entityBodyPart.onMovement?.Invoke();
        DashTo(ep, startPosition, goToPosition);
    }

    void DashTo(EntityPhysics ep, Vector3 startPosition, Vector3 goToPosition) {
        ep.Remove(dashForce);
        dashForce = Force.Const(goToPosition - startPosition, Speed, Dist / Speed);
        ep.Add(dashForce, (int)EntityPhysics.PhysicPriority.DASH);
    }
}

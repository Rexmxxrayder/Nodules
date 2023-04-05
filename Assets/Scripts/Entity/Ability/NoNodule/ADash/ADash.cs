using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADash : Ability {
    public float Speed;
    public float Dist;
    [SerializeField] Force dashForce;

    protected override void LaunchAbility((Brain, Body) bodyBrain) {
        EntityPhysics ep = bodyBrain.Item2.gameObject.Get<EntityPhysics>();
        Vector3 startPosition = bodyBrain.Item2.transform.position;
        Vector3 goToPosition = bodyBrain.Item1.Visor;
        DashTo(ep, startPosition, goToPosition);
    }

    void DashTo(EntityPhysics ep, Vector3 startPosition, Vector3 goToPosition) {
        ep.Remove(dashForce);
        dashForce = Force.Const(goToPosition - startPosition, Speed, Dist / Speed);
        ep.Add(dashForce, (int)EntityPhysics.PhysicPriority.DASH);
    }
}

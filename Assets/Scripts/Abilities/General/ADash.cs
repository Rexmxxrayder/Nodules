using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADash : Ability {
    [SerializeField] private float Speed;
    [SerializeField] private float Dist;
    [SerializeField] private AllCollider stopDash;
    private Force dashForce;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        EntityPhysics ep = gameObject.GetRootComponent<EntityPhysics>();
        Vector3 startPosition = gameObject.GetRootPosition();
        Vector3 goToPosition = brain.Visor;
        DashTo(ep, startPosition, goToPosition);
    }

    void DashTo(EntityPhysics ep, Vector3 startPosition, Vector3 goToPosition) {
        ep.Remove(dashForce);
        AllCollider stop = Instantiate(stopDash, ep.GetRootTransform());
        dashForce = Force.Const(goToPosition - startPosition, Speed, Dist / Speed);
        dashForce.OnEnd += (_) => {
            Destroy(stop.gameObject);
            StartCooldown();
        };
        stop.OnContact += StopIfEnemy;
        ep.Add(dashForce, EntityPhysics.PhysicPriority.DASH); 
    }

    private void StopIfEnemy(Collider collider) {
        if (collider.CompareTag("Enemy")) {
            dashForce.End();
        }
    }
}

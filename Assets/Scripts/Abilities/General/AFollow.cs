using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFollow : Ability {
    public float Speed;
    public float Time;
    public float MinimunDistance = 0.05f;
    [SerializeField] Force FollowForce = Force.Const(Vector3.zero, 0);
    EntityPhysics ep;
    Transform root;
    Transform toFollow;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        ep = gameObject.RootGet<EntityPhysics>();
        root = gameObject.GetRoot().transform;
        toFollow = brain.Selected;
        StopFollow();
        StartCoroutine(Follow(ep, root, toFollow));
    }

    IEnumerator Follow(EntityPhysics ep, Transform root, Transform goToPosition) {
        FollowForce = new(Force.Const(goToPosition.position - root.position, Speed, Time));
        ep.Add(FollowForce, EntityPhysics.PhysicPriority.INPUT);
        do {
            if(Vector3.Distance(goToPosition.position.PlanXZ(), root.position.PlanXZ()) <= MinimunDistance) {
                FollowForce.Direction = Vector3.zero;
            } else {
                Vector3 direction = goToPosition.position - root.position;
                FollowForce.Direction = direction.PlanXZ();
            }

            yield return null;
        } while (!FollowForce.HasEnded);
        StopFollow();
    }

    public void StopFollow() {
        ep.Remove(FollowForce);
        StopAllCoroutines();
    }

    public override void Cancel() {
        StopFollow();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWalk : Ability {
    public float Speed;
    public float MinimumDistance;
    [SerializeField] Force walkForce = Force.Const(Vector3.zero, 0);
    EntityPhysics ep;
    Transform root;
    Vector3 goToPosition;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        ep = gameObject.RootGet<EntityPhysics>();
        root = gameObject.GetRoot().transform;
        goToPosition = brain.Visor;
        StopWalk();

        StartCoroutine(WalkTo(ep, root, goToPosition));
    }

    IEnumerator WalkTo(EntityPhysics ep, Transform root, Vector3 goToPosition) {
        walkForce = new(Force.Const(goToPosition - root.position, Speed, 100));
        ep.Add(walkForce, EntityPhysics.PhysicPriority.INPUT);
        do {
            if (Vector3.Distance(goToPosition, root.position) < MinimumDistance) {
                yield break;
            }

            walkForce.Direction = goToPosition - root.position;
            yield return null;
        } while (!walkForce.HasEnded);
        StopWalk();
    }

    public void StopWalk() {
        ep.Remove(walkForce);
        StopAllCoroutines();
    }
}

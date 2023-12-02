using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWalk : Ability {
    public float Speed;
    public float MinimumDistance;
    [SerializeField] Force walkForce = Force.Const(Vector3.zero, 0);
    EntityPhysics ep;
    Transform root;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        ep = gameObject.GetRootComponent<EntityPhysics>();
        root = gameObject.GetRootTransform();
        StopWalk();
        StartCoroutine(WalkTo(ep, root, brain.Visor));
    }

    IEnumerator WalkTo(EntityPhysics ep, Transform root, Vector3 goToPosition) {
        Vector3 direction = goToPosition - root.position;
        direction.y = 0;
        walkForce = new(Force.Const(direction, Speed, 100));
        ep.Add(walkForce, EntityPhysics.PhysicPriority.INPUT);
        do {
            if (Vector3.Distance(goToPosition, root.position) < MinimumDistance) {
                StopWalk();
                yield break;
            }
            direction = goToPosition - root.position;
            direction.y = 0;
            walkForce.Direction = direction;
            yield return null;
        } while (!walkForce.HasEnded);
    }

    public void StopWalk() {
        ep.Remove(walkForce);
        StopAllCoroutines();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWalk : Ability {
    public float Speed;
    public float MinimumDistance;
    [SerializeField] Force walkForce = Force.Const(Vector3.zero, 0);
    [SerializeField] Coroutine coroutine;

    private void Start() {
        entityBodyPart.OnMovement += StopWalk;
    }

    protected override void LaunchAbility(EntityBrain brain) {
        EntityPhysics ep = entityBodyPart.Get<EntityPhysics>();
        Transform root = entityBodyPart.GetRoot().transform;
        Vector3 goToPosition = brain.Visor;
        StopWalk();
        coroutine = StartCoroutine(WalkTo(ep, root, goToPosition));
    }

    IEnumerator WalkTo(EntityPhysics ep, Transform root, Vector3 goToPosition) {
        do {
            if (Vector3.Distance(goToPosition, root.position) < MinimumDistance) {
                yield break;
            }
            ep.Remove(walkForce);
            walkForce = Force.Const(goToPosition - root.position, Speed, Vector3.Distance(goToPosition, root.position) / Speed);
            ep.Add(walkForce, (int)EntityPhysics.PhysicPriority.PLAYER_INPUT);
            yield return null;
        } while (!walkForce.HasEnded);
        ep.Remove(walkForce);
    }

    public void StopWalk() {
        entityBodyPart.Get<EntityPhysics>().Remove(walkForce);
        if (coroutine != null) {
            StopCoroutine(coroutine);
        }
    }
}

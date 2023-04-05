using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWalk : Ability {
    public float Speed;
    [SerializeField] Force walkForce;
    [SerializeField] Coroutine coroutine;

    protected override void LaunchAbility((Brain, Body) bodyBrain) {
        EntityPhysics ep = bodyBrain.Item2.gameObject.Get<EntityPhysics>();
        Transform bodyTransform = bodyBrain.Item2.transform;
        Vector3 goToPosition = bodyBrain.Item1.Visor;
        if(coroutine!= null) {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(WalkTo(ep, bodyTransform, goToPosition));
    }

    IEnumerator WalkTo(EntityPhysics ep, Transform bodyTransform, Vector3 goToPosition) {
        do {
            ep.Remove(walkForce);
            walkForce = Force.Const(goToPosition - bodyTransform.position, Speed, Vector3.Distance(goToPosition, bodyTransform.position) / Speed);
            ep.Add(walkForce, (int)EntityPhysics.PhysicPriority.PLAYER_INPUT);
            yield return null;
        } while (!walkForce.HasEnded);
    }
}

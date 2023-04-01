using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWalk : Ability {
    public float Speed;
    public Rigidbody2D rb;
    Coroutine coroutine;

    protected override void LaunchAbility((Brain, Body) bodyBrain) {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(Walk(bodyBrain.Item2.transform, bodyBrain.Item1.Visor));
    }

    IEnumerator Walk(Transform bodyT, Vector3 goToPosition) {
        while (Vector3.Distance(bodyT.position, goToPosition) > 1f) {
            rb.velocity = (goToPosition - bodyT.position).normalized * Speed;
            yield return null;
        }
        rb.velocity = Vector3.zero;
    }
}

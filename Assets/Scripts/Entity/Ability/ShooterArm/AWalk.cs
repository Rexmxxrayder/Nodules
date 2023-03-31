using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWalk : Ability {
    public float Speed;
    public Rigidbody2D rb;
    protected override void LaunchAbility((Brain, Body) bodyBrain) {
        StartCoroutine(Walk(bodyBrain.Item2.transform, bodyBrain.Item1.Visor));
    }

    IEnumerator Walk(Transform bodyT, Vector3 goToPosition) {
        while (Vector3.Distance(bodyT.position, goToPosition) > 1f) {
            Debug.Log(Vector3.Distance(bodyT.position, goToPosition));
            Debug.Log(rb.velocity);
            rb.velocity = (goToPosition - bodyT.position).normalized * Speed;
            yield return null;
        }
        rb.velocity = Vector3.zero;
    }
}

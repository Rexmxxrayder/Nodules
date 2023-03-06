using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyLegs : BodyPart {
    public float Speed;
    Rigidbody2D rb;
    Coroutine gotoo;

    private void Start() {
        rb = body.GetComponent<Rigidbody2D>();
    }
    protected override void NewBody(Body newBody) {
        body = newBody;
        brain = newBody.brain;
        brain.NewMovePoint += Gotooo;
    }

    void Gotooo(Vector2 goToPosition) {
        if(gotoo != null)
        StopCoroutine(gotoo);
        gotoo = StartCoroutine(Goto(goToPosition));
    }
    IEnumerator Goto(Vector2 goToPosition) {
        if (brain == null) { yield break; }
        while (Vector3.Distance(body.transform.position, goToPosition) > 1f) {
            Debug.Log(Vector3.Distance(body.transform.position, goToPosition));
            rb.velocity = ((Vector3)goToPosition - body.transform.position).normalized * Speed;
            yield return null;
        }
        rb.velocity = Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mia : EntityBasic {
    public override void Activate() {

    }

    void Boing(Collision c) {
        if (!c.gameObject.CompareTag("Wall")) {
            return;
        }
        //Vector2 velocity = Get<EntityPhysics>().Velocity;
        //float x = Mathf.Abs(c.contacts[0].point.x - _root.transform.position.x);
        //float y = Mathf.Abs(c.contacts[0].point.y - _root.transform.position.y);
        //if (x < y) {
        //    x
        //} else {

        //}
    }
}

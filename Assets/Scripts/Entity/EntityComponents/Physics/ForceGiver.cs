using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGiver : MonoBehaviour {

    public bool give;
    public EntityPhysics ep;
    public Vector2 direction;
    public float strength;
    public float duration;
    public float weight;
    public int priority;

    private void Update() {
        if(give) {
            give = false;
            ep.Add(Force.LinearTriangleUp(direction, strength, duration, weight), priority);
        }
    }
}

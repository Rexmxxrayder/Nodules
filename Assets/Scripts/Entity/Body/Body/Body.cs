using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {
    [SerializeField] public string bodyName;
    [SerializeField] public List<BodyPart> bodyparts = new List<BodyPart>();
    [SerializeField] public EntityPhysics entityPhysics;
    private void Awake() {
        for (int i = 0; i < bodyparts.Count; i++) {
            bodyparts[i].body = this;
        }
    }
}

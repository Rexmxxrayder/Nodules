using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {
    [SerializeField] public string bodyName;
    public List<BodyPart> bodyparts = new List<BodyPart>();
    private void Awake() {
        foreach (BodyPart bp in GetComponentsInChildren<BodyPart>()) {
            bodyparts.Add(bp);
            bp.body = this;
        }
    }
}

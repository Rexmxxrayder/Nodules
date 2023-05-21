using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class SlasherBrain : EntityBrain {
    public Transform target;

    protected override void StartSetup() {
        if (target == null) {
            target = FindObjectOfType<PlayerBrain>().transform;
        }
    }

    private void Update() {
        visor = target.transform.position;
        if (Get<EntityBodyParts>().Bodyparts[0].Available) {
            Get<EntityBodyParts>().Bodyparts[0].OnButtonUp(this);
        }
    }
}

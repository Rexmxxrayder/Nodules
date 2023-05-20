using Sloot;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SauteurBrain : EntityBrain {
    public Transform target;

    public override void InstanceResetSetup() {
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

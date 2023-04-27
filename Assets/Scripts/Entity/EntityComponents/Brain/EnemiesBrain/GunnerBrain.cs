using System.Collections;
using UnityEngine;
using Sloot;

public class GunnerBrain : EntityBrain, IReset {
    public Transform target;

    public void InstanceReset() {
    }

    public void InstanceResetSetup() {
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

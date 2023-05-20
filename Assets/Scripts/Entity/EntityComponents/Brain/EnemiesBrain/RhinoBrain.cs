using UnityEngine;
using Sloot;

public class RhinoBrain : EntityBrain, IReset {
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


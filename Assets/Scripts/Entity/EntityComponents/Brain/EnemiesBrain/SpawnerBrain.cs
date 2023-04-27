using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class SpawnerBrain : EntityBrain, IReset {

    public void InstanceReset() {
    }

    public void InstanceResetSetup() {

    }

    private void Update() {
        if (Get<EntityBodyParts>().Bodyparts[0].Available) {
            Get<EntityBodyParts>().Bodyparts[0].OnButtonUp(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class SlasherBrain : EntityBrain {
    [SerializeField] private EntityBodyPart slash;

    protected override void LoadSetup() {
        base.LoadSetup();
        selected = PlayerBrain.Transform;
    }

    private void Update() {
        visor = selected.transform.position;
        if (slash.Available) {
            slash.KeyEvenement(true);
        }
    }
}

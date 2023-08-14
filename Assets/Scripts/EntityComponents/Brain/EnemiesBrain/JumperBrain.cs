using Sloot;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumperBrain : EntityBrain {
    [SerializeField] EntityBodyPart jump;

    protected override void LoadSetup() {
        base.LoadSetup();
        selected = HMove.instance.transform;
    }

    private void Update() {
        visor = selected.transform.position;
        if (jump.Available) {
            jump.KeyEvenement(true);
        }
    }
}

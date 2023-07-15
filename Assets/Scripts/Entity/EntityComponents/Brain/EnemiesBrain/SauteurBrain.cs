using Sloot;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SauteurBrain : EntityBrain {
    public Transform target;
    [SerializeField] EntityBodyPart jump;

    protected override void ResetSetup() {
        if (target == null) {
            target = FindObjectOfType<PlayerBrain>().transform;
        }
    }

    private void Update() {
        visor = target.transform.position;
        if (jump.Available) {
            jump.Activate(true);
        }
    }
}

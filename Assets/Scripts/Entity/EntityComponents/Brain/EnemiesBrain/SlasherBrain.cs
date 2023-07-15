using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class SlasherBrain : EntityBrain {
    public Transform target;
    [SerializeField] private EntityBodyPart slash;

    protected override void ResetSetup() {
        if (target == null) {
            target = FindObjectOfType<PlayerBrain>().transform;
        }
    }

    private void Update() {
        visor = target.transform.position;
        if (slash.Available) {
            slash.Activate(true);
        }
    }
}

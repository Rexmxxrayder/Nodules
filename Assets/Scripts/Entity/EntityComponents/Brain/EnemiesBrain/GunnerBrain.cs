using System.Collections;
using UnityEngine;
using Sloot;

public class GunnerBrain : EntityBrain {
    public Transform target;
    [SerializeField] EntityBodyPart shoot;
    protected override void ResetSetup() {
        if (target == null) {
            target = FindObjectOfType<PlayerBrain>().transform;
        }
    }


    private void Update() {
        visor = target.transform.position;
        if (shoot.Available) {
            shoot.Activate(true);
        }
    }
}

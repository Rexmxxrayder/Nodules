using UnityEngine;
using Sloot;

public class RhinoBrain : EntityBrain {
    public Transform target;
    [SerializeField] private EntityBodyPart charge;
    protected override void ResetSetup() {
        if (target == null) {
            target = FindObjectOfType<PlayerBrain>().transform;
        }
    }

    private void Update() {
        visor = target.transform.position;
        if (charge.Available) {
            charge.KeyEvenement(true);
        }
    }
}


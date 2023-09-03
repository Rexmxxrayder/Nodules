using System.Collections;
using UnityEngine;
using Sloot;

public class GunnerBrain : EntityBrain {
    [SerializeField] EntityBodyPart shoot;

    protected override void LoadSetup() {
        base.LoadSetup();
        selected = PlayerBrain.Transform;
    }

    private void Update() {
        visor = selected.transform.position;
        if (shoot.Available) {
            shoot.KeyEvenement(true);
        }
    }
}

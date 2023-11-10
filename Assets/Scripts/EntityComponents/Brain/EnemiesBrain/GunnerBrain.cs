using UnityEngine;

public class GunnerBrain : EntityBrain {
    [SerializeField] private GunnerData GunnerData;
    private EntityBodyPart shoot;

    protected override void ResetSetup() {
        base.ResetSetup();
        shoot = GetComponentInChildren<EntityBodyPart>();
        GunnerData.SetupData(this);
    }

    protected override void LoadSetup() {
        base.LoadSetup();
        selected = PlayerBrain.Transform;
    }

    private void Update() {
        visor = selected.transform.position;
        if (shoot.Available && CanAct) {
            shoot.KeyEvenement(true);
        }
    }
}

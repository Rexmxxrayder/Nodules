using UnityEngine;

public class SlasherBrain : EntityBrain {
    [SerializeField] private SlasherData slasherData;
    private EntityBodyPart slash;

    protected override void ResetSetup() {
        base.ResetSetup();
        slash.GetComponentInChildren<EntityBodyPart>();
        slasherData.SetupData(this);
    }

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

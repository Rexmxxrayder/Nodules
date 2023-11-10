using UnityEngine;

public class JumperBrain : EntityBrain {
    [SerializeField] private JumperData jumperData;
    private EntityBodyPart jump;

    protected override void ResetSetup() {
        base.ResetSetup();
        jump = GetComponentInChildren<EntityBodyPart>();
        jumperData.SetupData(this);
    }

    protected override void LoadSetup() {
        base.LoadSetup();
        selected = PlayerBrain.Transform;
    }

    private void Update() {
        visor = selected.transform.position;
        if (jump.Available) {
            jump.KeyEvenement(true);
        }
    }
}

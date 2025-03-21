using UnityEngine;

public class SpawnerBrain : EntityBrain {
    [SerializeField] private SpawnerData spawnerData;
    private EntityBodyPart spawn;

    protected override void ResetSetup() {
        base.ResetSetup();
        spawnerData.SetupData(this);
        spawn = GetComponentInChildren<EntityBodyPart>();
    }

    private void Update() {
        if (spawn.Available) {
            spawn.KeyEvenement(true);
        }
    }
}

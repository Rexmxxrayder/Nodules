using UnityEngine;
using Sloot;
using System.Collections.Generic;

public class ASpawnEnemy : Ability {
    public List<EntityRoot> ennemiesPrefabs;
    private EntityRoot prefabChosen;
    public float distSpawn;
    public int NumberSpawn;

    protected override void Awake() {
        base.Awake();
        prefabChosen = ennemiesPrefabs[Random.Range(0, ennemiesPrefabs.Count)];
    }

    protected override void LaunchAbilityUp(EntityBrain brain) {
        Vector3 BodyPosition = gameObject.GetRootPosition();
        for (int i = 0; i < NumberSpawn; i++) {
            Vector3 direction = RotationSloot.GetDirectionOnAxis(360 / NumberSpawn * i, Vector3.up);
            Vector3 ennemiPosition = BodyPosition + direction * distSpawn;
            Spawn(ennemiPosition);
        }

        StartCooldown();
    }

    public void Spawn(Vector3 position) {
        EntityRoot ennemi = Instantiate(prefabChosen);
        ennemi.transform.position = position;
    }
}

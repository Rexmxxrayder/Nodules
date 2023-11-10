using UnityEngine;
using System.Collections.Generic;

public class ASpawnEnemy : Ability {
    private EntityRoot prefabChosen;
    [SerializeField] private List<EntityRoot> ennemiesPrefabs;
    [SerializeField] private float maxSpawnDist;
    [SerializeField] private int ennemiesSpawnByUse;

    public void SetupData(float cooldown, List<EntityRoot> ennemiesPrefabs, float maxSpawnDist, int ennemiesSpawnByUse) {
        this.cooldown = cooldown;
        this.ennemiesPrefabs = ennemiesPrefabs;
        this.maxSpawnDist = maxSpawnDist;
        this.ennemiesSpawnByUse = ennemiesSpawnByUse;
    }

    protected override void LaunchAbilityUp(EntityBrain brain) {
        if(prefabChosen == null) {
            prefabChosen = ennemiesPrefabs[Random.Range(0, ennemiesPrefabs.Count)];
        }
   
        int spawnDone = 0;
        int security = 0;
        while (spawnDone < ennemiesSpawnByUse && security < 30) {
            if (Spawn(gameObject.GetRootPosition() + Random.insideUnitSphere.normalized * maxSpawnDist)) {
                spawnDone++;
            }

            security++;
        }

        StartCooldown();
    }

    public bool Spawn(Vector3 position) {
        position.y = 0;
        Collider[] colliders = Physics.OverlapSphere(position, 2);
        foreach (Collider collider in colliders) {
            if (collider.transform.CompareTag("Wall")) {
                return false;
            }
        }

        RaycastHit[] hits = Physics.RaycastAll(position, Vector3.down * 5);
        bool hitGround = false;
        foreach (RaycastHit hit in hits) {
            if (hit.collider.transform.CompareTag("Ground")) {
                hitGround = true;
            }
        }

        if (!hitGround) {
            return false;
        }

        EntityRoot ennemi = Instantiate(prefabChosen);
        ennemi.transform.position = position;
        return true;
    }
}

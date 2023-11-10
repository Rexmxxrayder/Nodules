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
        int spawnDone = 0;
        int security = 0;
        while (spawnDone < NumberSpawn && security < 30) {
            if (Spawn(brain.transform.position + Random.insideUnitSphere.normalized * distSpawn)) {
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

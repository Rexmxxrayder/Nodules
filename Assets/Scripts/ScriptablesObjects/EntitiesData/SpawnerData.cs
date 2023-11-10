using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerData", menuName = "ScriptableObjects/EnnemiesData/SpawnerData")]
public class SpawnerData : ScriptableObject
{
    [Header("ASpawnEnemy")]
    [SerializeField] private float cooldown, maxSpawnDist;
    [SerializeField] private int ennemiesSpawnByUse;
    [SerializeField] private List<EntityRoot> ennemiesPrefabs;
    public void SetupData(SpawnerBrain entityBrain) {
        entityBrain.GetComponentInChildren<ASpawnEnemy>().SetupData(cooldown, ennemiesPrefabs, maxSpawnDist, ennemiesSpawnByUse);
    }
}

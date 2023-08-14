using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] public GameObject Spawn; 
    [SerializeField] public static int SpawnNumber; 
    [SerializeField] public static GameObject[] Enemies;
    [SerializeField] private int spawnNumber;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private List<GameObject> currentEnemies = new();
    [SerializeField] private Transform[] enemiesSpawnPoints;

    private void Awake() {
        if(spawnNumber != 0) {
            SpawnNumber = spawnNumber;
        }

        if (enemies.Length != 0) {
            Enemies = enemies;
        }

        enemiesSpawnPoints = new Transform[transform.GetChild(1).childCount];
        for (int i = 0; i < transform.GetChild(1).childCount; i++) {
            enemiesSpawnPoints[i] = transform.GetChild(1).GetChild(i);
        }
    }

    private void Start() {
        for (int i = 0; i < SpawnNumber; i++) {
            currentEnemies.Add(Instantiate(Enemies[Random.Range(0, Enemies.Length - 1)], enemiesSpawnPoints[Random.Range(0, enemiesSpawnPoints.Length - 1)].transform.position, Quaternion.identity));
        }
    }

    private void OnDestroy() {
        for (int i = 0; i < currentEnemies.Count; i++) {
            Destroy(currentEnemies[i]);
        }
    }
}

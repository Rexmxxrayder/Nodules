using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour {
    [SerializeField] private Transform doorsParent;
    [SerializeField] private Transform spawnPointsParent;
    private List<Transform> spawnPointsEnnemies = new();
    private List<EntityRoot> currentEnemies = new();
    private List<Door> doors = new();
    public List<Door> Doors => doors;
    public Action<int> EnterDoor;
    private void Awake() {
        foreach (Transform child in spawnPointsParent) {
            spawnPointsEnnemies.Add(child);
        }

        foreach (Transform child in doorsParent) {
            doors.Add(child.GetComponent<Door>());
        }
    }

    public void Enter() {
        for (int i = 0; i < Doors.Count; i++) {
            int a = i;
            Doors[a].OnEnter += (collider) => {
                if (collider.GetComponentInParent<PlayerBrain>() == null) {
                    return;
                }

                EnterDoor?.Invoke(a);
            };
        }
    }


    public void SpawnEnemies(int epic, params EntityRoot[] ennemies) {
        if (ennemies == null) {
            return;
        }

        List<Transform> spawnPoints = spawnPointsEnnemies.ToList();
        for (int i = 0; i < ennemies.Length; i++) {
            EntityRoot root = Instantiate(ennemies[i]);
            root.transform.localScale = root.transform.localScale * epic;
            currentEnemies.Add(root);
            int randomSpawn = Random.Range(0, spawnPoints.Count);
            root.transform.position = spawnPoints[randomSpawn].position;
            spawnPoints.RemoveAt(randomSpawn);
        }
    }

    private void OnDestroy() {
        for (int i = 0; i < currentEnemies.Count; i++) {
            if (currentEnemies[i] != null) {
                Destroy(currentEnemies[i].gameObject);
            }
        }
    }
}

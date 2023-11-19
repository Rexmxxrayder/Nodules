using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour {
    public const int openRoomChance = 50;
    public const float ennemiesSizePercentRandom = 0.2f;

    [SerializeField] private Transform doorsParent;
    [SerializeField] private Transform spawnPointsParent;
    [SerializeField] private Vector2Int ennemiesNumberDependingOnDifficulty = new(1, 6);
    private List<Transform> spawnPointsEnnemies = new();
    private List<EntityRoot> enemiesToSpawn = new();
    private List<EntityRoot> currentEnemies = new();
    private List<Door> doors = new();
    private int entrance = 0;
    private int ennemiesAlive = 0;
    private DungeonManager.RoomData[] nextRooms;
    public List<Door> Doors => doors;
    public Action<int> EnterDoor;
    public List<EntityRoot> EnemiesToSpawn => enemiesToSpawn;
    private void Awake() {
        foreach (Transform child in spawnPointsParent) {
            spawnPointsEnnemies.Add(child);
        }

        foreach (Transform child in doorsParent) {
            doors.Add(child.GetComponent<Door>());
        }
    }

    public void Setup(int entrance, List<EntityRoot> enemiesToSpawn, DungeonManager.RoomData[] nextRooms) {
        this.entrance = entrance;
        this.enemiesToSpawn = enemiesToSpawn == null ? new() : enemiesToSpawn;
        this.nextRooms = nextRooms;
        SpawnEnemies();
        SetupDoor();
        OpenDoors();
    }

    public void SpawnEnemies() {
        for (int i = 0; i < enemiesToSpawn.Count; i++) {
            SpawnEnemy(enemiesToSpawn[i]);
        }
    }

    public List<EntityRoot> GetEnemies(List<EntityRoot> enemiesType, float difficulty) {
        float enemiesRandom = Mathf.Abs(ennemiesNumberDependingOnDifficulty.x - ennemiesNumberDependingOnDifficulty.y) * ennemiesSizePercentRandom;
        int enemiesNumber = Mathf.Clamp((int)(Mathf.Lerp(ennemiesNumberDependingOnDifficulty.x, ennemiesNumberDependingOnDifficulty.y, difficulty) + Random.Range(-enemiesRandom, enemiesRandom)), ennemiesNumberDependingOnDifficulty.x,ennemiesNumberDependingOnDifficulty.y);
        int firstNumber = Random.Range(0, enemiesNumber + 1);
        EntityRoot firstEnemy = enemiesType[Random.Range(0, enemiesType.Count)];
        EntityRoot secondEnemy;
        int security = 0;
        do {
            secondEnemy = enemiesType[Random.Range(0, enemiesType.Count)];
            security++;
        } while (firstEnemy == secondEnemy && security < 10);

        List<EntityRoot> list = new List<EntityRoot>();

        for (int i = 0; i < enemiesNumber; i++) {
            list.Add(i < firstNumber ? firstEnemy : secondEnemy);
        }

        return list;
    }

    private void SpawnEnemy(EntityRoot toSpawn) {
        EntityRoot root = Instantiate(toSpawn);
        currentEnemies.Add(root);
        ennemiesAlive++;
        root.OnDeath += () => {
            --ennemiesAlive;
            OpenDoors();
        };

        int randomSpawn = Random.Range(0, spawnPointsEnnemies.Count);
        root.transform.position = spawnPointsEnnemies[randomSpawn].position;
        spawnPointsEnnemies.RemoveAt(randomSpawn);
    }

    private void OpenDoors() {
        if (ennemiesAlive != 0 || nextRooms == null) {
            return;
        }

        bool atleastOne = false;
        int RandomStart = Random.Range(0, 200);
        for (int i = 0 + RandomStart; i < 4 + RandomStart; i++) {
            if (i % 4 != entrance && Random.Range(0, 100) < openRoomChance) {
                Doors[i % 4].Open();
                atleastOne = true;
            }
        }

        while (!atleastOne) {
            int openDoor = Random.Range(0, 4);
            if (openDoor != entrance) {
                Doors[openDoor].Open();
                atleastOne = true;
            }
        }
    }

    private void SetupDoor() {
        for (int i = 0; i < Doors.Count; i++) {
            Doors[i].Close();
            int a = i;
            Doors[a].OnEnter += (collider) => {
                if (collider.GetComponentInParent<PlayerBrain>() == null || !collider.CompareTag("Player")) {
                    return;
                }

                EnterDoor?.Invoke(a);
            };

            if (nextRooms != null) {
                Doors[i].SetNextRoom(nextRooms[(i + 2) % 4]);
            }
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

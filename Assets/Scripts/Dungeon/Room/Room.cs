using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour {
    public const int openRoomChance = 50;

    [SerializeField] private Transform doorsParent;
    [SerializeField] private Transform spawnPointsParent;
    private List<Transform> spawnPointsEnnemies = new();
    private List<EntityRoot> ennemies = new();
    private List<EntityRoot> currentEnemies = new();
    private List<Door> doors = new();
    public int entrance = 0;
    private int ennemiesAlive = 0;
    private DungeonManager.RoomData[] nextRooms;
    public List<Door> Doors => doors;
    public Action<int> EnterDoor;
    public List<EntityRoot> Ennemies => ennemies;
    private void Awake() {
        foreach (Transform child in spawnPointsParent) {
            spawnPointsEnnemies.Add(child);
        }

        foreach (Transform child in doorsParent) {
            doors.Add(child.GetComponent<Door>());
        }
    }

    public void Setup(int entrance, EntityRoot[] ennemies, DungeonManager.RoomData[] nextRooms) {
        this.entrance = entrance;
        this.ennemies = ennemies == null ? new() : ennemies.ToList();
        this.nextRooms = nextRooms;
        SpawnEnemies();
        SetupDoor();
        OpenDoors();
    }

    public void SpawnEnemies() {
        if (ennemies == null) {
            return;
        }

        List<Transform> spawnPoints = spawnPointsEnnemies.ToList();
        for (int i = 0; i < ennemies.Count; i++) {
            EntityRoot root = Instantiate(ennemies[i]);
            currentEnemies.Add(root);
            ennemiesAlive++;
            root.OnDeath += () => {
                --ennemiesAlive;
                OpenDoors();
            };

            int randomSpawn = Random.Range(0, spawnPoints.Count);
            root.transform.position = spawnPoints[randomSpawn].position;
            spawnPoints.RemoveAt(randomSpawn);
        }
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
                if (collider.GetComponentInParent<PlayerBrain>() == null) {
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

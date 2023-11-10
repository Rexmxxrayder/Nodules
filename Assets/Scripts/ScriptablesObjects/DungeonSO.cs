using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Dungeon", menuName = "ScriptableObjects/Dungeon")]
public class DungeonSO : ScriptableObject {
    [SerializeField] private int size;
    [SerializeField] private List<int> roomsBetweensElite = new();
    [SerializeField] private List<Room> rooms = new();
    [SerializeField] private List<EntityRoot> ennemiesToSpawn = new();
    [SerializeField] private List<EntityRoot> epicEnnemiesToSpawn = new();
    [SerializeField] private Room apparitionRoom;
    [SerializeField] private Room bossRoom;
    [SerializeField] private EntityRoot boss;

    public int Size => size;
    public List<int> RoomsBetweensElite => roomsBetweensElite;
    public List<Room> Rooms => rooms;
    public List<EntityRoot> EnnemiesToSpawn => ennemiesToSpawn;
    public List<EntityRoot> EpicEnnemiesToSpawn => epicEnnemiesToSpawn;
    public Room ApparitionRoom => apparitionRoom;
    public Room BossRoom => bossRoom;
    public EntityRoot Boss => boss;
    private void OnValidate() {
        size = GetSize();
    }

    public int GetSize() {
        int size = 1;
        if (RoomsBetweensElite.Count == 0) {
            return 2;
        }

        foreach (int rooms in RoomsBetweensElite) {
            size += rooms + 1;
        }

        return size;
    }

    public Room GetRandomRoom() {
        return Rooms[Random.Range(0, Rooms.Count)];
    }

    public EntityRoot[] GetEnemies(bool isEpic) {
        int enemiesNumber = Random.Range(2, 5);
        int firstNumber = Random.Range(0, enemiesNumber + 1);
        EntityRoot firstEnemy = isEpic ? EpicEnnemiesToSpawn[Random.Range(0, EpicEnnemiesToSpawn.Count)] : EnnemiesToSpawn[Random.Range(0, EnnemiesToSpawn.Count)];
        EntityRoot secondEnemy = isEpic ? EpicEnnemiesToSpawn[Random.Range(0, EpicEnnemiesToSpawn.Count)] : EnnemiesToSpawn[Random.Range(0, EnnemiesToSpawn.Count)];
        int security = 0;
        while (firstEnemy == secondEnemy && security < 10) {
            security++;
            secondEnemy = isEpic ? EpicEnnemiesToSpawn[Random.Range(0, EpicEnnemiesToSpawn.Count)] : EnnemiesToSpawn[Random.Range(0, EnnemiesToSpawn.Count)];
        }

        EntityRoot[] entityRoots = new EntityRoot[enemiesNumber];
        for (int i = 0; i < entityRoots.Length; i++) {
            entityRoots[i] = i <= firstNumber ? firstEnemy : secondEnemy;
        }

        return entityRoots;
    }

    public EntityRoot GetBoss() {
        return Boss;
    }
}

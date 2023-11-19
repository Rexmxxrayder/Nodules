using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Dungeon", menuName = "ScriptableObjects/Dungeon")]
public class DungeonSO : ScriptableObject {
    [SerializeField] private int size;
    [SerializeField] private List<int> floorsRoomsNumber = new();
    [SerializeField] private List<AnimationCurve> floorsDifficulty = new();
    [SerializeField] private List<Room> rooms = new();
    [SerializeField] private List<EntityRoot> ennemiesToSpawn = new();
    [SerializeField] private List<EntityRoot> epicEnnemiesToSpawn = new();
    [SerializeField] private Room apparitionRoom;
    [SerializeField] private Room bossRoom;
    [SerializeField] private EntityRoot boss;

    public int Size => size;
    public List<int> FloorsRoomsNumber => floorsRoomsNumber;
    public List<AnimationCurve> FloorsDifficulty => floorsDifficulty;
    public List<Room> Rooms => rooms;
    public List<EntityRoot> EnnemiesToSpawn => ennemiesToSpawn;
    public List<EntityRoot> EpicEnnemiesToSpawn => epicEnnemiesToSpawn;
    public Room ApparitionRoom => apparitionRoom;
    public Room BossRoom => bossRoom;
    public EntityRoot Boss => boss;
    private void OnValidate() {
        size = GetSize();
        while (floorsDifficulty.Count < FloorsRoomsNumber.Count) {
            floorsDifficulty.Add(new AnimationCurve());
        }
    }

    public int GetSize() {
        int size = 1;
        if (FloorsRoomsNumber.Count == 0) {
            return 2;
        }

        foreach (int rooms in FloorsRoomsNumber) {
            size += rooms + 1;
        }

        return size;
    }

    public Room GetRandomRoom() {
        return Rooms[Random.Range(0, Rooms.Count)];
    }

    public List<EntityRoot> GetEnemies(bool isEpic) {
        return isEpic ? EpicEnnemiesToSpawn : EnnemiesToSpawn;
    }

    public EntityRoot GetBoss() {
        return Boss;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Dungeon", menuName = "ScriptableObjects/Dungeon")]
public class DungeonSO : ScriptableObject
{
    [SerializeField] private int size;
    public List<int> RoomsBetweensElite = new();
    public Room ApparitionRoom = new();
    public Room BossRoom = new();
    public List<Room> Rooms = new();
    public List<EntityRoot> EnnemiesToSpawn = new();
    public EntityRoot Boss;
    private void OnValidate() {
        size = Size();
    }

    public int Size() {
        int size = 1;
        if(RoomsBetweensElite.Count == 0) {
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
    
    public EntityRoot[] GetEnemies() {
        int enemiesNumber = Random.Range(2, 5);
        int firstNumber = Random.Range(0, enemiesNumber + 1);
        EntityRoot firstEnemy = EnnemiesToSpawn[Random.Range(0, EnnemiesToSpawn.Count)];
        EntityRoot secondEnemy = EnnemiesToSpawn[Random.Range(0, EnnemiesToSpawn.Count)];
        int security = 0;
        while(firstEnemy == secondEnemy && security < 10) {
            security++;
            secondEnemy = EnnemiesToSpawn[Random.Range(0, EnnemiesToSpawn.Count)];
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

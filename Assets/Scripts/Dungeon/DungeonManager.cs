using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

    struct RoomData {
        public Room Room;
        public EntityRoot[] ennemies;
        public bool isEpic;
        public RoomData(Room rooms, params EntityRoot[] ennemies) {
            Room = rooms;
            this.ennemies = ennemies;
            isEpic = false;
        }

        public RoomData(bool isEpic, Room rooms, params EntityRoot[] ennemies) {
            Room = rooms;
            this.ennemies = ennemies;
            this.isEpic = isEpic;
        }
    }
    [SerializeField] private DungeonSO dungeon;

    private RoomData[,] floor;
    private int currentRoomId = -1;
    private Room currentRoom;
    private void Start() {
        floor = new RoomData[dungeon.Size(), 4];
        for (int k = 0; k < 4; k++) {
            floor[0, k] = new RoomData(dungeon.ApparitionRoom, null);
        }

        int roomsNumber = 1;
        for (int i = 0; i < dungeon.RoomsBetweensElite.Count; i++) {
            for (int j = 0; j < dungeon.RoomsBetweensElite[i]; j++) {
                for (int k = 0; k < 4; k++) {
                    floor[roomsNumber + j, k] = new RoomData(false, dungeon.GetRandomRoom(), dungeon.GetEnemies());
                }
            }


            roomsNumber += dungeon.RoomsBetweensElite[i];
            if (roomsNumber == dungeon.Size() - 1) {
                continue;
            }
            for (int k = 0; k < 4; k++) {
                floor[roomsNumber, k] = new RoomData(true, dungeon.GetRandomRoom(), dungeon.GetEnemies());
            }

            roomsNumber++;
        }

        for (int k = 0; k < 4; k++) {
            floor[roomsNumber, k] = new RoomData(true, dungeon.BossRoom, dungeon.GetBoss());
        }

        NextRoom(3);
    }

    public void NextRoom(int door) {
        currentRoomId++;
        if (currentRoom != null) {
            currentRoom.gameObject.SetActive(false);
            Destroy(currentRoom.gameObject);
        }

        int inverseDoor = (door + 2) % 4;
        currentRoom = Instantiate(floor[currentRoomId, inverseDoor].Room, transform);
        List<int> doorToOpen = new();
        for (int i = 0; i < 3; i++) {
            doorToOpen.Add(Random.Range(0, 4));
        }

        for (int i = 0; i < 4; i++) {
            if (i == inverseDoor) {
                currentRoom.Doors[i].Close = true;
                if (doorToOpen.Contains(i)) {
                    currentRoom.Doors[door].Close = false;
                }
            } else {
                if (doorToOpen.Contains(i)) {
                    currentRoom.Doors[i].Close = false;
                }
            }

        }

        currentRoom.EnterDoor += (door) => {
            NextRoom(door);
        };

        currentRoom.Enter();

        for (int k = 0; k < 4; k++) {
            string inside = "";
            if (currentRoom.Doors[k].Close || currentRoomId == floor.GetLength(0) - 1) {
                inside = "CLOSE";
                currentRoom.Doors[k].Close = true;
            } else {
                for (int l = 0; l < floor[currentRoomId + 1, (k + 2) % 4].ennemies.Length; l++) {
                    inside += $"{floor[currentRoomId + 1, (k + 2) % 4].ennemies[l].Type}\n";
                }
            }

            currentRoom.Doors[k].Text = inside;
        }

        int epic = currentRoomId == floor.GetLength(0) - 1 ? 4 : floor[currentRoomId, inverseDoor].isEpic ? 2 : 1;
        PlayerBrain.Transform.position = currentRoom.Doors[inverseDoor].transform.position;
        currentRoom.SpawnEnemies(epic, floor[currentRoomId, inverseDoor].ennemies);
    }
}

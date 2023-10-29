using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

    public struct RoomData {
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

    private RoomData[][] floor;
    private int currentRoomId = -1;
    private Room currentRoom;
    private void Start() {
        floor = new RoomData[dungeon.Size()][];
        for (int i = 0; i < floor.Length; i++) {
            floor[i] = new RoomData[4];
        }

        for (int k = 0; k < 4; k++) {
            floor[0][k] = new RoomData(dungeon.ApparitionRoom, null);
        }

        int roomsNumber = 1;
        for (int i = 0; i < dungeon.RoomsBetweensElite.Count; i++) {
            for (int j = 0; j < dungeon.RoomsBetweensElite[i]; j++) {
                for (int k = 0; k < 4; k++) {
                    floor[roomsNumber + j][k] = new RoomData(false, dungeon.GetRandomRoom(), dungeon.GetEnemies());
                }
            }


            roomsNumber += dungeon.RoomsBetweensElite[i];
            if (roomsNumber == dungeon.Size() - 1) {
                continue;
            }
            for (int k = 0; k < 4; k++) {
                floor[roomsNumber][k] = new RoomData(true, dungeon.GetRandomRoom(), dungeon.GetEnemies());
            }

            roomsNumber++;
        }

        for (int k = 0; k < 4; k++) {
            floor[roomsNumber][k] = new RoomData(true, dungeon.BossRoom, dungeon.GetBoss());
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
        currentRoom = Instantiate(floor[currentRoomId][inverseDoor].Room, transform);
        currentRoom.EnterDoor += (door) => {
            NextRoom(door);
        };

        int roomPower = currentRoomId == floor.GetLength(0) - 1 ? 4 : floor[currentRoomId][inverseDoor].isEpic ? 2 : 1;
        PlayerBrain.Transform.position = currentRoom.Doors[inverseDoor].transform.position;
        currentRoom.Setup(inverseDoor, roomPower, floor[currentRoomId][inverseDoor].ennemies, floor.Length <= currentRoomId + 1 ? null : floor[currentRoomId +1]);
    }
}

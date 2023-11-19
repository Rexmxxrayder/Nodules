using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    [SerializeField] private DungeonSO dungeonSO;

    private RoomData[][] dungeon;
    private int currentRoomId = -1;
    private Room currentRoom;
    private void Start() {
        dungeon = new RoomData[dungeonSO.GetSize()][];
        for (int i = 0; i < dungeon.Length; i++) {
            dungeon[i] = new RoomData[4];
        }

        for (int k = 0; k < 4; k++) {
            dungeon[0][k] = new RoomData(dungeonSO.ApparitionRoom, null);
        }

        int roomsNumber = 1;
        for (int i = 0; i < dungeonSO.FloorsRoomsNumber.Count; i++) {
            for (int j = 0; j < dungeonSO.FloorsRoomsNumber[i]; j++) {
                for (int k = 0; k < 4; k++) {
                    Room room = dungeonSO.GetRandomRoom();
                    float difficulty = dungeonSO.FloorsDifficulty[i].Evaluate(Mathf.InverseLerp(i == 0 ? 0 : dungeonSO.FloorsRoomsNumber[i - 1], dungeonSO.FloorsRoomsNumber[i], currentRoomId));
                    dungeon[roomsNumber + j][k] = new RoomData(false, room, room.GetEnemies(dungeonSO.GetEnemies(false), difficulty).ToArray());
                }
            }

            roomsNumber += dungeonSO.FloorsRoomsNumber[i];
            if (roomsNumber == dungeonSO.GetSize() - 1) {
                continue;
            }

            for (int k = 0; k < 4; k++) {
                Room room = dungeonSO.GetRandomRoom();
                float difficulty = dungeonSO.FloorsDifficulty[i].Evaluate(Mathf.InverseLerp(i == 0 ? 0 : dungeonSO.FloorsRoomsNumber[i - 1], dungeonSO.FloorsRoomsNumber[i], currentRoomId));
                dungeon[roomsNumber][k] = new RoomData(false, room, room.GetEnemies(dungeonSO.GetEnemies(true), difficulty).ToArray());
            }

            roomsNumber++;
        }

        for (int k = 0; k < 4; k++) {
            dungeon[roomsNumber][k] = new RoomData(true, dungeonSO.BossRoom, dungeonSO.GetBoss());
        }

        NextRoom(3);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            NextRoom(3);
        }
    }

    public void NextRoom(int door) {
        currentRoomId++;
        if (currentRoom != null) {
            currentRoom.gameObject.SetActive(false);
            Destroy(currentRoom.gameObject);
        }

        int inverseDoor = (door + 2) % 4;
        currentRoom = Instantiate(dungeon[currentRoomId][inverseDoor].Room, transform);
        currentRoom.EnterDoor += (door) => {
            NextRoom(door);
        };

        PlayerBrain.Transform.position = currentRoom.Doors[inverseDoor].transform.position;
        currentRoom.Setup(inverseDoor, dungeon[currentRoomId][inverseDoor].ennemies == null ? null : dungeon[currentRoomId][inverseDoor].ennemies.ToList(), dungeon.Length <= currentRoomId + 1 ? null : dungeon[currentRoomId + 1]);
    }
}

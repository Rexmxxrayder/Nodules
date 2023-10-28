using UnityEngine;

public class RoomManager : MonoBehaviour {
    public static RoomManager Gino;
    [SerializeField] private Room firstRoom;
    [SerializeField] private Room[] rooms;
    [SerializeField] private Room currentRoom;

    private void Awake() {
        Gino = this;
    }


}
using UnityEngine;

public class RoomManager : MonoBehaviour {
    public static RoomManager Gino;
    [SerializeField] private Room firstRoom;
    [SerializeField] private Room[] rooms;
    [SerializeField] private Room currentRoom;


    private void Awake() {
        Gino = this;
    }
    private void Start() {
        currentRoom = Instantiate(firstRoom, transform);
        HMove.instance.transform.position = currentRoom.Spawn.transform.position;
    }

    public void NextRoom(Vector3 vector) {
        currentRoom.gameObject.SetActive(false);
        Destroy(currentRoom);
        currentRoom = Instantiate(rooms[Random.Range(0, rooms.Length - 1)], transform);
        if (vector == Vector3.forward) {
            currentRoom.transform.rotation = Quaternion.Euler(0, 90, 0);
        } else if (vector == Vector3.back) {
            currentRoom.transform.rotation = Quaternion.Euler(0, 270, 0);
        } else if (vector == Vector3.right) {
            currentRoom.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        HMove.instance.transform.position = currentRoom.Spawn.transform.position;
    }
}
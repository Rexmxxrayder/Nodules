using System;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField] private EntityBar bar;
    private bool isClose = true;
    private DungeonManager.RoomData nextRoom;
    public bool IsClose { get => isClose; set => isClose = value; }
    private Action<Collider> onEnter;
    public event Action<Collider> OnEnter { add { onEnter += value; } remove { onEnter += value; } }

    public void Close() {
        isClose = true;
        bar.Text.text = "LOCKED";
    }

    public void Open() {
        isClose = false;
        string nextEnemies = "";
        for (int l = 0; l < nextRoom.ennemies.Length; l++) {
            nextEnemies += $"{nextRoom.ennemies[l].Type}\n";
        }

        bar.Text.text = nextEnemies;
    }

    private void OnTriggerEnter(Collider other) {
        if (isClose) return;
        onEnter?.Invoke(other);
    }

    public void SetNextRoom(DungeonManager.RoomData nextRoom) {
        this.nextRoom = nextRoom;
    }
}

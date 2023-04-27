using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : MonoBehaviour {
    public List<string> ennemiToSpawn = new List<string>();
    public List<int> numberToSpawn = new List<int>();
    public List<Transform> SpawnPoint = new List<Transform>();
    private void Start() {
        for (int i = 0; i < ennemiToSpawn.Count; i++) {
            for (int j = 0; j < numberToSpawn[i]; j++) {
                EntityRoot ennemi = BrainPools.Gino.GetInstance(ennemiToSpawn[i]);
                int RandomPoint = Random.Range(0, SpawnPoint.Count - 1);
                ennemi.transform.position = SpawnPoint[RandomPoint].position;
                SpawnPoint.RemoveAt(RandomPoint);
            }
        }
    }

    private void OnValidate() {
        if (numberToSpawn.Count < ennemiToSpawn.Count) {
            while (numberToSpawn.Count < ennemiToSpawn.Count) {
                numberToSpawn.Add(0);
            }
        } else {
            while (numberToSpawn.Count > ennemiToSpawn.Count) {
                numberToSpawn.RemoveAt(numberToSpawn.Count - 1);
            }
        }
    }
}

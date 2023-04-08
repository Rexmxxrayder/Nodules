using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class Spawner : MonoBehaviour {
    public string ennemiType;
    public float spawnTimer;
    public float distSpawn;
    public int NumberSpawn;

    private void Start() {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning() {
        while (true) {
            yield return new WaitForSeconds(spawnTimer);
            Spawn();
        }
    }

    public void Spawn() {
        for (int i = 0; i < NumberSpawn; i++) {
            Vector3 direction = RotationSloot.GetDirectionOnAxis(360 / NumberSpawn * i, RotationSloot.TranslateVector3("z"));
            Vector3 ennemiPosition = transform.position + direction * distSpawn;
            Ennemi ennemi = (Ennemi)BasicPrefabs.Gino.GetInstance(ennemiType);
            ennemi.transform.position = ennemiPosition;
            ennemi.Activate();
        }

    }
}

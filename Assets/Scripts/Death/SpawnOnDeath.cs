using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDeath : MonoBehaviour {
    [SerializeField] private EntityRoot toSpawn;

    private void Start() {
        gameObject.GetRoot().OnDeath += () => {
            Instantiate(toSpawn, transform.position, Quaternion.identity);
        };
    }
}

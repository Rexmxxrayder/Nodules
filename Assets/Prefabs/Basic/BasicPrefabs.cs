using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPrefabs : MonoBehaviour {
    public static BasicPrefabs Gino;
    [SerializeField] Bullet bullet;

    public Bullet Bullet { get { return bullet; } }

    private void Awake() {
        if (Gino == null) {
            Gino = this;
        } else {
            Destroy(gameObject);
        }
    }
}

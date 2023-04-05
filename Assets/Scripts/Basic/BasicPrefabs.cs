using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPrefabs : MonoBehaviour {
    public static BasicPrefabs Gino;
    [SerializeField] List<Basic> prefabs = new();
    Dictionary<string, Pool<Basic>> pools = new();


    private void Awake() {
        if (Gino == null) {
            Gino = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        for (int i = 0; i < prefabs.Count; i++) {
            GameObject storage = new GameObject(prefabs[i].GetComponent<Basic>().GetType().Name + "Storage");
            storage.transform.parent = transform;
            pools.Add(prefabs[i].GetComponent<Basic>().Type, new Pool<Basic>(prefabs[i], storage));
        }
    }

    public Basic GetInstance(string instanceName) {
        Basic instance = pools[instanceName].GetInstance();
        instance.gameObject.Get<EntityHealth>().NewDeathWay += () => LetInstance(instance);
        return instance;
    }

    public void LetInstance(Basic instance) {
        pools[instance.Type].LetInstance(instance);
    }
}

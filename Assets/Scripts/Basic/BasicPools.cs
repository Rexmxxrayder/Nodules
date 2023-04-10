using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPools : MonoBehaviour {
    public static BasicPools Gino;
    [SerializeField] List<EntityBasic> prefabs = new();
    Dictionary<string, Pool<EntityBasic>> pools = new();


    private void Awake() {
        if (Gino == null) {
            Gino = this;
        } else {
            Destroy(gameObject);
        }
        for (int i = 0; i < prefabs.Count; i++) {
            GameObject storage = new GameObject(prefabs[i].gameObject.name + " Storage");
            storage.transform.parent = transform;
            pools.Add(prefabs[i].GetComponent<EntityBasic>().Type, new Pool<EntityBasic>(prefabs[i], storage));
        }
    }

    public EntityBasic GetInstance(string instanceName) {
        EntityBasic instance = pools[instanceName].GetInstance();
        instance.gameObject.Get<EntityHealth>().NewDeathWay += () => LetInstance(instance);
        return instance;
    }

    public void LetInstance(EntityBasic instance) {
        pools[instance.Type].LetInstance(instance);
    }
}

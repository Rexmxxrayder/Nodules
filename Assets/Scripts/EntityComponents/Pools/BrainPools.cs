using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainPools : MonoBehaviour {
    public static BrainPools Gino;
    [SerializeField] List<EntityBrain> prefabs = new();
    Dictionary<string, Pool<EntityBrain>> pools = new();


    private void Awake() {
        if (Gino == null) {
            Gino = this;
        } else {
            Destroy(gameObject);
        }
        for (int i = 0; i < prefabs.Count; i++) {
            GameObject storage = new GameObject(prefabs[i].gameObject.name + " Storage");
            storage.transform.parent = transform;
            pools.Add(prefabs[i].GetRoot().Type, new Pool<EntityBrain>(prefabs[i], storage));
        }
    }

    public EntityBrain GetInstance(string instanceName) {
        EntityBrain instance = pools[instanceName].GetInstance();
        instance.GetRoot().GiveType(pools[instanceName].Original.GetRoot().Type);
        instance.gameObject.GetRoot().NewDeathWay += () => LetInstance(instance);
        return instance;
    }

    public void LetInstance(EntityBrain instance) {
        pools[instance.GetRoot().Type].LetInstance(instance);
    }
}

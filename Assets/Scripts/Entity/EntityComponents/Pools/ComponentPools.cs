using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPool : MonoBehaviour {
    public static ComponentPool Gino;
    [SerializeField] List<EntityComponent> prefabs = new();
    Dictionary<string, Pool<EntityComponent>> pools = new();


    private void Awake() {
        if (Gino == null) {
            Gino = this;
        } else {
            Destroy(gameObject);
        }
        for (int i = 0; i < prefabs.Count; i++) {
            GameObject storage = new GameObject(prefabs[i].gameObject.name + " Storage");
            storage.transform.parent = transform;
            pools.Add(prefabs[i].GetComponent<EntityComponent>().Type, new Pool<EntityComponent>(prefabs[i], storage));
        }
    }

    public virtual EntityComponent GetInstance(string instanceName) {
        EntityComponent instance = pools[instanceName].GetInstance();
        return instance;
    }

    public virtual void LetInstance(EntityComponent instance) {
        pools[instance.Type].LetInstance(instance);
    }
}

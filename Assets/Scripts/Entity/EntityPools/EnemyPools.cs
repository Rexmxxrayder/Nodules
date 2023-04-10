using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPools : MonoBehaviour {
    public static EnemyPools Gino;
    [SerializeField] List<EntityEnemy> prefabs = new();
    Dictionary<string, Pool<EntityEnemy>> pools = new();


    private void Awake() {
        if (Gino == null) {
            Gino = this;
        } else {
            Destroy(gameObject);
        }
        for (int i = 0; i < prefabs.Count; i++) {
            GameObject storage = new GameObject(prefabs[i].gameObject.name + " Storage");
            storage.transform.parent = transform;
            pools.Add(prefabs[i].GetComponent<EntityEnemy>().Type, new Pool<EntityEnemy>(prefabs[i], storage));
        }
    }

    public EntityEnemy GetInstance(string instanceName) {
        EntityEnemy instance = pools[instanceName].GetInstance();
        instance.gameObject.Get<EntityHealth>().NewDeathWay += () => LetInstance(instance);
        return instance;
    }

    public void LetInstance(EntityEnemy instance) {
        pools[instance.Type].LetInstance(instance);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EntityRoot : EntityComponent {
    public override GameObject SetRoot() {
        _root = gameObject;
        return GetRoot();
    }

    public override GameObject GetRoot() {
        return _root;
    }

    protected override void ChildSetup() {
        if (Get<EntityHealth>() == null) {
            GameObject health = new GameObject("Health");
            health.AddComponent<EntityHealth>();
            health.transform.parent = transform;
        }
    }
}

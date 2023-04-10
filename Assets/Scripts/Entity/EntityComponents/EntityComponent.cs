using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EntityComponent : MonoBehaviour, IEntity {
    protected GameObject _root = null;

    private void Awake() {
        SetRoot();
        AwakeSetup();
    }

    private void Start() {
        StartSetup();
    }

    protected virtual void AwakeSetup() {

    }

    protected virtual void StartSetup() {

    }

    public virtual GameObject SetRoot() {
        if (_root == null) {
            if (transform.parent == null) {
                GameObject newParent = new GameObject();
                newParent.name = "Root";
                transform.parent = newParent.transform;
                Debug.Log(transform.gameObject.name);
            }
            EntityComponent parentComponent = transform.parent.GetComponent<EntityComponent>();
            _root = parentComponent == null ? transform.parent.AddComponent<EntityRoot>().GetRoot() : parentComponent.GetRoot();
        }
        return _root;
    }

    public virtual GameObject GetRoot() {
        if (_root == null) {
            return SetRoot();
        } else {
            return _root;
        }
    }

    public T Get<T>() where T : MonoBehaviour, IEntity {
        return _root.GetComponentInChildren<T>();
    }
}
public static class ExtensionForEntityComponent {
    public static T Get<T>(this GameObject gO) where T : MonoBehaviour, IEntity {
        EntityComponent ec = gO.GetComponent<EntityComponent>();
        if (ec != null) {
            return ec.Get<T>();
        } else { return null; }
    }
}

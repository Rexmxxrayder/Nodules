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

    public Vector3 GetRootPosition() {
        return GetRoot().transform.position;
    }

    public Quaternion GetRootRotation() {
        return GetRoot().transform.rotation;
    }

    public Vector3 GetRootScale() {
        return GetRoot().transform.localScale;
    }

    public T Get<T>() where T : MonoBehaviour, IEntity {
        return _root.GetComponentInChildren<T>();
    }
}
public static class ExtensionForEntityComponent {
    public static EntityRoot GetRoot(this GameObject gO) {
        return gO.GetComponentInParent<EntityRoot>();
    }

    public static T Get<T>(this GameObject gO) where T : MonoBehaviour, IEntity {
        if (gO.GetRoot() == null) {
            return null;
        }
        if (gO.GetComponent<EntityComponent>() != null) {
            return gO.GetComponent<EntityComponent>().Get<T>();
        }
        return gO.GetRoot().Get<T>();
    }

    public static Vector3 GetRootPosition(this GameObject gO) {
        return gO.GetRoot().GetRootPosition();
    }

    public static Quaternion GetRootRotation(this GameObject gO) {
        return gO.GetRoot().GetRootRotation();
    }

    public static Vector3 GetRootScale(this GameObject gO) {
        return gO.GetRoot().GetRootScale();
    }
}

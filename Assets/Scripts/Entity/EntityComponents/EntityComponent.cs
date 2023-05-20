using Sloot;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EntityComponent : MonoBehaviour, IEntity, IReset {
    protected GameObject _root = null;

    private void Awake() {
        SetRoot();
        AwakeSetup();
        InstanceReset();
    }

    private void Start() {
        StartSetup();
        InstanceResetSetup();
    }

    public virtual GameObject SetRoot() {
        if (_root == null) {
            if (transform.parent == null) {
                GameObject newParent = new GameObject();
                newParent.name = "Root";
                transform.parent = newParent.transform;
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

    protected virtual void AwakeSetup() {}

    protected virtual void StartSetup() {}

    public virtual void InstanceReset() {}

    public virtual void InstanceResetSetup() {}
}
public static class ExtensionEntityComponent {
    public static EntityRoot GetRoot(this GameObject gO) {
        return gO.GetComponentInParent<EntityRoot>();
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

    public static T Get<T>(this GameObject gO) where T : MonoBehaviour, IEntity {
        if (gO.GetRoot() == null) {
            return null;
        }
        return gO.GetRoot().Get<T>();
    }
}

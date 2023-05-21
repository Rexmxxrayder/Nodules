using Sloot;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EntityComponent : MonoBehaviour, IEntity, IReset {
    protected GameObject _root = null;

    private void Awake() {
        SetRoot();
        InstanceReset();
    }

    private void Start() {
        InstanceResetSetup();
    }

    protected virtual void AwakeSetup() { }

    protected virtual void StartSetup() { }

    public void InstanceReset() {
        AwakeSetup();
    }

    public void InstanceResetSetup() {
        StartSetup();
    }
    public virtual GameObject SetRoot() {
        if (_root == null) {
            if (gameObject.GetComponent<EntityRoot>() != null) {
                _root = gameObject;
                return _root;
            } else if (transform.parent == null) {
                GameObject newParent = new() {
                    name = "Root"
                };
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

}
public static class ExtensionEntityComponent {
    public static GameObject GetRoot(this GameObject gO) {
        if (gO.GetComponentInParent<EntityComponent>(true) == null) {
            return null;
        }
        return gO.GetComponentInParent<EntityComponent>(true).GetRoot();
    }

    public static T Get<T>(this GameObject gO) where T : MonoBehaviour, IEntity {
        if (gO.GetRoot() == null) {
            return null;
        }
        return gO.GetRoot().GetComponentInChildren<T>();
    }

    public static Vector3 GetRootPosition(this GameObject gO) {
        return gO.GetRoot().transform.position;
    }

    public static Quaternion GetRootRotation(this GameObject gO) {
        return gO.GetRoot().transform.rotation;
    }

    public static Vector3 GetRootScale(this GameObject gO) {
        return gO.GetRoot().transform.localScale;
    }

}

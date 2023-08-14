using Sloot;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class EntityBrain : EntityComponent {
    [SerializeField] protected Vector3 visor;
    [SerializeField] protected Transform selected;

    #region Properties
    public Vector3 Visor { get { return visor; } }
    public Transform Selected { get { return selected; } }
    #endregion

    public override EntityRoot SetRoot() {
        if (_root == null) {
            if (TryGetComponent(out EntityRoot root)) {
                _root = root;
            } else {
                _root = gameObject.AddComponent<EntityRoot>().GetRoot();
            }
        }

        return _root;
    }

    protected override void LoadSetup() {
        foreach (EntityBodyPart ebp in GetComponentsInChildren<EntityBodyPart>()) {
            ebp.AssignBrain(this);
        }
    }
}

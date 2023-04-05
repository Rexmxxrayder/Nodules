using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EntityMultipleCollider2D : EntityCollider2D {
    [SerializeField] List<EntityColliderDelegate2D> hitboxs = new List<EntityColliderDelegate2D>();

    protected override void ChildSetup() {
        foreach (EntityColliderDelegate2D colliderDelegate in GetComponentsInChildren<EntityColliderDelegate2D>()) {
            hitboxs.Add(colliderDelegate);
            AssignTo(colliderDelegate);
        }
    }

    public override void SetTrigger(bool newState) {
        for (int i = 0; i < hitboxs.Count; i++) {
            hitboxs[i].GetCollider().isTrigger = newState;
        }
    }
}

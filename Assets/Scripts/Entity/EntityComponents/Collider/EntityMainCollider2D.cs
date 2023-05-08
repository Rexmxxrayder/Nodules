using System.Collections.Generic;
using UnityEngine;

public class EntityMainCollider2D : EntityCollider2D {
    [SerializeField] List<EntityColliderDelegate2D> colliders = new List<EntityColliderDelegate2D>();

    protected override void StartSetup() {
        foreach (EntityColliderDelegate2D colliderDelegate in GetComponentsInChildren<EntityColliderDelegate2D>()) {
            colliders.Add(colliderDelegate);
            AssignTo(colliderDelegate);
        }
    }

    public override void SetTrigger(bool newState) {
        for (int i = 0; i < colliders.Count; i++) {
            colliders[i].GetCollider().isTrigger = newState;
        }
    }
}

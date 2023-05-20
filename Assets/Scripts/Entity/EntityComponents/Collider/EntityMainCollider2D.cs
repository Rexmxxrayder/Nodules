using System.Collections.Generic;
using UnityEngine;

public class EntityMainCollider2D : EntityCollider2D {
    [SerializeField] List<EntityCollider2D> colliders = new();

    protected override void StartSetup() {
        foreach (EntityCollider2D colliderDelegate in GetComponentsInChildren<EntityCollider2D>()) {
            colliders.Add(colliderDelegate);
            AssignTo(colliderDelegate);
        }
    }
}

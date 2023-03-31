using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovements : EntityComponent {
    [SerializeField] EntityPhysics entityPhysics;
    [SerializeField] EntityPosition entityPosition;
    [SerializeField] float speed;
    [SerializeField] Force currentForce;

    private void Awake() {
        if (entityPhysics == null || entityPosition == null) {
            Debug.LogError("Lack of Components");
        }
    }
    public void Goto(Vector2 destination) {
        if (currentForce != null && !currentForce.HasEnded) {
            entityPhysics.Remove(currentForce);
        }
        currentForce = Force.Const(destination - entityPosition, speed, (destination - entityPosition).magnitude / speed);
        entityPhysics.Add(currentForce, 0);
    }
}

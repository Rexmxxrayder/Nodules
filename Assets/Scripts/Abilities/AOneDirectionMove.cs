using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOneDirectionMove : Ability
{
    public Vector3 direction;
    [SerializeField] private static float Speed;
    [SerializeField] private static Force move;
    EntityPhysics ep;

    protected override void LaunchAbilityDown(EntityBrain brain) {
        if (move == null) {
            move = Force.Const(direction, Speed);
            ep.Add(move, EntityPhysics.PhysicPriority.INPUT, false);
        }
        move.Direction += direction;
        move.Direction = move.Direction.normalized * Speed;
    }

    protected override void LaunchAbilityUp(EntityBrain brain) {
        move.Direction -= direction;
        move.Direction = move.Direction.normalized;
    }
}

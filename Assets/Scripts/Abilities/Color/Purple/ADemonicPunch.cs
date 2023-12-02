using System;
using UnityEngine;

public class ADemonicPunch : Ability {

    [SerializeField] private DemonicMark demonicMark;
    [SerializeField] private int maxMark;
    [SerializeField] private float fistDuration = 1f;
    public static Action<Vector3, float> AttackToward;
    public static int MaxMark;
    protected override void LaunchAbilityUp(EntityBrain brain) {
        MaxMark = maxMark;
        brain.GetRootComponent<EntityPhysics>().Add(Force.Const(Vector3.zero, 1, fistDuration), EntityPhysics.PhysicPriority.BLOCK);
        Spawn(brain.GetRootPosition());
        AttackToward?.Invoke(brain.Visor, fistDuration);
    }

    private void Spawn(Vector3 position) {
        EntityRoot newInstance = Instantiate(demonicMark);
        newInstance.Spawn(position);
    }
}

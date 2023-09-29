using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicPunch : Ability
{
    [SerializeField] private DemonicMark demonicMark;
    [SerializeField] private int maxMark;
    protected override void LaunchAbilityUp(EntityBrain brain) {
        Spawn(brain.GetRootPosition());
        DemonicMark.AtkToward?.Invoke(brain.Visor);
        DemonicMark.MaxMark = maxMark;
    }


    private void Spawn(Vector3 position) {
        EntityRoot newInstance = Instantiate(demonicMark);
        newInstance.Spawn(position);
    }
}

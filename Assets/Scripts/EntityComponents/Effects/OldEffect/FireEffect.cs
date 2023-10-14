using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : EntityEffect {

    public override EffectType Type => EffectType.NONE;

    public override int MaxStack => 3;

    public override float OfficialDuration => 6;

    public override void AddStack(int stackNumber) {
        stackNumber = Mathf.Clamp(stackNumber, 0, MaxStack);
        stack += stackNumber;
        if (stack > MaxStack) {
            stack = MaxStack;
        }

        if(stack == MaxStack) {
            BurnEffect burn = new ();
            entityEffectManager.AddEffect(burn);
            EndEffect();
        }
    }
}

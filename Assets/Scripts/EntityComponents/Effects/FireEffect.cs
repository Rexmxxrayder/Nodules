using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : EntityEffect {

    public override EffectType Type => EffectType.FIRE;

    public override int MaxStack => 3;

    protected override void EffectTryingAdd(EntityEffect newEffect) {
        if (newEffect == this) {
            return;
        }

        if (newEffect.GetType() == typeof(FireEffect)) {
            AddStack(((FireEffect)newEffect).Stack);
            newEffect.Negate = true;
        }
    }

    public void AddStack(int stackNumber) {
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

    public void RemoveStack(int stackNumber) {
        stackNumber = Mathf.Clamp(stackNumber, 0, MaxStack);
        stack += stackNumber;
        if (stack <= 0) {
            stack = MaxStack;
        }
    }
}

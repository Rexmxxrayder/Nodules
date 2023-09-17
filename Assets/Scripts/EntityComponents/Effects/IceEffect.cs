using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffect : EntityEffect {
    public override EffectType Type => EffectType.ICE;

    public override int MaxStack => 5;

    public override void SetupEffect(EntityEffectManager effectManager) {
        base.SetupEffect(effectManager);
        duration = 10f;
    }

    protected override void EffectTryingAdd(EntityEffect newEffect) {
        if (newEffect == this) {
            return;
        }

        if (newEffect.GetType() == typeof(IceEffect)) {
            AddStack(((IceEffect)newEffect).Stack);
            newEffect.Negate = true;
        }
    }

    public void AddStack(int stackNumber) {
        stackNumber = Mathf.Clamp(stackNumber, 0, MaxStack);
        stack += stackNumber;
        if (stack > MaxStack) {
            stack = MaxStack;
        }

        if (stack == MaxStack) {
            IceEffect burn = new();
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

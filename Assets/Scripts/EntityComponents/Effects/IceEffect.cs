using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffect : EntityEffect {
    public override EffectType Type => EffectType.ICE;

    public override int MaxStack => 5;

    public override float StartDuration => 5f;

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

    public override void AddStack(int stackNumber) {
        base.AddStack(stackNumber);

        if (stack == MaxStack) {
            FreezeEffect freezeEffect = new();
            entityEffectManager.AddEffect(freezeEffect);
            EndEffect();
        }
    }
}

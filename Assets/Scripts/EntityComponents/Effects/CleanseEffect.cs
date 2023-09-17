using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanseEffect : EntityEffect {
    public override EffectType Type => EffectType.CLEANSE;

    public override int MaxStack => 1;

    public override void SetupEffect(EntityEffectManager newEffect) {
        base.SetupEffect(newEffect);
        foreach (EntityEffect effect in entityEffectManager.Effects) {
            entityEffectManager.RemoveEffect(effect);
        }
    }
}

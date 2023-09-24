using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CleanseEffect : EntityEffect {
    public override EffectType Type => EffectType.CLEANSE;

    public override int MaxStack => 1;

    public override float StartDuration => 1f;

    public override void SetupEffect(EntityEffectManager newEffect) {
        base.SetupEffect(newEffect);
        List<EntityEffect> effects = entityEffectManager.Effects.ToList();
        foreach (EntityEffect effect in effects) {
            entityEffectManager.RemoveEffect(effect);
        }
    }
}

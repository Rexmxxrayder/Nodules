using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsanityEffect : EntityEffect
{
    public override EffectType Type => EffectType.INSANITY;

    public override int MaxStack => 5;

    public override float OfficialDuration => 9999;

    protected override void EffectTryingAdd(EntityEffect newEffect) {

        switch (newEffect.Type) {
            case EffectType.INSANITY:
                AddStack(newEffect.Stack);
                newEffect.Negate = true;
                break;
            case EffectType.ICE:
            case EffectType.POWDER:
            case EffectType.FREEZE:
                MadnessEffect madness = new ();
                madness.AddStack(Stack - 1);
                EndEffect();
                entityEffectManager.AddEffect(madness);
                break;
            default:
                break;
        }
    }
}

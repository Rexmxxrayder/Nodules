using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadnessEffect : EntityEffect
{
    public override EffectType Type => EffectType.MADNESS;

    public override int MaxStack => 5;

    public override float OfficialDuration => 1f;

    public static AnimationCurve damagesCurve;

    public override void SetupEffect(EntityEffectManager effectManager) {
        base.SetupEffect(effectManager);
        entityEffectManager.RootGet<EntityHealth>().RemoveHealth((int)damagesCurve.Evaluate(stack));
        EndEffect();
    }
}

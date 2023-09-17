using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : EntityEffect
{
    public override EffectType Type => EffectType.ICE;

    public override int MaxStack => 1;

    private const float freezeDuration = 1f;

    protected override void EffectTryingAdd(EntityEffect newEffect) {
        if (newEffect == this) {
            return;
        }

        if (newEffect.GetType() == typeof(FreezeEffect)) {
            newEffect.Negate = true;
        }
    }

    public override void SetupEffect(EntityEffectManager effectManager) {
        base.SetupEffect(effectManager);
        EntityPhysics ep = effectManager.RootGet<EntityPhysics>();
        ep.Add(Force.Const(Vector3.zero, 1, freezeDuration), EntityPhysics.PhysicPriority.BLOCK);
    }
}

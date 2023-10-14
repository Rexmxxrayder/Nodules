using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : EntityEffect {
    public override EffectType Type => EffectType.FREEZE;

    public override int MaxStack => 1;

    public override float OfficialDuration => 2f;

    private EntityPhysics ep;
    private Force freezeForce;
    private EntityBrain eb;

    protected override void EffectTryingAdd(EntityEffect newEffect) {
        if (newEffect == this) {
            return;
        }

        if (newEffect.Type == EffectType.ICE || newEffect.Type == EffectType.FREEZE) {
            newEffect.Negate = true;
        }
    }

    public override void SetupEffect(EntityEffectManager effectManager) {
        base.SetupEffect(effectManager);
        ep = effectManager.RootGet<EntityPhysics>();
        eb = effectManager.RootGet<EntityBrain>();
        freezeForce = Force.Const(Vector3.zero, 1, OfficialDuration);
        ep.Add(freezeForce, EntityPhysics.PhysicPriority.BLOCK);
        if (eb != null) {
            eb.CanAct = false;
        }
    }

    public override bool EndEffect() {
        if (!base.EndEffect()) {
            return false;
        }

        if (eb != null) {
            eb.CanAct = true;
        }

        ep.Remove(freezeForce);
        return true;
    }
}

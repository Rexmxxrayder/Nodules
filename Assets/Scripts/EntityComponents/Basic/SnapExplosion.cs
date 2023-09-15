using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapExplosion : AreaDamage3D {

    protected override void ResetSetup() {
        OnEnter += ApplyFire;
    }

    private void ApplyFire(EntityHealth health) {
        EntityEffectManager effectManager = health.RootGet<EntityEffectManager>();
        if (effectManager != null) {
            FireEffect fireEffect = new();
            fireEffect.AddStack(1);
            fireEffect.Duration = 6;
            effectManager.AddEffect(fireEffect);
        }
    }
}

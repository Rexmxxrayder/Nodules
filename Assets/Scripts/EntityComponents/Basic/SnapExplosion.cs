using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapExplosion : AreaDamage3D {

    protected override void ResetSetup() {
        OnEnter += ApplyEffect;
    }

    private void ApplyEffect(EntityHealth health) {
        EntityEffectManager effectManager = health.RootGet<EntityEffectManager>();
        if (effectManager != null) {
            FireEffect fireEffect = new();
            fireEffect.AddStack(1);
            effectManager.AddEffect(fireEffect);
            FocusEffect focusEffect = new();
            effectManager.AddEffect(focusEffect);
        }
    }
}

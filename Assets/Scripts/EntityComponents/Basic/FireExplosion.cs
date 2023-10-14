using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosion : AreaDamage3D
{
    [SerializeField] private AreaDamage3D FireZone;

    protected override void ResetSetup() {
        OnDeath += SpawnFireZone;
    }

    private void SpawnFireZone() {
        AreaDamage3D areaDamage = Instantiate(FireZone);
        areaDamage.Spawn(GetRootPosition());
        areaDamage.OnStayDamage += ApplyFire;
    }

    private void ApplyFire(EntityHealth health) {
        EntityEffectManager effectManager = health.RootGet<EntityEffectManager>();
        if (effectManager != null) {
            FireEffect fireEffect = new();
            fireEffect.AddStack(1);
            fireEffect.CurrentDuration = 6;
            effectManager.AddEffect(fireEffect);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapExplosion : AreaDamage
{
    public int damage;
    public List<string> Damaged = new();
    void DoDamageCollider(Collider collider) {

        EntityHealth health = collider.gameObject.RootGet<EntityHealth>();
        if (health != null && Damaged.Contains(health.GetRoot().tag)) {
            health.RemoveHealth(damage);
        }

        EntityEffectManager effectManager = collider.gameObject.RootGet<EntityEffectManager>();
        if (effectManager != null) {
            FireEffect fireEffect = new();
            fireEffect.AddStack(1);
            fireEffect.Duration = 6;
            effectManager.AddEffect(fireEffect);
        }
    }

    protected override void LoadSetup() {
        RootGet<EntityMainCollider3D>().OnTriggerEnterDelegate += DoDamageCollider;
        base.LoadSetup();
    }
}

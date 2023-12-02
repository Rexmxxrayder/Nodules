using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowderEffect : EntityEffect {
    public override EffectType Type => EffectType.POWDER;

    public override int MaxStack => 1;
    public override float OfficialDuration => 5;
    public int Damage => 12;

    public static PowderCollider powderColliderPrefab;
    private PowderCollider powderCollider;


    public override void SetupEffect(EntityEffectManager entityEffectManager) {
        base.SetupEffect(entityEffectManager);
        powderCollider = GameObject.Instantiate(powderColliderPrefab, entityEffectManager.GetRootTransform());
    }

    public override bool EndEffect() {
        if (!base.EndEffect()) {
            return false;
        }
        entityEffectManager.GetRootComponent<EntityHealth>().RemoveHealth(Damage, "Powder");
        GameObject.Destroy(powderCollider.gameObject);
        return true;
    }
}

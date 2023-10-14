using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEffect : EntityEffect {
    public override EffectType Type => EffectType.NONE;

    public override int MaxStack => 1;

    public override float OfficialDuration => 6f;

    private const float TICKBURN = 1f;
    private const int damageEveryTick = 1;
    private float lastTick = 0f;
    private EntityHealth entityHealth;

    public override void SetupEffect(EntityEffectManager effectManager) {
        base.SetupEffect(effectManager);
        entityHealth = effectManager.RootGet<EntityHealth>();
        Currentduration = 6.01f;
    }
    public override void UpdateEffect(float deltaTime) {
        lastTick += deltaTime;
        if (lastTick / TICKBURN >= 1f) {
            entityHealth.RemoveHealth(damageEveryTick, "Burn");
            lastTick %= TICKBURN;
        }

        base.UpdateEffect(deltaTime);
    }
}

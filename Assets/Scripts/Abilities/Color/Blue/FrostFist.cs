using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntityEffect;

public class FrostFist : AreaDamage3D
{
    [SerializeField] private float damageMultiplierIfStatus = 3;
    protected override void AddEntityHealth(EntityHealth health, bool Collision) {
        if (health != null && !entitiesInside.ContainsKey(health) && damageables.Contains(health.GetRoot().tag)) {
            entitiesInside.Add(health, 0f);
            if (enterDamage != 0) {
                EntityEffectManager eem = health.GetRootComponent<EntityEffectManager>();
                int damages = enterDamage;
                if (eem != null) {
                    if (eem.Contains(new List<EffectType> { EffectType.ICE, EffectType.POWDER, EffectType.FREEZE, EffectType.FOCUS, EffectType.EMERALD, EffectType.INSANITY , EffectType.MADNESS})) {
                        damages = (int)(damages * damageMultiplierIfStatus);
                    }
                }
                health.RemoveHealth(damages, damageType);
            }

            health.GetRoot().OnDeath += () => entitiesInside.Remove(health);
            onEnter?.Invoke(health);
        }

        if (Collision && destroyOnCollision || !Collision && destroyOnTrigger) {
            Die();
        }
    }
}

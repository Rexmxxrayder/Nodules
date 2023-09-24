using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusEffect : EntityEffect {


    public override EffectType Type => EffectType.FOCUS;

    public override int MaxStack => 1;

    public override float StartDuration => 5f;

    EntityHealthModfier ehm;
    IHealthModifier modifier;
    EntityHealth eh;
    public override void SetupEffect(EntityEffectManager effectManager) {
        base.SetupEffect(effectManager);
        modifier = new DoubleDamageModifer();
        ehm = effectManager.RootGet<EntityHealthModfier>();
        ehm.AddModifier(modifier);
        eh = effectManager.RootGet<EntityHealth>();
        eh.OnDamaged += RemoveModifier;
    }

    private void RemoveModifier(int value, string type) {
        if (type == "") {
            ehm.RemoveModifier(modifier);
            eh.OnDamaged -= RemoveModifier;
        }
    }
}

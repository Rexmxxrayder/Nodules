using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEmpty : Ability {
    private void Start() {
        Cooldown = 0f;
    }
    protected override void LaunchAbility(EntityBrain brain) {
        
    }

    public override void Activate(EntityBrain brain) {
        LaunchAbility(brain);
        foreach (Ability ability in abilities) {
            ability.Activate(brain);
        }
    }
}

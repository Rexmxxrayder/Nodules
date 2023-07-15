using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEmpty : Ability {
    private void Start() {
        Cooldown = 0f;
    }

    public override void Activate(EntityBrain brain, bool isUp) {
        LaunchAbilityUp(brain);
        foreach (Ability ability in abilities) {
            ability.Activate(brain, isUp);
        }
    }
}

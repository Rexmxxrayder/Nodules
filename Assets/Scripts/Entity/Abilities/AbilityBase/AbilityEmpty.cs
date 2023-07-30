using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEmpty : Ability {
    private void Start() {
        Cooldown = 0f;
    }

    public override void Launch(EntityBrain brain, bool isUp) {
        foreach (Ability ability in abilities) {
            ability.Launch(brain, isUp);
        }
    }
}

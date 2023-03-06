using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerBody : Body {
    [SerializeField] BodyPart A;
    [SerializeField] BodyPart Z;
    [SerializeField] BodyPart E;
    [SerializeField] BodyPart R;
    [SerializeField] BodyPart Space;

    private void Start() {
        AssignBodyPart();
    }
    protected override void NewBrain(Brain newBrain) {
        brain = newBrain;
    }

    void AssignBodyPart() {
        if (A != null) {
            brain.FirstAbility += () => A.LaunchAbility(brain.Visor);
        }
        if (Z != null) {
            brain.SecondAbility += () => Z.LaunchAbility(brain.Visor);
        }
        if (E != null) {
            brain.ThirdAbility += () => E.LaunchAbility(brain.Visor);
        }
        if (R != null) {
            brain.FourthAbility += () => R.LaunchAbility(brain.Visor);
        }
        if (Space != null) {
            brain.FifthAbility += () => Space.LaunchAbility(brain.Visor);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {
    [SerializeField] List<Ability> abilities = new List<Ability>();
    public void Activate((Brain, Body) bodyBrain) {
        LaunchAbility(bodyBrain);
        foreach (Ability ability in abilities) {
            ability.Activate(bodyBrain);
        }
    }
    protected abstract void LaunchAbility((Brain, Body) bodyBrain);
}

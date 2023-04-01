using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {
    public static bool isDebug = true;
    [SerializeField] string abilityName;
    List<Ability> abilities = new List<Ability>();
    public void Activate((Brain, Body) bodyBrain) {
        if (isDebug) {
            ActivateDebug();
        }
        LaunchAbility(bodyBrain);
        foreach (Ability ability in abilities) {
            ability.Activate(bodyBrain);
        }
    }

    private void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            Ability childAbility = transform.GetChild(i).GetComponent<Ability>();
            if (childAbility != null) {
                abilities.Add(childAbility);
            }
        }
    }
    protected abstract void LaunchAbility((Brain, Body) bodyBrain);

    protected virtual void ActivateDebug() {
        Debug.Log("Launch : " + abilityName);
    }
}

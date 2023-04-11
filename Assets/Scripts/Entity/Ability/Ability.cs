using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {
    protected EntityBodyPart entityBodyPart;
    public static bool isDebug = false;
 //   [SerializeField] string abilityName;
    List<Ability> abilities = new List<Ability>();
    public void Activate(EntityBrain brain) {
        //if (isDebug) {
        //    ActivateDebug();
        //}
        LaunchAbility(brain);
        foreach (Ability ability in abilities) {
            ability.Activate(brain);
        }
    }

    protected void Awake() {
        entityBodyPart = GetComponentInParent<EntityBodyPart>();
        for (int i = 0; i < transform.childCount; i++) {
            Ability childAbility = transform.GetChild(i).GetComponent<Ability>();
            if (childAbility != null) {
                abilities.Add(childAbility);
            }
        }
    }
    protected abstract void LaunchAbility(EntityBrain brain);

    //protected virtual void ActivateDebug() {
    //    Debug.Log("Launch : " + abilityName);
    //}
}

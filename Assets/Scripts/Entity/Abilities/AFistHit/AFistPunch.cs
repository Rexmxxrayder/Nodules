using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class AFistPunch : Ability {
    [SerializeField] GameObject prefab;
    [SerializeField] float fistSpeed;
    protected override void LaunchAbility(EntityBrain brain) {
        if(GetComponentInParent<EntityBodyParts>().isDashing) {
            return;
        }
        GetComponentInParent<EntityBodyParts>().onMovement?.Invoke();
        Vector3 cursor = brain.Visor;
        GameObject newFPE = Instantiate(prefab, GetComponentInParent<EntityBodyParts>().GetRoot().transform);
        newFPE.transform.position = GetComponentInParent<EntityBodyParts>().GetRoot().transform.position;
        newFPE.transform.rotation = Quaternion.Euler(0, 0, RotationSloot.GetDegreeBasedOfTarget(newFPE.transform.position, cursor, RotationSloot.TranslateVector3("z")));
        newFPE.GetComponentInChildren<Animator>().SetFloat("FistSpeed", fistSpeed);
        GetComponentInParent<EntityBodyParts>().OnMovement += () => Kill(newFPE);
    }

    void Kill(GameObject g) {
        if (g != null) {
            g.SetActive(false);
            Destroy(g);
        }
        GetComponentInParent<EntityBodyParts>().OnMovement -= () => Kill(g);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class AFistPunch : Ability {
    [SerializeField] GameObject prefab;
    [SerializeField] float fistSpeed;
    protected override void LaunchAbilityUp(EntityBrain brain) {
        Vector3 cursor = brain.Visor;
        GameObject newFPE = Instantiate(prefab, gameObject.GetRootTransform());
        newFPE.transform.position = gameObject.GetRootPosition();
        newFPE.transform.rotation = Quaternion.Euler(0, 0, RotationSloot.GetDegreeBasedOfTarget(newFPE.transform.position, cursor, RotationSloot.TranslateVector3("z")));
        newFPE.GetComponentInChildren<Animator>().SetFloat("FistSpeed", fistSpeed);
    }

    void Kill(GameObject g) {
        if (g != null) {
            g.SetActive(false);
            Destroy(g);
        }
    }
}

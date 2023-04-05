using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class AFistPunch : Ability {
    [SerializeField] GameObject prefab;
    [SerializeField] int hitDamage;
    [SerializeField] float fistSpeed;
    protected override void LaunchAbility((Brain, Body) bodyBrain) {
        Vector3 cursor = bodyBrain.Item1.Visor;
        GameObject newFPE = Instantiate(prefab, bodyBrain.Item2.gameObject.transform);
        newFPE.transform.position = bodyBrain.Item2.gameObject.transform.position;
        newFPE.transform.rotation = Quaternion.Euler(0,0,RotationSloot.GetDegreeBasedOfTarget(newFPE.transform.position, cursor, RotationSloot.TranslateVector3("z")));
        newFPE.GetComponentInChildren<Animator>().SetFloat("FistSpeed", fistSpeed);
        newFPE.GetComponentInChildren<EntityDamageCollider2D>().SetDamage(hitDamage);
    }
}

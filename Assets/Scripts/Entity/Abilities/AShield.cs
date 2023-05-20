using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class AShield : Ability
{
    [SerializeField] Shield shield;
    [SerializeField] float shieldDuration;
    protected override void LaunchAbility(EntityBrain brain) {
        GetComponentInParent<EntityBodyParts>().onMovement?.Invoke();
        Vector3 cursor = brain.Visor;
        Shield newFPE = Instantiate(shield, GetComponentInParent<EntityBodyParts>().GetRoot().transform);
        newFPE.transform.SetPositionAndRotation(GetComponentInParent<EntityBodyParts>().GetRoot().transform.position, Quaternion.Euler(0, 0, RotationSloot.GetDegreeBasedOfTarget(newFPE.transform.position, cursor, RotationSloot.TranslateVector3("z"))));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class AShield : Ability {
    [SerializeField] WindShield _shield;
    [SerializeField] float _shieldDuration;
    protected override void LaunchAbility(EntityBrain brain) {
        GetComponentInParent<EntityBodyParts>().onMovement?.Invoke();
        Vector3 cursor = brain.Visor;
        WindShield windShield = Instantiate(_shield, brain.GetRoot().transform);
        windShield.Duration = _shieldDuration;
        windShield.transform.SetPositionAndRotation(GetComponentInParent<EntityBodyParts>().GetRoot().transform.position, Quaternion.Euler(0, 0, RotationSloot.GetDegreeBasedOfTarget(windShield.transform.position, cursor, RotationSloot.TranslateVector3("z"))));
    }
}

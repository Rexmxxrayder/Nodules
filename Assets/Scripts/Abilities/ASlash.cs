using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;
using Unity.VisualScripting;

public class ASlash : Ability {
    public GameObject slashPrefab;
    public float Speed;
    public float Dist;
    Force dashForce = Force.Empty();
    public float SlashSpeed;


    protected override void LaunchAbilityUp(EntityBrain brain) {
        EntityPhysics ep = gameObject.RootGet<EntityPhysics>();
        Vector3 startPosition = gameObject.GetRoot().GetRootPosition();
        Vector3 goToPosition = brain.Visor;
        DashTo(ep, startPosition, goToPosition);
        StartCooldown();
    }

    void Slash(Vector3 visor) {
        GameObject slash = Instantiate(slashPrefab, transform);
        slash.SetActive(true);
        slash.transform.position = transform.position;
        slash.transform.rotation = Quaternion.Euler(0, RotationSloot.GetDegreeBasedOfTarget(transform.position, visor, RotationSloot.TranslateVector3("y")) - 180, 0);
        slash.GetComponentInChildren<Animator>().SetFloat("SlashSpeed", SlashSpeed);
    }

    void DashTo(EntityPhysics ep, Vector3 startPosition, Vector3 goToPosition) {
        ep.Remove(dashForce);
        dashForce = Force.Const(goToPosition - startPosition, Speed, Dist / Speed);
        dashForce.OnEnd += (_) => Slash(goToPosition + goToPosition.normalized);
        ep.Add(dashForce, EntityPhysics.PhysicPriority.DASH);
    }
}

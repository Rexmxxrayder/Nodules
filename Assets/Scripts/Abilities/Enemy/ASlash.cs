using UnityEngine;
using Sloot;

public class ASlash : Ability {
    public GameObject slashPrefab;
    public float Speed;
    public float Dist;
    Force dashForce = Force.Empty();
    public float SlashSpeed;
    public float distMinTotarget;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        EntityPhysics ep = gameObject.RootGet<EntityPhysics>();
        Vector3 startPosition = gameObject.GetRoot().GetRootPosition();
        Vector3 goToPosition = brain.Visor;
        DashTo(ep, brain.Selected);
        StartCooldown();
    }

    void Slash(Transform visor) {
        GameObject slash = Instantiate(slashPrefab, transform);
        slash.SetActive(true);
        slash.transform.position = transform.position;
        slash.transform.rotation = Quaternion.Euler(0, RotationSloot.GetDegreeBasedOfTarget(transform.position, visor.position, RotationSloot.TranslateVector3("y")) - 180, 0);
        slash.GetComponentInChildren<Animator>().SetFloat("SlashSpeed", SlashSpeed);
    }

    void DashTo(EntityPhysics ep, Transform target) {
        ep.Remove(dashForce);
        Vector3 position = ep.GetRootPosition();
        float distToTarget = Vector3.Distance(position, target.position);

        float distToDash = distToTarget > Dist + distMinTotarget ? Dist : distToTarget - distMinTotarget;
        if(distToDash < 0) {
            Slash(target);
            return;
        }

        dashForce = Force.Const(target.position - position, Speed, distToDash / Speed);
        dashForce.OnEnd += (_) => Slash(target);
        ep.Add(dashForce, EntityPhysics.PhysicPriority.DASH);
    }
}

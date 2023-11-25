using UnityEngine;
using Sloot;

public class ASlash : Ability {
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float dist;
    [SerializeField] private float slashSpeed;
    [SerializeField] private float distMinTotarget;
    [SerializeField] private float percentReduction;
    [SerializeField] private float flatReduction;
    Force dashForce = Force.Empty();

    public void SetupData(float cooldown, GameObject slashPrefab, float speed, float dist, float slashSpeed, float distMinTotarget) {
        this.cooldown = cooldown;
        this.slashPrefab = slashPrefab;
        this.speed = speed;
        this.dist = dist;
        this.slashSpeed = slashSpeed;
        this.distMinTotarget = distMinTotarget;
    }

    protected override void LaunchAbilityUp(EntityBrain brain) {
        EntityPhysics ep = gameObject.RootGet<EntityPhysics>();
        DashTo(ep, brain.Selected);
        StartCooldown();
    }

    void Slash(Transform visor) {
        GameObject slash = Instantiate(slashPrefab, transform);
        slash.SetActive(true);
        slash.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, RotationSloot.GetDegreeBasedOfTarget(transform.position, visor.position, RotationSloot.TranslateVector3("y")) - 180, 0));
        slash.GetComponentInChildren<Animator>().SetFloat("SlashSpeed", slashSpeed);
    }

    void DashTo(EntityPhysics ep, Transform target) {
        ep.Remove(dashForce);
        DamageReductionModifier dashResistance = new(percentReduction, flatReduction);
        ep.RootGet<EntityHealthModfier>().AddModifier(dashResistance);
        Vector3 position = ep.GetRootPosition();
        float distToTarget = Vector3.Distance(position, target.position);
        float distToDash = distToTarget > dist + distMinTotarget ? dist : distToTarget - distMinTotarget;
        if(distToDash < 0) {
            Slash(target);
            return;
        }

        dashForce = Force.Const(target.position - position, speed, distToDash / speed);
        dashForce.OnEnd += (_) => Slash(target);
        dashForce.OnEnd += (_) => ep.RootGet<EntityHealthModfier>().RemoveModifier(dashResistance);
        ep.Add(dashForce, EntityPhysics.PhysicPriority.DASH);
    }
}

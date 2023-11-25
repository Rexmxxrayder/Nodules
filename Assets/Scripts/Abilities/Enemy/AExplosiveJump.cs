using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AExplosiveJump : Ability {
    [SerializeField] private float distMaxJump, speedJump;
    [SerializeField] private AreaDamage3D areaDamage;
    [SerializeField] private float percentReduction;
    [SerializeField] private float flatReduction;
    private DamageReductionModifier dashResistance;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        dashResistance ??= dashResistance = new(percentReduction, flatReduction);
        StartCoroutine(Jump(brain.Visor));
    }

    public void SetupData(float cooldown, float distMaxJump, float speedJump, AreaDamage3D areaDamage) {
        this.cooldown = cooldown;
        this.distMaxJump = distMaxJump;
        this.speedJump = speedJump;
        this.areaDamage = areaDamage;
    }

    IEnumerator Jump(Vector3 destination) {
        EntityPhysics ep = gameObject.RootGet<EntityPhysics>();
        ep.RootGet<EntityHealthModfier>().RemoveModifier(dashResistance);
        StartCooldown();
        Vector3 position = gameObject.GetRootPosition();
        Vector3 directionJump = destination - position;
        float distJump = Mathf.Min(Vector3.Distance(destination, position), distMaxJump);
        ImpactDamage();
        Force jumpForce = Force.Const(directionJump, speedJump, distJump / speedJump);
        ep.Add(jumpForce, EntityPhysics.PhysicPriority.DASH);
        yield return new WaitForSeconds(distJump / speedJump);
        ep.RootGet<EntityHealthModfier>().AddModifier(dashResistance);
        ImpactDamage();
    }

    void ImpactDamage() {
        AreaDamage3D ad = Instantiate(areaDamage);
        ad.transform.position = gameObject.GetRootPosition();
        ad.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AExplosiveJump : Ability {
    [SerializeField] private float distMaxJump, speedJump;
    [SerializeField] private AreaDamage3D areaDamage;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        StartCoroutine(Jump(brain.Visor));
    }

    public void SetupData(float cooldown, float distMaxJump, float speedJump, AreaDamage3D areaDamage) {
        this.cooldown = cooldown;
        this.distMaxJump = distMaxJump;
        this.speedJump = speedJump;
        this.areaDamage = areaDamage;
    }

    IEnumerator Jump(Vector3 destination) {
        StartCooldown();
        Vector3 position = gameObject.GetRootPosition();
        Vector3 directionJump = destination - position;
        float distJump = Mathf.Min(Vector3.Distance(destination, position), distMaxJump);
        ImpactDamage();
        gameObject.RootGet<EntityPhysics>().Add(Force.Const(directionJump, speedJump, distJump / speedJump), EntityPhysics.PhysicPriority.DASH);
        yield return new WaitForSeconds(distJump / speedJump);
        ImpactDamage();

    }

    void ImpactDamage() {
        AreaDamage3D ad = Instantiate(areaDamage);
        ad.transform.position = gameObject.GetRootPosition();
        ad.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AExplosiveJump : Ability {
    [SerializeField] float distMaxJump;
    [SerializeField] float SpeedJump;
    [SerializeField] AreaDamage areaDamage;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        StartCoroutine(Jump(brain.Visor));
    }

    IEnumerator Jump(Vector3 destination) {
        StartCooldown();
        Vector3 position = gameObject.GetRootPosition();
        Vector3 directionJump = destination - position;
        float distJump = Mathf.Min(Vector3.Distance(destination, position), distMaxJump);
        ImpactDamage();
        gameObject.RootGet<EntityPhysics>().Add(Force.Const(directionJump, SpeedJump, distJump / SpeedJump), EntityPhysics.PhysicPriority.DASH);
        yield return new WaitForSeconds(distJump / SpeedJump);
        ImpactDamage();

    }

    void ImpactDamage() {
        AreaDamage ad = Instantiate(areaDamage);
        ad.transform.position = gameObject.GetRootPosition();
        ad.gameObject.SetActive(true);
    }
}

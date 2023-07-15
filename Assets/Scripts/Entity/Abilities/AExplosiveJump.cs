using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AExplosiveJump : Ability {
    [SerializeField] float distMaxJump;
    [SerializeField] float SpeedJump;
    [SerializeField] string areaDamage;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        StartCoroutine(Jump(brain.Visor));
    }

    IEnumerator Jump(Vector2 destination) {
        Vector2 position = (Vector2)gameObject.GetRootPosition();
        Vector2 directionJump = destination - position;
        float distJump = Mathf.Min(Vector3.Distance(destination, position), distMaxJump);
        ImpactDamage();
        gameObject.RootGet<EntityPhysics>().Add(Force.Const(directionJump, SpeedJump, distJump / SpeedJump), EntityPhysics.PhysicPriority.DASH);
        yield return new WaitForSeconds(distJump / SpeedJump);
        ImpactDamage();
    }

    void ImpactDamage() {
        AreaDamage ad = (AreaDamage)BasicPools.Gino.GetInstance(areaDamage);
        ad.transform.position = gameObject.GetRootPosition();
    }
}

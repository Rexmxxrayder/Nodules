using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AExplosiveJump : Ability {
    [SerializeField] float distMaxJump;
    [SerializeField] float SpeedJump;
    [SerializeField] string areaDamage;

    protected override void LaunchAbility(EntityBrain brain) {
        StartCoroutine(Jump(brain.Visor));
    }

    IEnumerator Jump(Vector2 destination) {
        Vector2 directionJump;
        float distJump;
        ImpactDamage();
        directionJump = destination - (Vector2)GetComponentInParent<EntityBodyParts>().GetRoot().transform.position;
        distJump = Mathf.Min(Vector3.Distance(destination, (Vector2)GetComponentInParent<EntityBodyParts>().GetRootPosition()), distMaxJump);
        GetComponentInParent<EntityRoot>().Get<EntityPhysics>().Add(Force.Const(directionJump, SpeedJump, distJump / SpeedJump), 20);
        yield return new WaitForSeconds(distJump / SpeedJump);
        ImpactDamage();
    }

    void ImpactDamage() {
        AreaDamage ad = (AreaDamage)BasicPools.Gino.GetInstance(areaDamage);
        ad.transform.position = transform.position;
        ad.Activate();
    }
}

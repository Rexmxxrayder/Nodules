using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sauteur : EntityEnemy {
    public Transform target;
    public string areaDamage;
    public float TimeBeforeJump;
    public int JumpDamage;
    public float DistMaxJump;
    public float SpeedJump;
    public float TimeAfterJump;
    EntityPhysics ep;

    protected override void AwakeSetup() {
        ep = gameObject.Get<EntityPhysics>();
    }
    IEnumerator Jump() {
        Vector3 directionJump;
        float distJump;
        while (true) { 
            yield return new WaitForSeconds(TimeBeforeJump);
            ImpactDamage();
            directionJump = target.position - transform.position;
            distJump = Mathf.Min(Vector3.Distance(target.position, transform.position), DistMaxJump);
            ep.Add(Force.Const(directionJump, SpeedJump, distJump / SpeedJump), 20);
            yield return new WaitForSeconds(distJump / SpeedJump);
            ImpactDamage();
            yield return new WaitForSeconds(TimeAfterJump);
        }
    }

    void ImpactDamage() {
        AreaDamage ad = (AreaDamage)BasicPools.Gino.GetInstance(areaDamage);
        ad.transform.position = transform.position;
        ad.Activate();
    }

    public override void InstanceReset() {
        throw new System.NotImplementedException();
    }

    public override void InstanceResetSetup() {
        if (target == null) {
            target = FindObjectOfType<EntityBodyPart>().transform;
        }
        StartCoroutine(Jump());
    }
}

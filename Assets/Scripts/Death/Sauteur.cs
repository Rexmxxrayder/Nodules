using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sauteur : MonoBehaviour {
    public Transform target;
    public float TimeBeforeJump;
    public float TimeDamage;
    public float DistMaxJump;
    public float SpeedJump;
    public float TimeAfterJump;
    EntityPhysics ep;

    private void Start() {
        StartCoroutine(Jump());
        ep = gameObject.GetComponentInChildren<EntityPhysics>();
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
        AreaDamage ad = (AreaDamage)BasicPrefabs.Gino.GetInstance("CircleAreaDamage");
        ad.timeDamage = TimeDamage;
        ad.transform.position = transform.position;
        ad.Activate(gameObject.Get<EntityRoot>());
    }
}

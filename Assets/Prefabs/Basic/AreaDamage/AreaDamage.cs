using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : EntityBasic {
    [SerializeField] EntityDamageCollider2D edc;
    public float timeDamage;

    public override void Activate() {
        StartCoroutine(DeathCooldown());
    }

    IEnumerator DeathCooldown() {
        if(timeDamage == 0f) {
            yield return null;
        } else {
            yield return new WaitForSeconds(timeDamage);
        }
        gameObject.Get<EntityDeath>().Die();
    }
}

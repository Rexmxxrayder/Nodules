using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : Basic {
    [SerializeField] EntityDamageCollider2D edc;
    public float timeDamage;
    public int damage;

    public override void Activate() {
        edc.InstanceReset();
        edc.damage = damage;
        StartCoroutine(DeathCooldown());
    }

    IEnumerator DeathCooldown() {
        yield return new WaitForSeconds(timeDamage);
        gameObject.Get<EntityHealth>().LethalDamage();
    }
}

using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : Basic {
    [SerializeField] EntityDamageCollider2D edc;
    public float timeDamage;

    public override void Activate(EntityRoot root) {
        edc.InstanceReset();
        if (root.CompareTag("Ennemi")) {
            edc.Damaged.Add("Player");
        }
        StartCoroutine(DeathCooldown());
    }

    IEnumerator DeathCooldown() {
        yield return new WaitForSeconds(timeDamage);
        BasicPrefabs.Gino.LetInstance(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIcicleCanon : Ability {
    public Bullet bulletprefab;
    public float timeForShoot;

    protected override void LaunchAbilityDown(EntityBrain brain) {
        Shoot(brain);
    }

    void Shoot(EntityBrain brain) {
        Bullet currentBullet = Instantiate(bulletprefab, transform);
        currentBullet.Fire(brain.Visor - GetComponentInParent<EntityBodyPart>().GetRootPosition());
        StartCoroutine(AbilityTime());
    }

    IEnumerator AbilityTime() {
        yield return new WaitForSeconds(timeForShoot);
        StartCooldown();
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIcicleCanon : Ability {
    public Bullet bulletprefab;
    public float timeForShoot;
    Coroutine coroutine;

    protected override void LaunchAbilityDown(EntityBrain brain) {
        coroutine ??= StartCoroutine(StopPlayer());
        Shoot(brain);

    }

    void Shoot(EntityBrain brain) {
        Bullet currentBullet = Instantiate(bulletprefab, transform);
        currentBullet.Fire(brain.Visor - GetComponentInParent<EntityBodyPart>().GetRootPosition());
    }

    IEnumerator StopPlayer() {
        EntityPhysics ep = gameObject.RootGet<EntityPhysics>();
        Force rootForce = Force.Const(Vector3.zero, 1, timeForShoot);
        ep.Add(rootForce, EntityPhysics.PhysicPriority.INPUT);
        float timer = 0f;
        while (timer < timeForShoot && !rootForce.HasEnded) {
            timer += Time.deltaTime;
            yield return null;
        }
        StartCooldown();
        coroutine = null;
    }

    public override void Cancel() {
        if(coroutine != null) {
            StopCoroutine(coroutine);
            coroutine = null;
            StartCooldown();
        }
    }
}

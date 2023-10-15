using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AShootBullet : Ability {
    public Bullet bulletprefab;
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    [SerializeField] private bool freeChild;
    [SerializeField] private float bulletNumber = 1f;
    [SerializeField] private float timeForShoot = 5f;
    private Coroutine coroutine;
    private int numberShot = 0;
    protected override void LaunchAbilityUp(EntityBrain brain) {
        coroutine ??= StartCoroutine(ShootTime());
        Shoot(brain.Visor - GetComponentInParent<EntityBodyPart>().GetRootPosition());
    }

    void Shoot(Vector3 direction) {
        numberShot++;
        direction.y = 0;
        direction.Normalize();
        Bullet newBullet = freeChild ? Instantiate(bulletprefab, transform.position, Quaternion.identity) : Instantiate(bulletprefab, transform);
        newBullet.gameObject.SetActive(false);
        newBullet.transform.position = transform.position;
        newBullet.Fire(direction, speed, maxDistance);
    }

    IEnumerator ShootTime() {
        numberShot = 0;
        float timer = 0f;
        while (timer < timeForShoot && numberShot < bulletNumber) {
            timer += Time.deltaTime;
            yield return null;
        }
        StartCooldown();
        coroutine = null;
    }

    public override void Cancel() {
        if (coroutine != null) {
            StopCoroutine(coroutine);
            coroutine = null;
            StartCooldown();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AShootBullet : Ability {
    public Bullet bulletprefab;
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    [SerializeField] private bool freeChild;
    protected override void LaunchAbilityUp(EntityBrain brain) {
        Shoot(brain.Visor - GetComponentInParent<EntityBodyPart>().GetRootPosition());
    }

    void Shoot(Vector3 direction) {
        direction.y = 0;
        direction.Normalize();
        Bullet newBullet = freeChild ? Instantiate(bulletprefab, transform.position, Quaternion.identity) : Instantiate(bulletprefab, transform);
        newBullet.gameObject.SetActive(false);
        newBullet.transform.position = transform.position;
        newBullet.Fire(direction, speed, maxDistance);
        StartCooldown();
    }
}
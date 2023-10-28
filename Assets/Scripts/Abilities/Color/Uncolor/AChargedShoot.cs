using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AChargedShoot : Ability {
    public Bullet bulletprefab;
    public Bullet currentBullet;
    public bool shoot = false;
    public float maxCharge;
    public float maxDamage;
    public Slider slider;
    public Vector2 damage;
    public Vector2 size;
    protected override void LaunchAbilityDown(EntityBrain brain) {
        Charge(brain);
    }

    protected override void LaunchAbilityUp(EntityBrain brain) {
        shoot = true;
    }

    void Charge(EntityBrain brain) {
        shoot = false;
        currentBullet = Instantiate(bulletprefab, transform);
        currentBullet.gameObject.SetActive(false);
        currentBullet.transform.position = transform.position;
        StartCoroutine(ChargeShoot(brain));
    }
    
    IEnumerator ChargeShoot(EntityBrain brain) {
        slider.gameObject.SetActive(true);
        float time = 0f;
        slider.maxValue = maxCharge;
        slider.minValue = 0;
        slider.value = 0;
        while (!shoot) {
            time += Time.deltaTime;
            if (time >= maxCharge) {
                shoot = true;
            }
            slider.value = time;
            slider.transform.parent.LookAt(slider.transform.parent.position - Camera.main.transform.forward);
            yield return null;
        }

        currentBullet.Fire(brain.Visor - GetComponentInParent<EntityBodyPart>().GetRootPosition());
        slider.gameObject.SetActive(false);
        currentBullet.GetComponentInChildren<AreaDamage3D>().enterDamage = (int)Mathf.Lerp(damage.x, damage.y, Mathf.InverseLerp(0, maxCharge, time));
        currentBullet.transform.localScale = Vector3.one * Mathf.Lerp(size.x, size.y, Mathf.InverseLerp(0, maxCharge, time)); ;
        currentBullet = null;
    }
    public override void Cancel() {
        if(currentBullet != null) {
            Destroy(currentBullet);
        }

        StopAllCoroutines();
    }
}
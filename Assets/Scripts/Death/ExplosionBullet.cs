using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class ExplosionBullet : Bullet {
    [SerializeField] string explosionType;
    protected override void ResetSetup() {
        base.ResetSetup();
        gameObject.GetRoot().OnDeath += YellowBoom;
    }

    void YellowBoom() {
        //AreaDamage ad = (AreaDamage)BasicPools.Gino.GetInstance(explosionType);
        //ad.transform.position = transform.position;
    }    
}

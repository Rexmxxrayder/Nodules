using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class ExplosionBullet : Bullet {
    [SerializeField] string explosionType;
    protected override void StartSetup() {
        base.StartSetup();
        gameObject.Get<EntityDeath>().OnDeath += YellowBoom;
    }

    void YellowBoom() {
        AreaDamage ad = (AreaDamage)BasicPools.Gino.GetInstance(explosionType);
        ad.transform.position = transform.position;
    }    
}

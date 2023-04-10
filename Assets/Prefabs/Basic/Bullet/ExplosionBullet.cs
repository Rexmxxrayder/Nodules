using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class ExplosionBullet : Bullet {
    [SerializeField] string explosionType;
    public override void Activate() {
        base.Activate();
        gameObject.Get<EntityHealth>().OnDeath += YellowBoom;
    }

    void YellowBoom() {
        AreaDamage ad = (AreaDamage)BasicPools.Gino.GetInstance(explosionType);
        ad.transform.position = transform.position;
        ad.Activate();
    }    
}

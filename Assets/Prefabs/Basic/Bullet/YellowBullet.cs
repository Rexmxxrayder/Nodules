using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class YellowBullet : Bullet {
    public int explosiondamage;
    public override void Activate() {
        EntityHealth eh = gameObject.Get<EntityHealth>();
        beforeDeath = new Timer(this, lifeTime, () => eh.LethalDamage()).Start();
        eh.OnDeath += YellowBoom;
    }

    void YellowBoom() {
        AreaDamage ad = (AreaDamage)BasicPrefabs.Gino.GetInstance("YellowBulletAreaDamage");
        ad.damage = explosiondamage;
        ad.transform.position = transform.position;
        ad.Activate();
    }
}

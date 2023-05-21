using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBasic : EntityRoot {
    [SerializeField] float _duration = 1;
    public float Duration { get { return _duration; } set { _duration = value; } }
    protected override void StartSetup() {
        if(Get<EntityDeath>() == null) {
            GetRoot().AddComponent<EntityDeath>();
        }
        StartCoroutine(LifeCooldown());
    }

    IEnumerator LifeCooldown() {
        yield return new WaitForSeconds(Duration);
        Get<EntityDeath>().Die();
    }
}

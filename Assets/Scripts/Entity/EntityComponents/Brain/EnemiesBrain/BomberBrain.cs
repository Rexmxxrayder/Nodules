using Sloot;
using UnityEngine;

public class BomberBrain : EntityBrain {
    public Transform target;
    public string areaDamage;
    public float TimeBeforeExplode;
    float timerBeforeExplode = 0;
    EntityCollider2D ec;

    void Update() {
        visor = target.position;
        Get<EntityBodyParts>().Bodyparts[0].OnButtonUp(this);
        timerBeforeExplode += Time.deltaTime;
            if (timerBeforeExplode > TimeBeforeExplode) {
                gameObject.Get<EntityDeath>().Die();
            }
    }

    void MustExplode(Collision2D c) {
        if (!c.gameObject.CompareTag("Player"))
            return;
        gameObject.Get<EntityDeath>().Die();
    }
    void Explode() {
        AreaDamage ad = (AreaDamage)BasicPools.Gino.GetInstance(areaDamage);
        ad.transform.position = transform.position;
        ad.Activate();
    }

    protected override void AwakeSetup() {
        base.AwakeSetup();
        ec = gameObject.Get<EntityCollider2D>();
    }


    public override void InstanceReset() {
        timerBeforeExplode = 0;
    }

    public override void InstanceResetSetup() {
        gameObject.Get<EntityDeath>().OnDeath += Explode;
        ec.OnCollisionEnter += MustExplode;
        if (target == null) {
            target = FindObjectOfType<PlayerBrain>().transform;
        }
    }
}

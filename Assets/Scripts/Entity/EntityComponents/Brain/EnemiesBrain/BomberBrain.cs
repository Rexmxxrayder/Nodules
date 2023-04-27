using Sloot;
using UnityEngine;

public class BomberBrain : EntityBrain, IReset {
    public Transform target;
    public string areaDamage;
    public float TimeBeforeExplode;
    float timerBeforeExplode = 0;
    EntityPhysics ep;
    EntityCollider2D ec;
    Force follow = Force.Const(Vector3.zero, 0f);

    void Update() {
        visor = target.position;
        Get<EntityBodyParts>().Bodyparts[0].OnButtonUp(this);
        timerBeforeExplode += Time.deltaTime;
            if (timerBeforeExplode > TimeBeforeExplode) {
                gameObject.Get<EntityHealth>().LethalDamage();
            }
    }

    void MustExplode(Collision2D c) {
        if (!c.gameObject.CompareTag("Player"))
            return;
        gameObject.Get<EntityHealth>().LethalDamage();
    }
    void Explode() {
        AreaDamage ad = (AreaDamage)BasicPools.Gino.GetInstance(areaDamage);
        ad.transform.position = transform.position;
        ad.Activate();
    }

    protected override void AwakeSetup() {
        ep = gameObject.Get<EntityPhysics>();
        ec = gameObject.Get<EntityCollider2D>();
    }

    public void InstanceReset() {
        timerBeforeExplode = 0;
    }

    public void InstanceResetSetup() {
        gameObject.Get<EntityHealth>().OnDeath += Explode;
        ec.OnCollisionEnter += MustExplode;
        if (target == null) {
            target = FindObjectOfType<PlayerBrain>().transform;
        }
    }
}

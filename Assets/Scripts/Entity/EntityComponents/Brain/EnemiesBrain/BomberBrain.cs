using Sloot;
using UnityEngine;

public class BomberBrain : EntityBrain {
    public Transform target;
    public string areaDamage;
    public float TimeBeforeExplode;
    float timerBeforeExplode = 0;
    EntityCollider2D ec;
    [SerializeField] private EntityBodyPart follow;

    void Update() {
        visor = target.position;
        follow.KeyEvenement(true);
        timerBeforeExplode += Time.deltaTime;
            if (timerBeforeExplode > TimeBeforeExplode) {
                gameObject.Die();
            }
    }

    void MustExplode(Collision2D c) {
        if (!c.gameObject.CompareTag("Player"))
            return;
        gameObject.Die();
    }
    void Explode() {
        AreaDamage ad = (AreaDamage)BasicPools.Gino.GetInstance(areaDamage);
        ad.transform.position = transform.position;
    }

    protected override void DefinitveSetup() {
        base.DefinitveSetup();
        ec = gameObject.RootGet<EntityCollider2D>();
        timerBeforeExplode = 0;
    }

    protected override void ResetSetup() {
        gameObject.GetRoot().OnDeath += Explode;
        ec.OnCollisionEnter += MustExplode;
        if (target == null) {
            target = FindObjectOfType<PlayerBrain>().transform;
        }
    }
}

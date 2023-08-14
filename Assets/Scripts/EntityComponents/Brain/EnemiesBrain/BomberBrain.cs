using Sloot;
using System.Collections;
using UnityEngine;

public class BomberBrain : EntityBrain {
    [SerializeField] private EntityBasic explosion;
    public float TimeBeforeExplode;
    float timerBeforeExplode = 0;
    EntityBodyPart bodyPart;

    void Update() {
        Debug.DrawLine(transform.position, visor, Color.red);

        timerBeforeExplode += Time.deltaTime;
        if (timerBeforeExplode > TimeBeforeExplode) {
            Die();
        }
    }

    void DieOnCollision(Collision c) {
        if (!c.gameObject.CompareTag("Player"))
            return;
        Die();
    }

    protected void Explode() {
        explosion.transform.parent = null;
        explosion.gameObject.SetActive(true);
    }

    protected override void LoadSetup() {
        base.LoadSetup();
        gameObject.GetRoot().OnDeath += Explode;
        RootGet<EntityCollider3D>().OnCollisionEnterDelegate += DieOnCollision;
        selected = HMove.instance.transform;
        bodyPart = RootGet<EntityBodyPart>();
        bodyPart.KeyEvenement(true);
    }
}

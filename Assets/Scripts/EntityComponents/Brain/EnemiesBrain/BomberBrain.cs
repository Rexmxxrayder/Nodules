using UnityEngine;

public class BomberBrain : EntityBrain {
    [SerializeField] private BomberData BomberData;
    [SerializeField] private EntityBasic explosion;
    [SerializeField] private float timeBeforeExplode;
    private float timerBeforeExplode = 0;
    private EntityBodyPart bodyPart;

    protected override void ResetSetup() {
        base.ResetSetup();
    }
    public void SetupData(float timeBeforeExplode) {
        this.timeBeforeExplode = timeBeforeExplode;
    }

    protected override void LoadSetup() {
        base.LoadSetup();
        gameObject.GetRoot().OnDeath += Explode;
        RootGet<EntityCollider3D>().OnCollisionEnterDelegate += DieOnCollision;
        selected = PlayerBrain.Transform;
        bodyPart = RootGet<EntityBodyPart>();
        bodyPart.KeyEvenement(true);
        OnCanActAgain += () => {
            bodyPart.KeyEvenement(true);
        };
    }

    void Update() {
        Debug.DrawLine(transform.position, visor, Color.red);

        timerBeforeExplode += Time.deltaTime;
        if (timerBeforeExplode > timeBeforeExplode) {
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

}

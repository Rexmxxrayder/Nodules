using UnityEngine;

public class BomberBrain : EntityBrain {
    [SerializeField] private BomberData BomberData;
    [SerializeField] private float timeBeforeExplode;
    [SerializeField] private EntityBasic explosion;
    private float timerBeforeExplode = 0;
    private EntityBodyPart bodyPart;
    private EntityPhysics entityPhysics;

    protected override void ResetSetup() {
        base.ResetSetup();
        BomberData.SetupData(this);
    }
    public void SetupData(EntityBasic explosion, float timeBeforeExplode) {
        this.explosion = explosion;
        this.timeBeforeExplode = timeBeforeExplode;
    }

    protected override void LoadSetup() {
        base.LoadSetup();
        gameObject.GetRoot().OnDeath += Fall;
        RootGet<EntityCollider3D>().OnCollisionEnterDelegate += DieOnCollision;
        selected = PlayerBrain.Transform;
        bodyPart = RootGet<EntityBodyPart>();
        entityPhysics = RootGet<EntityPhysics>();
        bodyPart.KeyEvenement(true);
        OnCanActAgain += () => {
            bodyPart.KeyEvenement(true);
        };
        GetComponent<Animator>().SetTrigger("Move");
    }

    void Update() {
        Debug.DrawLine(transform.position, visor, Color.red);
        timerBeforeExplode += Time.deltaTime;
        if (timerBeforeExplode > timeBeforeExplode) {
            Die();
        }

        transform.LookAt(transform.position + entityPhysics.Direction);
    }

    void DieOnCollision(Collision c) {
        if (!c.gameObject.CompareTag("Player"))
            return;
        gameObject.GetRoot().OnDeath -= Fall;
        gameObject.GetRoot().OnDeath += Explode;
        Die();
    }

    protected void Fall() {
        gameObject.GetRoot().NewDeathWay += () => {
            GetComponent<Animator>().SetTrigger("Die");
            bodyPart.GetComponentInChildren<AFollow>().GoForward();
        };
    }

    public void Explode() {
        EntityBasic newExplosion = Instantiate(explosion, transform.position, transform.rotation); 
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

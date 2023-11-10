using UnityEngine;

public class MinionBrain : EntityBrain {
    [SerializeField] private MinionData minionData;
    [SerializeField] private float projectionStrenght, projectionDist;
    [SerializeField] private int damagesHit;
    private EntityBodyPart follow;
    protected override void ResetSetup() {
        base.ResetSetup();
        follow = GetComponentInChildren<EntityBodyPart>();
        minionData.SetupData(this);
    }

    public void SetupData(float projectionStrenght, float projectionDist, int damagesHit) {
        this.projectionDist = projectionDist;          
        this.projectionStrenght = projectionStrenght;          
        this.damagesHit = damagesHit;          
    }

    protected override void LoadSetup() {
        base.LoadSetup();
        selected = PlayerBrain.Transform;
        RootGet<EntityCollider3D>().OnCollisionEnterDelegate += Hit;
    }

    private void Update() {
        visor = selected.transform.position;
        if (follow.Available) {
            follow.KeyEvenement(true);
        }
    }

    void Hit(Collision c) {
        EntityPhysics ep = c.gameObject.RootGet<EntityPhysics>();
        EntityHealth eh = c.gameObject.RootGet<EntityHealth>();
        if (ep != null) {
            ep.Add(Force.Const(c.transform.position - transform.position, projectionStrenght, projectionDist / projectionStrenght), EntityPhysics.PhysicPriority.PROJECTION);
        }

        if (eh != null) {
            eh.RemoveHealth(damagesHit);
        }
    }
}

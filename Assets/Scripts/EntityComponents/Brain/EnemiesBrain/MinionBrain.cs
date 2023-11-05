using UnityEngine;

public class MinionBrain : EntityBrain {
    public float projectionStrenght, projectionDist;
    public int damagesHit;
    [SerializeField] private EntityBodyPart follow;

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

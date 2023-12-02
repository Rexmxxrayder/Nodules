using System.Collections;
using UnityEngine;

public class AFollow : Ability {
    [SerializeField] private float speed;
    [SerializeField] private float time;
    [SerializeField] private float minimunDistance = 0.05f;
    [SerializeField] private Force FollowForce = Force.Const(Vector3.zero, 0);
    private EntityPhysics ep;
    private Transform root;
    private Transform toFollow;

    public void SetupData(float speed, float time, float minimunDistance) {
        this.speed = speed;
        this.time = time;
        this.minimunDistance = minimunDistance;
    }

    protected override void LaunchAbilityUp(EntityBrain brain) {
        ep = gameObject.RootGet<EntityPhysics>();
        root = gameObject.GetRoot().transform;
        toFollow = brain.Selected;
        StopFollow();
        StartCoroutine(Follow(ep, root, toFollow));
    }

    IEnumerator Follow(EntityPhysics ep, Transform root, Transform goToPosition) {
        FollowForce = new(Force.Const(goToPosition.position - root.position, speed, time));
        ep.Add(FollowForce, EntityPhysics.PhysicPriority.INPUT);
        do {
            if(Vector3.Distance(goToPosition.position.PlanXZ(), root.position.PlanXZ()) <= minimunDistance) {
                FollowForce.Direction = Vector3.zero;
            } else {
                Vector3 direction = goToPosition.position - root.position;
                FollowForce.Direction = direction.PlanXZ();
            }
            yield return null;
        } while (!FollowForce.HasEnded);
        StopFollow();
    }

    public void StopFollow() {
        ep.Remove(FollowForce);
        StopAllCoroutines();
    }

    public void GoForward() {
        StopAllCoroutines();
        FollowForce = new(Force.Const(FollowForce.Direction, speed, 10f));
    }

    public override void Cancel() {
        StopFollow();
    }
}

using UnityEngine;
using Sloot;

public class AFistPunch : Ability {
    [SerializeField] private GameObject fist;
    [SerializeField] private float fistDuration = 1f;
    protected override void LaunchAbilityUp(EntityBrain brain) {
        brain.GetRootComponent<EntityPhysics>().Add(Force.Const(Vector3.zero, 1, fistDuration), EntityPhysics.PhysicPriority.BLOCK);
        Punch(brain.Visor);
    }

    public void EndAnim() {
        fist.GetComponentInChildren<Animator>().SetFloat("FistDuration", 0);
        fist.gameObject.SetActive(false);
    }

    public override void Cancel() {
        EndAnim();
    }

    public virtual void Punch(Vector3 toward) {
        fist.gameObject.SetActive(true);
        fist.transform.rotation = Quaternion.Euler(0, RotationSloot.GetDegreeBasedOfTarget(fist.transform.position, toward, RotationSloot.TranslateVector3("y")), 0);
        fist.GetComponentInChildren<Animator>().SetFloat("FistDuration", 1 / fistDuration);
        StartCooldown();
    }
}

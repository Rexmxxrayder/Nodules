using UnityEngine;
using Sloot;

public class AFistPunch : Ability {
    [SerializeField] GameObject fist;
    [SerializeField] float fistSpeed;
    protected override void LaunchAbilityUp(EntityBrain brain) {
        Vector3 cursor = brain.Visor;
        fist.gameObject.SetActive(true);
        fist.transform.rotation = Quaternion.Euler(0, RotationSloot.GetDegreeBasedOfTarget(fist.transform.position, cursor, RotationSloot.TranslateVector3("y")), 0);
        fist.GetComponentInChildren<Animator>().SetFloat("FistSpeed", fistSpeed);
        StartCooldown();
    }

    public void EndAnim() {
        fist.GetComponentInChildren<Animator>().SetFloat("FistSpeed", 0);
        fist.gameObject.SetActive(false);
    }

    public override void Cancel() {
        EndAnim();
    }
}

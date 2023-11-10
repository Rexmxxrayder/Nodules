using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFlash : Ability {
    [SerializeField] private float distance;
    [SerializeField] private float comeBackTime;
    private Vector3 lastposition;
    private Transform toComeback;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        if(InUse) {
            return;
        }

        Flash(brain.GetRootTransform(), brain.Visor);
    }

    private void Flash(Transform transform, Vector3 direction) {
        inUse = true;
        toComeback = transform;
        lastposition = transform.position;
        transform.position = transform.position + (direction - transform.position).normalized * distance;
        StartCoroutine(ComeBack());
    }

    private IEnumerator ComeBack() {
        yield return new WaitForSeconds(comeBackTime);
        toComeback.position = lastposition;
        StartCooldown();
    }
}

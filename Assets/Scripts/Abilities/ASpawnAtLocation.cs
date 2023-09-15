using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASpawnAtLocation : Ability
{
    [SerializeField] private EntityRoot prefab;
    [SerializeField] private bool onUp;
    [SerializeField] private float maxDistance;
    [SerializeField] private AnimationCurve timeBeforeSpawnOnDistance;

    private void OnValidate() {
        timeBeforeSpawnOnDistance.ChangeDuration(maxDistance);
    }

    protected override void LaunchAbilityUp(EntityBrain brain) {
        if (onUp) {
            StartCoroutine(Spawn(brain.GetRootPosition(), brain.Visor));
        }
    }

    protected override void LaunchAbilityDown(EntityBrain brain) {
        if (!onUp) {
            StartCoroutine(Spawn(brain.GetRootPosition(), brain.Visor));
        }
    }

    private IEnumerator Spawn(Vector3 LauncherPosition, Vector3 visorPosition) {
        Vector3 direction = visorPosition - LauncherPosition;
        float distance = direction.magnitude;
        Vector3 spawnPosition;
        float timeBeforeSpawn;
        if (distance > maxDistance) {
            spawnPosition = direction.normalized * maxDistance + LauncherPosition;
        } else {
            spawnPosition = visorPosition;
        }

        timeBeforeSpawn = timeBeforeSpawnOnDistance.Evaluate(distance);
        yield return new WaitForSeconds(timeBeforeSpawn);
        EntityRoot newInstance = Instantiate(prefab);
        newInstance.Spawn(spawnPosition);
    }
}

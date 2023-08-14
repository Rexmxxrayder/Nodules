using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASpawnEntity : Ability {
    [SerializeField] string toSpawn;
    [SerializeField] float distSpawn;

    protected override void LaunchAbilityUp(EntityBrain brain) {
        Vector3 BodyPosition = gameObject.GetRootPosition();
        Vector3 Visor = brain.Visor;
        SpawnEntity(BodyPosition + (Visor - BodyPosition).normalized * distSpawn);
    }

    void SpawnEntity(Vector3 SpawnPosition) {
        EntityBasic er = BasicPools.Gino.GetInstance(toSpawn);
        er.transform.position = SpawnPosition;
    }
}
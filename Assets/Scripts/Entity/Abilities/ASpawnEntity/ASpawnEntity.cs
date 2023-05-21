using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASpawnEntity : Ability {
    [SerializeField] string toSpawn;
    [SerializeField] float distSpawn;

    protected override void LaunchAbility(EntityBrain brain) {
        GetComponentInParent<EntityBodyParts>().onMovement?.Invoke();
        Vector3 BodyPosition = GetComponentInParent<EntityBodyParts>().GetRoot().transform.position;
        Vector3 Visor = brain.Visor;
        SpawnEntity(BodyPosition + (Visor - BodyPosition).normalized * distSpawn);
    }

    void SpawnEntity(Vector3 SpawnPosition) {
        EntityBasic er = BasicPools.Gino.GetInstance(toSpawn);
        er.transform.position = SpawnPosition;
    }
}

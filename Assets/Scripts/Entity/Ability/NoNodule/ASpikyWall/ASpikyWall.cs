using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASpikyWall : Ability {

    [SerializeField] SpikyWall SpikyWallprefab;
    [SerializeField] float distSpawn;

    protected override void LaunchAbility(EntityBrain brain) {
        Vector3 BodyPosition = entityBodyPart.GetRoot().transform.position;
        Vector3 Visor = brain.Visor;
        SpawnSpikyWall(BodyPosition + (Visor - BodyPosition).normalized * distSpawn);
    }

    void SpawnSpikyWall(Vector3 SpawnPosition) {
        SpikyWall newSpikyWall = Instantiate(SpikyWallprefab, SpawnPosition, Quaternion.identity);
    }
}

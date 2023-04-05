using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASpikyWall : Ability {

    [SerializeField] SpikyWall SpikyWallprefab;
    [SerializeField] float distSpawn;

    protected override void LaunchAbility((Brain, Body) bodyBrain) {
        Vector3 BodyPosition = bodyBrain.Item2.transform.position;
        Vector3 Visor = bodyBrain.Item1.Visor;
        SpawnSpikyWall(BodyPosition + (Visor - BodyPosition).normalized * distSpawn);
    }

    void SpawnSpikyWall(Vector3 SpawnPosition) {
        SpikyWall newSpikyWall = Instantiate(SpikyWallprefab, SpawnPosition, Quaternion.identity);
    }
}

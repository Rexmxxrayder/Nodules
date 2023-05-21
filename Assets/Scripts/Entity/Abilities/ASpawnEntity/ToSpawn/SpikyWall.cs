using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class SpikyWall : EntityBasic {
    public int SpikeNumber;
    public float SpikeDistSpawn;

    protected override void StartSetup() {
        gameObject.Get<EntityDeath>().OnDeath += Died;
    }

    void Died() {
        for (int i = 0; i < SpikeNumber; i++) {
            Vector3 direction = RotationSloot.GetDirectionOnAxis(360 / SpikeNumber * i, RotationSloot.TranslateVector3("z"));
            Vector3 spikePosition = transform.position + direction * SpikeDistSpawn;
            SpawnSpikes(spikePosition, Quaternion.Euler(0, 0, 360 / SpikeNumber * i), direction);
        }
    }

    void SpawnSpikes(Vector3 position, Quaternion rotation, Vector3 direction) {
        EntityBasic newSpike = BasicPools.Gino.GetInstance("Spike");
        newSpike.transform.position = position;
        newSpike.transform.rotation = rotation;
        Force force = Force.Const(direction.normalized, 10, 2);
        newSpike.gameObject.Get<EntityPhysics>().Add(force, EntityPhysics.PhysicPriority.PROJECTION);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class SpikyWall : MonoBehaviour {
    public int SpikeNumber;
    public float SpikeSpeed;
    public float SpikeTime;
    public float SpikeDistSpawn;
    bool died = false;
    private void Start() {
        GetComponentInChildren<EntityHealth>().OnDeath += Died;
    }

    void Died() {
        if(!died) { 
            died = true;
        } else {
            return;
        }
        gameObject.SetActive(false);
        Destroy(gameObject);
        for (int i = 0; i < SpikeNumber; i++) {
            Vector3 direction = RotationSloot.GetDirectionOnAxis(360 / SpikeNumber * i, RotationSloot.TranslateVector3("z"));
            Vector3 spikePosition = transform.position + direction * SpikeDistSpawn;
            SpawnSpikes(spikePosition, Quaternion.Euler(0, 0, 360 / SpikeNumber * i), direction);
        }
    }

    void SpawnSpikes(Vector3 position, Quaternion rotation, Vector3 direction) {
        Basic newSpike = BasicPrefabs.Gino.GetInstance("Spike");
        newSpike.transform.position = position;
        newSpike.transform.rotation = rotation;
        ((Spike)newSpike).lifeTime = SpikeTime;
        Force force = Force.Const(direction.normalized, SpikeSpeed, SpikeTime);
        newSpike.gameObject.Get<EntityPhysics>().Add(force, (int)EntityPhysics.PhysicPriority.DASH);
    }
}

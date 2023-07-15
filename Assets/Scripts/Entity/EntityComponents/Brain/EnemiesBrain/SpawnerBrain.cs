using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public class SpawnerBrain : EntityBrain {
    [SerializeField] private EntityBodyPart spawn;
    private void Update() {
        if (spawn.Available) {
            spawn.Activate(true);
        }
    }
}

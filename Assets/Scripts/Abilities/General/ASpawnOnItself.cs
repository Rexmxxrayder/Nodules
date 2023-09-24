using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASpawnOnItself : Ability {
    [SerializeField] private bool onUp;
    [SerializeField] private EntityRoot prefab;
    protected override void LaunchAbilityUp(EntityBrain brain) {
        if (onUp) {
            Spawn(brain.GetRootPosition());
        }
    }

    protected override void LaunchAbilityDown(EntityBrain brain) {
        if(!onUp) {
            Spawn(brain.GetRootPosition());
        }
    }

    private void Spawn(Vector3 position) {
        EntityRoot newInstance = Instantiate(prefab);
        newInstance.Spawn(position);
    }
}

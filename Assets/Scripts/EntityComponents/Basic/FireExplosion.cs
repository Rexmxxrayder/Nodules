using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosion : AreaDamage3D
{
    [SerializeField] private AreaDamage3D FireZone;

    protected override void ResetSetup() {
        OnDeath += SpawnFireZone;
    }

    private void SpawnFireZone() {
        AreaDamage3D areaDamage = Instantiate(FireZone);
        areaDamage.Spawn(GetRootPosition());
    }
}

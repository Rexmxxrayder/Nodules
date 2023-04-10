using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sloot;

public abstract class EntityEnemy : EntityRoot, IReset {
    public abstract void InstanceReset();

    public abstract void InstanceResetSetup();
}

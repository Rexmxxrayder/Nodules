using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCollider : EntityComponent {
    protected bool isActive = true;
    public virtual bool IsActive {
        get { return isActive; }
        set { isActive = value; }
    }
}

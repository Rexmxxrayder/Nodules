using Sloot;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class EntityBrain : EntityRoot {
    [SerializeField] protected Vector3 visor;

    #region Properties
    public Vector3 Visor { get { return visor; } }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class EntityBrain : EntityRoot {
    //protected List<Nerve> nerves = new List<Nerve>();
    [SerializeField] protected Vector3 visor;

    #region Properties
    public Vector3 Visor { get { return visor; } }
    #endregion

    protected override void AwakeSetup() {
        foreach (Nerve nerve in GetComponentsInChildren<Nerve>()) {
            //nerves.Add(nerve);
            nerve.brain = this;
        }
        Get<EntityHealth>().OnDeath += Death;
    }

    private void Death() {
        Time.timeScale = 0;
    }
}

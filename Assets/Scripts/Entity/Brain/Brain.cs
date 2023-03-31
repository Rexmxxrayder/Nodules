using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class Brain : MonoBehaviour {
    [SerializeField] protected List<Nerve> nerves = new List<Nerve>();
    [SerializeField] protected List<Body> bodies = new List<Body>();
    [SerializeField] protected Vector2 visor;

    #region Properties
    public Vector2 Visor { get { return visor; } }
    #endregion

    private void Awake() {
        for (int i = 0; i < nerves.Count; i++) {
            nerves[i].brain = this;
        }
    }
}

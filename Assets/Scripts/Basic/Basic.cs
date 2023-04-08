using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Basic : MonoBehaviour {
    [SerializeField] string type;
    public string Type => type;
    private void Awake() {
        AwakeSetup();
    }

    protected virtual void AwakeSetup() {

    }
    public abstract void Activate();
}

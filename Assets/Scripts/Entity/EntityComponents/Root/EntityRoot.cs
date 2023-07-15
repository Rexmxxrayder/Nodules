using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EntityRoot : EntityComponent {

    private string type;
    bool died;
    readonly UnityEvent _onDeath = new();
    public event UnityAction OnDeath { add => _onDeath.AddListener(value); remove => _onDeath.RemoveListener(value); }

    delegate void DeathWay();
    DeathWay newDeathWay;
    public event UnityAction NewDeathWay { add => newDeathWay = new(value); remove => newDeathWay = null; }
    public string Type => type;
    public override EntityRoot SetRoot() {
        _root = this;
        return _root;
    }

    private void OnValidate() {
        type = gameObject.name;
    }

    public void GiveType(string typeGiven) { 
        type ??= typeGiven;
    }

    protected override void DefinitveSetup() {
        newDeathWay = null;
        _onDeath.RemoveAllListeners();
    }

    protected override void ResetSetup() {
        died = false;
    }

    public override void Die() {
        if (died) {
            return;
        } else {
            died = true;
        }
        _onDeath?.Invoke();
        if (newDeathWay != null) {
            newDeathWay();
        } else {
            Debug.Log("DESTROY " + _root.name);
            GetRootGameObject().SetActive(false);
            Destroy(gameObject);
        }
    }
}

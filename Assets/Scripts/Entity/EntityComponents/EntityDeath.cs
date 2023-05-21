using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityDeath : EntityComponent
{
    bool died;
    readonly UnityEvent _onDeath = new();
    public event UnityAction OnDeath { add => _onDeath.AddListener(value); remove => _onDeath.RemoveListener(value); }

    delegate void DeathWay();
    DeathWay newDeathWay;
    public event UnityAction NewDeathWay { add => newDeathWay = new(value); remove => newDeathWay = null; }

    protected override void AwakeSetup() {
        died = false;
        newDeathWay = null;
        _onDeath.RemoveAllListeners();
    }

    protected override void StartSetup() {
        if(Get<EntityHealth>() != null)
        Get<EntityHealth>().OnZeroHealth += Die;
    }

    public void Die() {
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
            _root.SetActive(false);
            Destroy(_root);
        }
    }

}

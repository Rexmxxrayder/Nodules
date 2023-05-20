using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityDeath : EntityComponent, IReset
{
    bool died;
    UnityEvent _onDeath = new();
    public event UnityAction OnDeath { add => _onDeath.AddListener(value); remove => _onDeath.RemoveListener(value); }

    delegate void DeathWay();
    DeathWay newDeathWay;
    public event UnityAction NewDeathWay { add => newDeathWay = new(value); remove => newDeathWay = null; }

    protected override void StartSetup() {
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

    public override void InstanceReset() {
        died = false;
        newDeathWay = null;
        _onDeath.RemoveAllListeners();
    }
}

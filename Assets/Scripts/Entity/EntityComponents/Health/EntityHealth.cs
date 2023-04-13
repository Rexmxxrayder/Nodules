using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : EntityComponent, IReset {
    [SerializeField] int _health;
    [SerializeField] int _maxHealth;
    public int Health => _health;
    public int MaxHealth => _maxHealth;

    UnityEvent<int> _onDamaged = new();
    public event UnityAction<int> OnDamaged { add => _onDamaged.AddListener(value); remove => _onDamaged.RemoveListener(value); }

    UnityEvent<int> _onHealed = new();
    public event UnityAction<int> OnHealed { add => _onHealed.AddListener(value); remove => _onHealed.RemoveListener(value); }

    bool died = false;
    UnityEvent _onDeath = new();
    public event UnityAction OnDeath { add => _onDeath.AddListener(value); remove => _onDeath.RemoveListener(value); }

    delegate void DeathWay();
    DeathWay newDeathWay;
    public event UnityAction NewDeathWay { add => newDeathWay = new(value); remove => newDeathWay = null; }

    protected override void AwakeSetup() {
        NewMaxHealth(_maxHealth);
        _health = Mathf.Max(0, _health);
    }

    void Die() {
        if (died) {
            return;
        } else {
            died = true;
        }
        //Debug.Log("Dead" + _root.name);
        _onDeath?.Invoke();
        if (newDeathWay != null) {
            newDeathWay();
        } else {
            Debug.Log("DESTROY " + _root.name);
            _root.SetActive(false);
            Destroy(_root);
        }
    }

    public void LethalDamage() {
        RemoveHealth(_health);
    }

    public int AddHealth(int add) {
        add = Mathf.Max(0, add);
        if (_health + add > _maxHealth) {
            _health = _maxHealth;
        } else {
            _health = _health + add;
        }
        if (add != 0) {
            _onHealed?.Invoke(add);
        }
        return _health;
    }

    public int RemoveHealth(int remove) {
        remove = Mathf.Max(0, remove);
        _health = _health - remove < 0 ? 0 : _health - remove;
        if (remove != 0) {
            _onDamaged?.Invoke(remove);
        }
        if (_health == 0) {
            Die();
        }
        return _health;
    }

    public int NewMaxHealth(int newMaxHealth) {
        if (newMaxHealth < 1) {
            _maxHealth = 1;
        }
        if (_health > MaxHealth) {
            _health = newMaxHealth;
        }
        return _health;
    }

    public void InstanceReset() {
        died = false;
        newDeathWay = null;
        _onDeath.RemoveAllListeners();
        _health = _maxHealth;
    }

    public void InstanceResetSetup() {
    }
}

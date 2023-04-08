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

    bool died = false;
    UnityEvent _onDeath = new();
    public event UnityAction OnDeath { add => _onDeath.AddListener(value); remove => _onDeath.RemoveListener(value); }

    delegate void DeathWay();
    DeathWay newDeathWay;
    public event UnityAction NewDeathWay { add => newDeathWay = new(value); remove => newDeathWay = null; }

    protected override void ChildSetup() {
        NewMaxHealth(_maxHealth);
        _health = Mathf.Max(0, _health);
    }

    void Die() {
        if(died) {
            return;
        } else {
            died = true;
        }
        _onDeath?.Invoke();
        if (newDeathWay != null) {
            newDeathWay();
        } else {
            _root.SetActive(false);
            Destroy(_root);
        }
    }

    public void LethalDamage() {
        RemoveHealth(_health);
    }

    public int AddHealth(int add) {
        _health = _health + add > _maxHealth ? _maxHealth : _health + add;
        return _health;
    }

    public int RemoveHealth(int remove) {
        _health = _health - remove < 0 ? 0 : _health - remove;
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
}

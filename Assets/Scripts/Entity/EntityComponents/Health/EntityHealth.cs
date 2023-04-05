using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : EntityComponent {
    [SerializeField] int _health;
    [SerializeField] int _maxHealth;
    public int Health => _health;
    public int MaxHealth => _maxHealth;

    public bool DestroyOnDeath;

    UnityEvent _onDeath = new();
    public event UnityAction OnDeath { add => _onDeath.AddListener(value); remove => _onDeath.RemoveListener(value); }

    protected override void ChildSetup() {
        NewMaxHealth(_maxHealth);
        _health = Mathf.Max(0, _health);
        if (DestroyOnDeath) {
            OnDeath += () => { _root.SetActive(false); Destroy(_root); };
        }
    }

    public int AddHealth(int add) {
        _health = _health + add > _maxHealth ? _maxHealth : _health + add;
        return _health;
    }

    public int RemoveHealth(int remove) {
        _health = _health - remove < 0 ? 0 : _health - remove;
        if (_health == 0) {
            _onDeath?.Invoke();
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
}

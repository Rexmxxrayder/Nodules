using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : EntityRangeInt {

    public int Health => _currentValue;
    public int MaxHealth => _maxValue;

    public event UnityAction<int> OnDamaged { add => _onDecreasing.AddListener(value); remove => _onDecreasing.RemoveListener(value); }
    public event UnityAction<int> OnHealed { add => _onIncreasing.AddListener(value); remove => _onIncreasing.RemoveListener(value); }
    public event UnityAction<int> OnOverDamaged { add => _onOverDecreased.AddListener(value); remove => _onOverDecreased.RemoveListener(value); }
    public event UnityAction<int> OnOverHealed { add => _onOverIncreased.AddListener(value); remove => _onOverIncreased.RemoveListener(value); }

    readonly UnityEvent _onZeroHealth = new();
    public event UnityAction OnZeroHealth { add => _onZeroHealth.AddListener(value); remove => _onZeroHealth.RemoveListener(value); }

    protected override void AwakeSetup() {
        if(Get<EntityDeath>() == null) {
            gameObject.AddComponent<EntityDeath>();
        }
        NewMinValue(_maxValue);
        NewMinValue(0);
        _currentValue = _maxValue;
        RemoveAllListeners();
    }

    public int AddHealth(int add) {
        return IncreaseOf(add);
    }

    public int RemoveHealth(int remove) {
        DecreaseOf(remove);
        if (_currentValue == 0) { _onZeroHealth?.Invoke(); }
        return _currentValue;
    }

    public void LethalDamage() {
        RemoveHealth(_currentValue);
    }

    public void NewMaxHealth(int newMaxHealth) {
        newMaxHealth = Mathf.Max(0, newMaxHealth);
        NewMaxValue(newMaxHealth);
    }

    public int GetPercentHealth(int percent) {
        return GetPercentRange(percent);
    }

    protected override void RemoveAllListeners() {
        base.RemoveAllListeners();
        _onZeroHealth.RemoveAllListeners();
    }
}

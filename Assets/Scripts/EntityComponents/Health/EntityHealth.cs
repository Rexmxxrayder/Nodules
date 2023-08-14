using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : EntityComponent{

    private readonly RangeInt health = new();
    [SerializeField] private int startMaxHealth;
    [SerializeField] private int startHealth;
    public int Health => health.CurrentValue;
    public int MaxHealth => health.MaxValue;

    public event UnityAction<int> OnDamaged { add => health.OnDecreasing += value; remove => health.OnDecreasing -= value; }
    public event UnityAction<int> OnHealed { add => health.OnIncreasing += value; remove => health.OnIncreasing -= value; }
    public event UnityAction<int> OnOverDamaged { add => health.OnOverDecreased += value; remove => health.OnOverDecreased -= value; }
    public event UnityAction<int> OnOverHealed { add => health.OnOverIncreased += value; remove => health.OnOverIncreased -= value; }

    readonly UnityEvent _onZeroHealth = new();
    public event UnityAction OnZeroHealth { add => _onZeroHealth.AddListener(value); remove => _onZeroHealth.RemoveListener(value); }

    protected override void LoadSetup() {
        OnZeroHealth += Die;
    }

    protected override void ResetSetup() {
        RemoveAllListeners();
        health.NewMinValue(0);
        NewMaxHealth(startMaxHealth);
        health.EqualTo(startHealth);
    }

    public int AddHealth(int add) {
        return health.IncreaseOf(add);
    }

    public int RemoveHealth(int remove) {
        health.DecreaseOf(remove);
        if (Health == 0) { _onZeroHealth?.Invoke(); }
        return Health;
    }

    public void LethalDamage() {
        RemoveHealth(Health);
    }

    public void NewMaxHealth(int newMaxHealth) {
        newMaxHealth = Mathf.Max(0, newMaxHealth);
        health.NewMaxValue(newMaxHealth);
    }

    public int GetPercentHealth(int percent) {
        return health.GetPercentRange(percent);
    }

    public void RemoveAllListeners() {
        health.RemoveAllListeners();
        _onZeroHealth.RemoveAllListeners();
    }
}

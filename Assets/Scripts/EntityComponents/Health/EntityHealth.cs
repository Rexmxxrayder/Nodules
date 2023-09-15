using Sloot;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : EntityComponent{

    private RangeInt health = new();
    [SerializeField] private int startMaxHealth;
    [SerializeField] private int startHealth;
    public int Health => health.CurrentValue;
    public int MaxHealth => health.MaxValue;

    private Func<int, int> damageModifier;
    private Func<int, int> healModifier;
    public event Func<int, int> DamageModifier { add => damageModifier = value; remove => damageModifier = null; }
    public event Func<int, int> HealModifier { add => healModifier = value; remove => healModifier = null; }

    public event Action<int> OnDamaged { add => health.OnDecreasing += value; remove => health.OnDecreasing -= value; }
    public event Action<int> OnHealed { add => health.OnIncreasing += value; remove => health.OnIncreasing -= value; }
    public event Action<int> OnOverDamaged { add => health.OnOverDecreased += value; remove => health.OnOverDecreased -= value; }
    public event Action<int> OnOverHealed { add => health.OnOverIncreased += value; remove => health.OnOverIncreased -= value; }

    private Action _onZeroHealth;
    public event Action OnZeroHealth { add => _onZeroHealth += value; remove => _onZeroHealth -= value; }

    protected override void LoadSetup() {
        OnZeroHealth += Die;
    }

    protected override void ResetSetup() {
        RemoveAllListeners();
        health.NewMinValue(0);
        NewMaxHealth(startMaxHealth);
        health.EqualTo(startHealth);
    }

    public int AddHealth(int heal) {
        if (healModifier != null) {
            heal = healModifier(heal);
        }

        if (heal <= 0) {
            return Health;
        }

        return health.IncreaseOf(heal);
    }

    public int RemoveHealth(int damage) {
        if (damageModifier != null) {
            damage = damageModifier(damage);
        }

        if(damage <= 0) { 
            return Health;
        }

        health.DecreaseOf(damage);
        if (Health == 0) { _onZeroHealth?.Invoke(); }
        return Health;
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
        _onZeroHealth = null;
    }
}

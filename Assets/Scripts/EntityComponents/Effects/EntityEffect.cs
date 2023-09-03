using UnityEngine;

[System.Serializable]
public abstract class EntityEffect {
    public enum EffectType {
        BURN,
        FIRE
    }

    public abstract EffectType Type {
        get;
    }

    protected EntityEffectManager entityEffectManager;
    protected float duration = 0;
    protected float elapsedTime = 0;
    protected bool end = false;
    protected bool cancel = false;
    protected bool negate = false;
    [SerializeField] protected int stack = 1;
    public abstract int MaxStack {
        get;
    }
    public int Stack => stack;
    public float Duration { get => duration; set => duration = value; }
    public float ElapsedTime => elapsedTime;
    public float TimeRemaining => duration - elapsedTime;
    public bool IsEnd => end || cancel;
    public bool Negate { get => negate; set => negate = value; }

    public virtual void SetupEffect(EntityEffectManager effectManager) {
        entityEffectManager = effectManager;
        entityEffectManager.OnEffectAdd += EffectAdd;
        entityEffectManager.OnEffectRemove += EffectRemove;
        entityEffectManager.OnEffectWantAdd += EffectTryingAdd;
        entityEffectManager.OnEffectWantRemove += EffectTryingRemove;
        return;
    }

    public virtual void UpdateEffect(float deltaTime) {
        elapsedTime += deltaTime;
        if (elapsedTime >= duration) {
            EndEffect();
        }
    }

    public virtual void EndEffect() {
        end = true;
        entityEffectManager.OnEffectAdd -= EffectAdd;
        entityEffectManager.OnEffectRemove -= EffectRemove;
        entityEffectManager.OnEffectWantAdd -= EffectTryingAdd;
        entityEffectManager.OnEffectWantRemove -= EffectTryingRemove;
    }

    public virtual void CancelEffect() {
        cancel = true;
        EndEffect();
    }

    protected virtual void EffectAdd(EntityEffect newEffect) {
        if (newEffect == this) {
            return;
        }
    }

    protected virtual void EffectTryingAdd(EntityEffect newEffect) {
        if (newEffect == this) {
            return;
        }
    }

    protected virtual void EffectRemove(EntityEffect effect) {
        if (effect == this) {
            return;
        }
    }

    protected virtual void EffectTryingRemove(EntityEffect newEffect) {
        if (newEffect == this) {
            return;
        }
    }
}

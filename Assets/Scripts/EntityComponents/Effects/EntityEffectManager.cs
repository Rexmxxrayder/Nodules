using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EntityEffectManager : EntityComponent {
    [SerializeField] private List<string> effectsVisualise = new();
    private List<EntityEffect> effects = new();
    private Action<EntityEffect> onEffectAdd;
    private Action<EntityEffect> onEffectWantAdd;
    private Action<EntityEffect> onEffectRemove ;
    private Action<EntityEffect> onEffectWantRemove;
    public event Action<EntityEffect> OnEffectAdd { add { onEffectAdd += value; } remove { onEffectAdd -= value; } }
    public event Action<EntityEffect> OnEffectWantAdd { add { onEffectWantAdd += value; } remove { onEffectWantAdd -= value; } }
    public event Action<EntityEffect> OnEffectRemove { add { onEffectRemove += value; } remove { onEffectRemove -= value; } }
    public event Action<EntityEffect> OnEffectWantRemove { add { onEffectWantRemove += value; } remove { onEffectWantRemove -= value; } }
    public List<EntityEffect> Effects => effects;

    public void AddEffect(EntityEffect effect) {
        onEffectWantAdd?.Invoke(effect);
        if(effect.Negate) {
            return;
        }

        effects.Add(effect);
        effect.SetupEffect(this);
        onEffectAdd?.Invoke(effect);
    }

    public void FixedUpdate() {
        for (int i = 0; i < effects.Count; i++) {
            effects[i].UpdateEffect(Time.fixedDeltaTime);
            if(effects[i].IsEnd) {
                RemoveEffect(effects[i]);
                i--;
            }
        }

        effectsVisualise.Clear();
        foreach (var effect in effects) {
            effectsVisualise.Add($"{effect.Type} {effect.Stack}");
        }
        Debug.Log(effectsVisualise.Count);
    }

    public void RemoveEffect(EntityEffect effect) {
        effect.Negate = true;
        onEffectWantRemove?.Invoke(effect);
        if (!effect.Negate) {
            return;
        }

        effects.Remove(effect);
        onEffectRemove?.Invoke(effect);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EntityEffectManager : EntityComponent {
    [SerializeField] private List<string> effectsVisualise = new();
    private List<EntityEffect> effects = new();
    private readonly UnityEvent<EntityEffect> onEffectAdd = new();
    private readonly UnityEvent<EntityEffect> onEffectWantAdd = new();
    private readonly UnityEvent<EntityEffect> onEffectRemove = new();
    private readonly UnityEvent<EntityEffect> onEffectWantRemove = new();
    public event UnityAction<EntityEffect> OnEffectAdd { add { onEffectAdd.AddListener(value); } remove { onEffectAdd.RemoveListener(value); } }
    public event UnityAction<EntityEffect> OnEffectWantAdd { add { onEffectWantAdd.AddListener(value); } remove { onEffectWantAdd.RemoveListener(value); } }
    public event UnityAction<EntityEffect> OnEffectRemove { add { onEffectRemove.AddListener(value); } remove { onEffectRemove.RemoveListener(value); } }
    public event UnityAction<EntityEffect> OnEffectWantRemove { add { onEffectWantRemove.AddListener(value); } remove { onEffectWantRemove.RemoveListener(value); } }
    public List<EntityEffect> Effects => effects;

    public void AddEffect(EntityEffect effect) {
        onEffectWantAdd.Invoke(effect);
        if(effect.Negate) {
            return;
        }

        effects.Add(effect);
        effect.SetupEffect(this);
        onEffectAdd.Invoke(effect);
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
    }

    public void RemoveEffect(EntityEffect effect) {
        effect.Negate = true;
        onEffectWantRemove.Invoke(effect);
        if (!effect.Negate) {
            return;
        }

        effects.Remove(effect);
        onEffectRemove.Invoke(effect);
    }
}

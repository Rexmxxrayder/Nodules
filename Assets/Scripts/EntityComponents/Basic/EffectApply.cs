using System.Collections.Generic;
using UnityEngine;
using static EntityEffect;

public class EffectApply : MonoBehaviour {
    [SerializeField] EffectType effectType;
    [SerializeField] private int stack;
    [SerializeField] private bool onlyOneHit;
    private Dictionary<EntityEffectManager, int> inside = new();
    private void OnTriggerEnter(Collider other) {
        EntityEffectManager eem = other.gameObject.GetRootComponent<EntityEffectManager>();
        if (eem != null) {
            if (!inside.ContainsKey(eem)) {
                inside.Add(eem, 0);
            }

            if (inside[eem] == 0) {
                AddEffect(eem);
            }

            inside[eem]++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!onlyOneHit) {
            EntityEffectManager eem = other.gameObject.GetRootComponent<EntityEffectManager>();
            if (eem != null) {
                if (inside.ContainsKey(eem)) {
                    inside[eem]--;
                }
            }
        }
    }

    private void OnDisable() {
        inside.Clear();
    }

    private void AddEffect(EntityEffectManager eem) {
        EntityEffect effect;
        switch (effectType) {
            case EffectType.CLEANSE:
                effect = new CleanseEffect();
                break;
            case EffectType.ICE:
                effect = new IceEffect();
                break;
            case EffectType.FREEZE:
                effect = new FreezeEffect();
                break;
            case EffectType.FOCUS:
                effect = new FocusEffect();
                break;
            case EffectType.INSANITY:
                effect = new InsanityEffect();
                break;
            case EffectType.MADNESS:
                effect = new MadnessEffect();
                break;
            case EffectType.POWDER:
                effect = new PowderEffect();
                break;
            default:
            case EffectType.EMERALD:
                return;
        }

        effect.AddStack(stack - 1);
        eem.AddEffect(effect);
    }
}

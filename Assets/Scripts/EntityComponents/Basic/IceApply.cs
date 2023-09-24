using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceApply : MonoBehaviour
{
    [SerializeField] int stack;
    private Dictionary<EntityEffectManager, int> inside = new();
    private void OnTriggerEnter(Collider other) {
        EntityEffectManager eem = other.gameObject.RootGet<EntityEffectManager>();
        if (eem != null) {
            if (!inside.ContainsKey(eem)) {
                inside.Add(eem, 0);
            }

            if (inside[eem] == 0) {
                IceEffect iceEffect = new();
                iceEffect.AddStack(stack - 1);
                eem.AddEffect(iceEffect);
            }

            inside[eem]++;
        }
    }

    private void OnTriggerExit(Collider other) {
        EntityEffectManager eem = other.gameObject.RootGet<EntityEffectManager>();
        if (eem != null) {
            if (inside.ContainsKey(eem)) {
                inside[eem]--;
            }
        }
    }
}

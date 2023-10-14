using System.Collections.Generic;
using UnityEngine;
using static EntityEffect;

public class PowderCollider : MonoBehaviour {
    private bool first = true;
    private float firstDuration = 0.2f;
    private float firstTimer = 0f;
    private List<Collider> colliders = new();
    private void Update() {
        if (first) {
            firstTimer += Time.deltaTime;
            if (firstTimer > firstDuration) {
                first = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (first) {
            if (!colliders.Contains(other)) {
                colliders.Add(other);
            }
        } else {
            Debug.Log(other.name + " / " + other.CompareTag("Enemy") + " / " + (other.gameObject.GetRoot() != null)+  " / " + (!colliders.Contains(other)));
            if (other.CompareTag("Enemy") && other.gameObject.GetRoot() != null && !colliders.Contains(other))  {
                EntityEffectManager eem = other.gameObject.RootGet<EntityEffectManager>();
                if (eem != null && !eem.Contains(EffectType.POWDER)) {
                    PowderEffect powderEffect = new();
                    powderEffect.CurrentDuration = ((PowderEffect)gameObject.RootGet<EntityEffectManager>().Get(EffectType.POWDER)).TimeRemaining;
                    eem.AddEffect(powderEffect);
                    gameObject.RootGet<EntityEffectManager>().RemoveEffect(EffectType.POWDER);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (colliders.Contains(other)) {
            colliders.Remove(other);
        }
    }
}

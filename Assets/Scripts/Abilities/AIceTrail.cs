using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIceTrail : Ability {
    [SerializeField] private EntityRoot prefab;
    [SerializeField] private float duration;
    [SerializeField] private float trailSize;
    [SerializeField] private float spawnTick;
    private Coroutine coroutine;
    protected override void LaunchAbilityDown(EntityBrain brain) {
        coroutine ??= StartCoroutine(IceTrail());
    }

    private void Spawn(Vector3 position) {
        EntityRoot newInstance = Instantiate(prefab);
        newInstance.transform.localScale = Vector3.one * trailSize;
        newInstance.Spawn(position);
    }

    private IEnumerator IceTrail() {
       // gameObject.RootGet<EntityEffectManager>().AddEffect(new CleanseEffect());
        float timer = 0f;
        float tick = 0f;
        while (timer < duration) {
            timer += Time.deltaTime;
            tick += Time.deltaTime;
            if(tick >= spawnTick) {
                tick -= spawnTick;
                Spawn(gameObject.GetRootPosition());
            }

            yield return null;
        }

        StartCooldown();
        coroutine = null;
    }
}

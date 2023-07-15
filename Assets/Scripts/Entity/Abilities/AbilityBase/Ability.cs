using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class Ability : MonoBehaviour {

    public float Cooldown = 1f;
    [HideInInspector] public float TimeRemainingCooldown = 0f;
    public bool IsAvailable => TimeRemainingCooldown == 0f;
    UnityEvent _onAvailable = new();
    public event UnityAction OnAvailable { add => _onAvailable.AddListener(value); remove => _onAvailable.RemoveListener(value); }
    protected List<Ability> abilities = new ();
    public virtual void Activate(EntityBrain brain, bool isUp) {
        if (!IsAvailable) {
            return;
        }

        if(isUp) {
            LaunchAbilityUp(brain);
        } else {
            LaunchAbilityDown(brain);
        }

        foreach (Ability ability in abilities) {
            ability.Activate(brain, isUp);
        }

        if (Cooldown != 0f) {
            StartCoroutine(CooldownManager());
        }
    }

    protected void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            Ability childAbility = transform.GetChild(i).GetComponent<Ability>();
            if (childAbility != null) {
                abilities.Add(childAbility);
            }
        }
    }


    private IEnumerator CooldownManager() {
        TimeRemainingCooldown = Cooldown;
        while (TimeRemainingCooldown > 0f) {
            yield return null;
            TimeRemainingCooldown -= Time.deltaTime;
        }

        TimeRemainingCooldown = 0f;
        _onAvailable?.Invoke();
    }
    protected virtual void LaunchAbilityUp(EntityBrain brain) { }
    protected virtual void LaunchAbilityDown(EntityBrain brain) { }
}

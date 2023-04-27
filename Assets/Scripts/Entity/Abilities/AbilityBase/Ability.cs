using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class Ability : MonoBehaviour {
    protected BodyPart BodyPart;
    public static bool isDebug = false;
    public float Cooldown = 1f;
    [HideInInspector] public float TimeRemainingCooldown = 0f;
    public bool IsAvailable => TimeRemainingCooldown == 0f;
    UnityEvent _onAvailable = new();
    public event UnityAction OnAvailable { add => _onAvailable.AddListener(value); remove => _onAvailable.RemoveListener(value); }
    //   [SerializeField] string abilityName;
    protected List<Ability> abilities = new List<Ability>();
    public virtual void Activate(EntityBrain brain) {
        //if (isDebug) {
        //    ActivateDebug();
        //}
        if (!IsAvailable) {
            return;
        }
        LaunchAbility(brain);
        foreach (Ability ability in abilities) {
            ability.Activate(brain);
        }
        if (Cooldown != 0f) {
            StartCoroutine(CooldownManager());
        }
    }

    protected void Awake() {
        BodyPart = GetComponentInParent<BodyPart>();
        for (int i = 0; i < transform.childCount; i++) {
            Ability childAbility = transform.GetChild(i).GetComponent<Ability>();
            if (childAbility != null) {
                abilities.Add(childAbility);
            }
        }
    }
    protected abstract void LaunchAbility(EntityBrain brain);

    private IEnumerator CooldownManager() {
        TimeRemainingCooldown = Cooldown;
        while (TimeRemainingCooldown > 0f) {
            yield return null;
            TimeRemainingCooldown -= Time.deltaTime;
        }
        TimeRemainingCooldown = 0f;
        _onAvailable?.Invoke();
    }

    //protected virtual void ActivateDebug() {
    //    Debug.Log("Launch : " + abilityName);
    //}
}

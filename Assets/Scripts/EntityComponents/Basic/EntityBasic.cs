using System.Collections;
using UnityEngine;

public class EntityBasic : EntityRoot {
    [SerializeField] private float _duration = 1;
    public float Duration { get { return _duration; } set { _duration = value; } }
    protected override void LoadSetup() {
        StartCoroutine(LifeCooldown());
    }

    IEnumerator LifeCooldown() {
        yield return new WaitForSeconds(Duration);
        Die();
    }
}
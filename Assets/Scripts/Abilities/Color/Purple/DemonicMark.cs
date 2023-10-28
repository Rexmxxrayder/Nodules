using System;
using UnityEngine;
using Sloot;
using System.Collections.Generic;

public class DemonicMark : EntityBasic
{
    [SerializeField] private GameObject punch;
    [SerializeField] private int currentMark = 0;
    Animator animator;
    protected override void ResetSetup() {
        base.ResetSetup();
        ADemonicPunch.AttackToward += Punch;
        animator = punch.GetComponentInChildren<Animator>(true);
    }

    protected override void DestroySetup() {
        base.DestroySetup();
        ADemonicPunch.AttackToward -= Punch;
    }

    public void Punch(Vector3 toward, float duration) {
        if (currentMark == ADemonicPunch.MaxMark) {
            Die();
            return;
        }

        currentMark++;
        punch.gameObject.SetActive(true);
        punch.transform.rotation = Quaternion.Euler(0, RotationSloot.GetDegreeBasedOfTarget(punch.transform.position, toward, RotationSloot.TranslateVector3("y")), 0);
        animator.SetFloat("FistDuration", 1 / duration);
    }

    public void EndAnim() {
        animator.SetFloat("FistDuration", 0);
        punch.SetActive(false);
    }
}

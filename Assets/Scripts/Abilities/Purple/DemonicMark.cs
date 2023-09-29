using System;
using UnityEngine;
using Sloot;
using System.Collections.Generic;

public class DemonicMark : EntityBasic
{
    public static Action<Vector3> AtkToward;
    public static int MaxMark;
    [SerializeField] private GameObject punch;
    [SerializeField] private float fistSpeed;
    [SerializeField] private int currentMark = 0;
    protected override void ResetSetup() {
        base.ResetSetup();
        AtkToward += Punch;
    }

    protected override void DestroySetup() {
        base.DestroySetup();
        AtkToward -= Punch;
    }

    public void Punch(Vector3 toward) {
        if (currentMark == MaxMark) {
            Die();
        }
        currentMark++;

        punch.gameObject.SetActive(true);
        punch.transform.rotation = Quaternion.Euler(0, RotationSloot.GetDegreeBasedOfTarget(punch.transform.position, toward, RotationSloot.TranslateVector3("y")), 0);
        punch.GetComponentInChildren<Animator>().SetFloat("FistSpeed", fistSpeed);
    }

    public void EndAnim() {
        punch.GetComponentInChildren<Animator>().SetFloat("FistSpeed", 0);
        punch.gameObject.SetActive(false);

    }
}

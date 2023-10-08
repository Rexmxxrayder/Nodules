using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticValuesEffect", menuName = "Effect")]
public class StaticValuesEffectSO : ScriptableObject
{
    [SerializeField] private AnimationCurve madnessCurve;

    public void StartGame() {
        MadnessEffect.damagesCurve = madnessCurve;
    }
}

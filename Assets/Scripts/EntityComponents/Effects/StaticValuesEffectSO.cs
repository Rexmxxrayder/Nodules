using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticValuesEffect", menuName = "Static/ValuesEffect")]
public class StaticValuesEffectSO : ScriptableObject
{
    [SerializeField] private AnimationCurve madnessCurve;

    public void StartGame() {
        MadnessEffect.damagesCurve = madnessCurve;
    }
}

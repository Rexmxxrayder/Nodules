using UnityEngine;

[CreateAssetMenu(fileName = "StaticEffectsValues", menuName = "ScriptableObjects/StaticEffectsValues")]
public class StaticEffectsValuesSO : ScriptableObject
{
    [SerializeField] private AnimationCurve madnessCurve;

    public void StartGame() {
        MadnessEffect.damagesCurve = madnessCurve;
    }
}

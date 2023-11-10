using UnityEngine;

[CreateAssetMenu(fileName = "MinionData", menuName = "ScriptableObjects/EnnemiesData/MinionData")]
public class MinionData : ScriptableObject {

    [Header("AFollow")]
    [SerializeField] private int damagesHit;
    [SerializeField] private float projectionStrenght, projectionDist, cooldown, speed, time, minimunDistance;
    public void SetupData(MinionBrain entityBrain) {
        entityBrain.SetupData(projectionStrenght, projectionDist, damagesHit);
        entityBrain.GetComponentInChildren<AFollow>().SetupData(speed, time, minimunDistance);
    }
}
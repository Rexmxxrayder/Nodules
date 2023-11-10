using UnityEngine;

[CreateAssetMenu(fileName = "BomberData", menuName = "ScriptableObjects/EnnemiesData/BomberData")]
public class BomberData : ScriptableObject {
    [SerializeField] private float timeBeforeExplode, speed, time, minimunDistance;
    public void SetupData(BomberBrain entityBrain) {
        entityBrain.SetupData(timeBeforeExplode);
        entityBrain.GetComponentInChildren<AFollow>().SetupData(speed, time, minimunDistance);
    }
}

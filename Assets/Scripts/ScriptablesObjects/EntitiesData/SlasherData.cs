using UnityEngine;

[CreateAssetMenu(fileName = "SlasherData", menuName = "ScriptableObjects/EnnemiesData/SlasherData")]
public class SlasherData : ScriptableObject {

    [Header("ASlash")]
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private float cooldown, speed, dist, slashSpeed, distMinTotarget;

    public void SetupData(SlasherBrain entityBrain) {
        entityBrain.GetComponentInChildren<ASlash>().SetupData(cooldown, slashPrefab, speed, dist, slashSpeed, distMinTotarget);
    }
}

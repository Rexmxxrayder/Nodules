using UnityEngine;

[CreateAssetMenu(fileName = "JumperData", menuName = "ScriptableObjects/EnnemiesData/JumperData")]
public class JumperData : ScriptableObject {

    [Header("AExplosiveJump")]
    [SerializeField] private AreaDamage3D areaDamage;
    [SerializeField] private float distMaxJump, cooldown, speedJump;
    public void SetupData(JumperBrain entityBrain) {
        entityBrain.GetComponentInChildren<AExplosiveJump>().SetupData(cooldown, distMaxJump, speedJump, areaDamage);
    }
}

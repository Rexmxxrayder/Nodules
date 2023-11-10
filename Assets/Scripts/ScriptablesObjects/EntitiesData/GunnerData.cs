using UnityEngine;

[CreateAssetMenu(fileName = "GunnerData", menuName = "ScriptableObjects/EnnemiesData/GunnerData")]
public class GunnerData : ScriptableObject {

    [Header("AShootBullet")]
    [SerializeField] private Bullet bulletprefab;
    [SerializeField] private float cooldown, speed, maxDistance, bulletNumber, timeForShoot;
    [SerializeField] private bool freeChild;

    public void SetupData(GunnerBrain entityBrain) {
        entityBrain.GetComponentInChildren<AShootBullet>().SetupData(bulletprefab, cooldown, speed, maxDistance, bulletNumber, timeForShoot, freeChild);
    }
}

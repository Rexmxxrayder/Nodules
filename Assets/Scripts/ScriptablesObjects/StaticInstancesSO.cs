using UnityEngine;

[CreateAssetMenu(fileName = "StaticInstances", menuName = "ScriptableObjects/StaticInstances")]
public class StaticInstancesSO : ScriptableObject
{
    [SerializeField] private PowderCollider powderColliderPrefab;

    public void StartGame() {
        PowderEffect.powderColliderPrefab = powderColliderPrefab;
    }
}

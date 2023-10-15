using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticInstance", menuName = "Static/Instance")]
public class StaticInstanceSO : ScriptableObject
{
    [SerializeField] private PowderCollider powderColliderPrefab;

    public void StartGame() {
        PowderEffect.powderColliderPrefab = powderColliderPrefab;
    }
}

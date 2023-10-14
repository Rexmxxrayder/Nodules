using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private StaticValuesEffectSO staticValuesEffectSO;
    [SerializeField] private StaticInstanceSO staticInstanceSO;

    private void Awake() {
        staticValuesEffectSO.StartGame();
        staticInstanceSO.StartGame();
    }
}

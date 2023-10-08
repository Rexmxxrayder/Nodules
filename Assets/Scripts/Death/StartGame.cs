using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private StaticValuesEffectSO staticValuesEffectSO;

    private void Awake() {
        staticValuesEffectSO.StartGame();
    }
}

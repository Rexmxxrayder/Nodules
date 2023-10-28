using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private StaticEffectsValuesSO staticEffectsValuesSO;
    [SerializeField] private StaticInstancesSO staticInstancesSO;

    private void Awake() {
        staticEffectsValuesSO.StartGame();
        staticInstancesSO.StartGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFunction : MonoBehaviour {
    public void SetTimeScale(float newTimeScale) {
        Time.timeScale = newTimeScale;
    }
}

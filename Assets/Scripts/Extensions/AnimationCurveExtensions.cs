using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationCurveExtensions {
    public static float GetCurveDuration(this AnimationCurve curve) {
        return curve.keys[curve.length - 1].time - curve.keys[0].time;
    }

    public static void ChangeCurveDuration(this AnimationCurve curve, float newSize) {
        for (int i = 0; i < curve.length; i++) {
            curve.keys[i].time /= GetCurveDuration(curve);
            curve.keys[i].time *= newSize;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationCurveExtensions {
    public static float GetDuration(this AnimationCurve curve) {
        return curve.keys[curve.length - 1].time - curve.keys[0].time;
    }

    public static void ChangeDuration(this AnimationCurve curve, float newSize) {
        for (int i = 0; i < curve.length; i++) {
            curve.keys[i].time /= GetDuration(curve);
            curve.keys[i].time *= newSize;
        }
    }
}

public static class Vector3Extensions {
    public static Vector3 PlanXZ(this Vector3 curve) {
        return new Vector3(curve.x, 0, curve.z);
    }
}
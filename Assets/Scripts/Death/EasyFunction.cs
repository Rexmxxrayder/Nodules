using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyFunction : MonoBehaviour {
    public static void DebugString(string toSay) {
        Debug.Log(toSay);
    }

    public static void DebugCollision(Collision2D collision) {
        Debug.Log(collision.gameObject.name);
    }

    public void End() {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void EndParent() {
        transform.parent.gameObject.SetActive(false);
        Destroy(transform.parent.gameObject);
    }
}

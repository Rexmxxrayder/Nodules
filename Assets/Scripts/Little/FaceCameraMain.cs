using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCameraMain : MonoBehaviour
{
    private void Update() {
        transform.LookAt(Camera.main.transform.position);
    }
}

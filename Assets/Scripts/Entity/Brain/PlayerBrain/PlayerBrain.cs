using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : Brain {
    private void Update() {
        visor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

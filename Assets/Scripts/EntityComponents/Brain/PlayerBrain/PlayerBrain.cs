using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : EntityBrain {
    private void Update() {
        visor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        visor.z = 0;
    }
}

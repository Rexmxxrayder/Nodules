using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerBrain : PlayerBrain {
    private void Update() {
        if(Input.GetKeyDown("a")) { 
            firstAbility?.Invoke();
        }
        if (Input.GetKeyDown("z")) {
            secondAbility?.Invoke();
        }
        if (Input.GetKeyDown("e")) {
            thirdAbility?.Invoke();
        }
        if (Input.GetKeyDown("r")) {
            fourthAbility?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            fifthAbility?.Invoke();
        }
        visor = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        if(Input.GetMouseButtonDown(1)) {
            newMovePoint?.Invoke(visor);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityBar : MonoBehaviour
{
    [SerializeField] public Canvas canvas;
    [SerializeField] public TMP_Text Text;

    
    private void Update() {
        canvas.worldCamera = PlayerBrain.Transform.GetComponentInChildren<Camera>();
        if (canvas == null)
        {
            return;
        }

        canvas.transform.LookAt(canvas.transform.position - Camera.main.transform.forward);
    }
}

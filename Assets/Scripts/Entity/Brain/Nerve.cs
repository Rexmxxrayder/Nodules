using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

public class Nerve : MonoBehaviour {
    public Brain brain;
    [SerializeField] KeyCode keyCode;
    [SerializeField] public List<BodyPart> bodyparts = new List<BodyPart>();
    [SerializeField] protected UnityEvent<Brain> _keyUp = new UnityEvent<Brain>();
    [SerializeField] protected UnityEvent<Brain> _keyDown = new UnityEvent<Brain>();
    public event UnityAction<Brain> KeyUp { add { _keyUp.AddListener(value); } remove { _keyUp.RemoveListener(value); } }
    public event UnityAction<Brain> KeyDown { add { _keyDown.AddListener(value); } remove { _keyDown.RemoveListener(value); } }
    private void Update() {
        if (Input.GetKeyDown(keyCode)) {
            PressKeyDown();
        }
        if (Input.GetKeyUp(keyCode)) {
            PressKeyUp();
        }
    }

    private void Start() {
        for (int i = 0; i < bodyparts.Count; i++) {
            KeyUp += bodyparts[i].OnButtonUp;
            KeyDown += bodyparts[i].OnButtonDown;
        }
    }

    public void AssignBodyPart(BodyPart bodyPart) {
        bodyparts.Add(bodyPart);
        KeyUp += bodyPart.OnButtonUp;
        KeyDown += bodyPart.OnButtonDown;
    }

    public void UnAssignBodyPart(BodyPart bodyPart) {
        bodyparts.Remove(bodyPart);
        KeyUp -= bodyPart.OnButtonUp;
        KeyDown -= bodyPart.OnButtonDown;
    }
    public void PressKeyUp() {
        _keyUp?.Invoke(brain);
    }

    public void PressKeyDown() {
        _keyDown?.Invoke(brain);
    }
}

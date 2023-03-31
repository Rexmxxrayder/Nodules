using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

public class Nerve : MonoBehaviour {
    public Brain brain;
    [SerializeField] KeyCode keyCode;
    [SerializeField] public List<BodyPart> bodyparts = new List<BodyPart>();
    [SerializeField] protected UnityEvent<Brain> _abilityUp = new UnityEvent<Brain>();
    [SerializeField] protected UnityEvent<Brain> _abilityDown = new UnityEvent<Brain>();
    public event UnityAction<Brain> AbilityUp { add { _abilityUp.AddListener(value); } remove { _abilityUp.RemoveListener(value); } }
    public event UnityAction<Brain> AbilityDown { add { _abilityDown.AddListener(value); } remove { _abilityDown.RemoveListener(value); } }
    private void Update() {
        if (Input.GetKeyDown(keyCode)) {
            _abilityDown?.Invoke(brain);
        }
        if (Input.GetKeyUp(keyCode)) {
            _abilityUp?.Invoke(brain);
        }
    }

    private void Start() {
        for (int i = 0; i < bodyparts.Count; i++) {
            AbilityUp += bodyparts[i].OnButtonUp;
            AbilityDown += bodyparts[i].OnButtonDown;
        }
    }

    public void AssignBodyPart(BodyPart bodyPart) {
        bodyparts.Add(bodyPart);
        AbilityUp += bodyPart.OnButtonUp;
        AbilityDown += bodyPart.OnButtonDown;
    }

    public void UnAssignBodyPart(BodyPart bodyPart) {
        bodyparts.Remove(bodyPart);
        AbilityUp -= bodyPart.OnButtonUp;
        AbilityDown -= bodyPart.OnButtonDown;
    }
}

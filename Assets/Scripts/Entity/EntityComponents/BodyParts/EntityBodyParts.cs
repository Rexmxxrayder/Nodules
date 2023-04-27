using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityBodyParts : EntityComponent {
    public List<BodyPart> Bodyparts = new List<BodyPart>();
    public UnityEvent onMovement = new UnityEvent();
    public event UnityAction OnMovement { add { onMovement.AddListener(value); } remove { onMovement.RemoveListener(value); } }
    public bool isDashing = false;

    protected override void AwakeSetup() {
        for (int i = 0; i < transform.childCount; i++) {
            BodyPart newBodyPart = transform.GetChild(i).GetComponent<BodyPart>();
            Bodyparts.Add(newBodyPart);
        }
    }

    public void ActivateBodyPartUp(int partIndex) {
        Bodyparts[partIndex].OnButtonUp(Get<EntityBrain>());
    }

    public void ActivateBodyPartDown(int partIndex) {
        Bodyparts[partIndex].OnButtonUp(Get<EntityBrain>());
    }


}

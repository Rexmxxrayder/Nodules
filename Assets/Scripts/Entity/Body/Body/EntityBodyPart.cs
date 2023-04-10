using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityBodyPart : EntityComponent {
   // public List<BodyPart> bodyparts = new List<BodyPart>();
    public UnityEvent onMovement = new UnityEvent();
    public event UnityAction OnMovement { add { onMovement.AddListener(value); } remove { onMovement.RemoveListener(value); } }
}

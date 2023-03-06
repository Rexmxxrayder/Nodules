using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityPhysics))]
public abstract class Body : MonoBehaviour {
    [SerializeField] public string bodyName;
    [SerializeField] public Brain brain;
    [SerializeField] public EntityPhysics entityPhysics;
    protected abstract void NewBrain(Brain newBrain);

}

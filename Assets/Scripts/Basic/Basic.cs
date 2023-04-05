using Sloot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Basic : MonoBehaviour {
    public string Type;
    public abstract void Activate(EntityRoot root);
}

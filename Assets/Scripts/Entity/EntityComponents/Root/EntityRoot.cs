using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EntityRoot : EntityComponent {
    string type;
    public string Type => type;
    public override GameObject SetRoot() {
        _root = gameObject;
        return _root;
    }

    private void OnValidate() {
        type = gameObject.name;
    }

    public void GiveType(string typeGiven) { 
        type ??= typeGiven;
    }
}

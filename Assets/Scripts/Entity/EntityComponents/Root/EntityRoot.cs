using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityRoot : EntityComponent {
    public override GameObject SetRoot() {
        _root = gameObject;
        return GetRoot();
    }

    public override GameObject GetRoot() {
        return _root;
    }
}

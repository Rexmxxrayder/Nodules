using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPosition : EntityComponent {
    public static implicit operator Vector3(EntityPosition position) {
        Vector3 output = position.transform.position;
        return output;
    }

    public static implicit operator Vector2(EntityPosition position) {
        Vector2 output = position.transform.position;
        return output;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPlayer : EntityComponent {
    public static GameObject Player;

    protected override void AwakeSetup() {
        if (Player == null) {
            Player = _root;
        } else {
            Destroy(gameObject);
        }
    }
}

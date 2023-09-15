using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvulneraibilityAdd : MonoBehaviour
{
    public bool add = false;
    public bool remove = false;
    InvulnerabilityModifier modifier;
    void Update()
    {
        if(add) {
            add = false;
            modifier = new InvulnerabilityModifier();
            gameObject.RootGet<EntityHealthModfier>().AddModifier(modifier);
        }

        if (remove) {
            remove = false;
            gameObject.RootGet<EntityHealthModfier>().RemoveModifier(modifier);
            modifier = null;
        }
    }
}

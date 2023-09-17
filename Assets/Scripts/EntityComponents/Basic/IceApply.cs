using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceApply : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        EntityEffectManager eem = other.gameObject.RootGet<EntityEffectManager>();
        if(eem != null) {
            IceEffect iceEffect = new ();
            iceEffect.AddStack(1);
            eem.AddEffect(iceEffect);
        }
    }
}

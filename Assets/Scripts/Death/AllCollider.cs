using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCollider : MonoBehaviour
{
    private Action<Collider> onContact;
    public event Action<Collider> OnContact { add { onContact += value; } remove { onContact -= value; } }

    private void OnTriggerEnter(Collider other) {
        onContact(other);
    }
}

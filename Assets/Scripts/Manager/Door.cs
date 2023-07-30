using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 vectora = (transform.position - other.transform.position).normalized;
            Vector3 vectorb = new Vector3(Mathf.RoundToInt(vectora.x), 0, Mathf.RoundToInt(vectora.z));
            RoomManager.Gino.NextRoom(vectorb);
        }
    }
}

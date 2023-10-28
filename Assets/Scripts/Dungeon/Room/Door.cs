using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private EntityBar bar;
    public string Text;
    private bool close = true;
    public bool Close { get => close; set => close = value; }
    private Action<Collider> onEnter;
    public event Action<Collider> OnEnter { add { onEnter += value; } remove { onEnter += value; } }

    private void Start() {
        bar.Text.text = Text;
    }

    private void OnTriggerEnter(Collider other) {
        if(close) return;
        onEnter?.Invoke(other);
    }
}

//if (other.gameObject.CompareTag("Player"))
//{
//    Vector3 vectora = (transform.position - other.transform.position).normalized;
//    Vector3 vectorb = new Vector3(Mathf.RoundToInt(vectora.x), 0, Mathf.RoundToInt(vectora.z));
//    RoomManager.Gino.NextRoom(vectorb);
//}

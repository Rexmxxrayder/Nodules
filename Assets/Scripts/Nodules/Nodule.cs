using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodule : MonoBehaviour {
    [SerializeField] Sprite sprite;
    [SerializeField] Color color;
    [SerializeField] int id;
    public Sprite Sprite { get { return sprite; } }
    public int Id { get { return id; } }
    public Color Color { get { return color; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour {
    public enum BodyPartType {
        HEAD = 0,
        HAND = 1,
        BODY = 2,
        LEG = 3,
        NONE = 4
    }

    public static Dictionary<(string, string), BodyPart> bodyparts = new Dictionary<(string, string), BodyPart>();
    [SerializeField] BodyPartType type = BodyPartType.NONE;

    [SerializeField] protected Nodule currentNodule;
    [SerializeField] protected string bodyName = "";
    [SerializeField] protected string bodyPartName = "";
    [SerializeField] protected int health = 0;
    protected Sprite sprite;
    public Sprite Sprite => sprite;
    public Nodule Nodule => currentNodule;
    public BodyPartType Type => type;
    public int Health => health;

    private void Awake() {
        if (bodyName != "" && bodyPartName != "")
            bodyparts.Add((bodyName, bodyPartName), this);
        ColliderTwoD CTD = GetComponentInChildren<ColliderTwoD>();
        if (CTD != null) {
            CTD.OnCollisionEnter += BodyPartHit;
        }
    }



    public void AddNodules(Nodule nodule) {
        currentNodule = nodule;
        GetComponent<SpriteRenderer>().color = nodule.Color;
    }

    public void RemoveNodules() {
        currentNodule = null;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void BodyPartHit(Collision2D collision) {
        if (collision.gameObject.CompareTag("DoDamage")) {
            health -= 5;
            if (health < 0) health = 0;
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BodyPart : MonoBehaviour {
    [SerializeField] protected Body body;
    [SerializeField] protected Brain brain;

    [SerializeField] protected Nodule currentNodule;
    [SerializeField] protected string bodyPartName = "";
    [SerializeField] protected int health = 0;
    protected Sprite sprite;
    public Sprite Sprite => sprite;
    public Nodule Nodule => currentNodule;
    public int Health => health;

    private void Awake() {
        ColliderTwoD CTD = GetComponentInChildren<ColliderTwoD>();
        if (CTD != null) {
            CTD.OnCollisionEnter += BodyPartHit;
        }
        NewBody(GetComponentInParent<Body>());
    }

    protected virtual void NewBody(Body newBody) {
        body = newBody;
        brain = newBody.brain;
    }

    public void AddNodules(Nodule nodule) {
        currentNodule = nodule;
        GetComponent<SpriteRenderer>().color = nodule.Color;
    }

    public void RemoveNodules() {
        currentNodule = null;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public virtual void LaunchAbility(Vector2 mousePosition) {}

    protected void BodyPartHit(Collision2D collision) {
        if (collision.gameObject.CompareTag("DoDamage")) {
            health -= 5;
            if (health < 0) health = 0;
        }
    }
}



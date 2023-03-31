using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BodyPart : MonoBehaviour {
    public Body body;
    [SerializeField] protected string bodyPartName = "";
    [SerializeField] protected Nodule currentNodule;
    [SerializeField] Ability abilityOnUp;
    [SerializeField] Ability abilityOnDown;

    [SerializeField] protected int health = 0;
    protected Sprite sprite;
    public Sprite Sprite => sprite;
    public Nodule Nodule => currentNodule;
    public int Health => health;

    private void Awake() {
        ColliderTwoD CTD = GetComponentInChildren<ColliderTwoD>();
    }

    public void AddNodules(Nodule nodule) {
        currentNodule = nodule;
        GetComponent<SpriteRenderer>().color = nodule.Color;
    }

    public void RemoveNodules() {
        currentNodule = null;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void OnButtonUp(Brain brain) {
        (Brain, Body) bodyBrain = new(brain, body);
        abilityOnUp.Activate(bodyBrain);
    }
    public void OnButtonDown(Brain brain) {
        (Brain, Body) bodyBrain = new(brain, body);
        abilityOnDown.Activate(bodyBrain);
    }
}



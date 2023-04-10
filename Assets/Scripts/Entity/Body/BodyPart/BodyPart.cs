using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BodyPart : MonoBehaviour {
    [SerializeField] protected string bodyPartName = "";
    [SerializeField] protected Nodule currentNodule;
    [SerializeField] List<Ability> abilitiesOnUpList = new();
    [SerializeField] List<Ability> abilitiesOnDownList = new();
    Dictionary<int, Ability> abilitiesOnUp = new();
    Dictionary<int, Ability> abilitiesOnDown = new();

    [SerializeField] protected int health = 0;
    protected Sprite sprite;
    public Sprite Sprite => sprite;
    public Nodule Nodule => currentNodule;
    public int Health => health;

    private void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            abilitiesOnUpList.Add(transform.GetChild(i).GetChild(0).GetComponent<Ability>());
            abilitiesOnDownList.Add(transform.GetChild(i).GetChild(1).GetComponent<Ability>());
        }
        for (int i = 0; i < abilitiesOnUpList.Count; i++) {
            abilitiesOnUp.Add(i, abilitiesOnUpList[i]);
        }
        for (int i = 0; i < abilitiesOnDownList.Count; i++) {
            abilitiesOnDown.Add(i, abilitiesOnDownList[i]);
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

    public void OnButtonUp(EntityBrain brain) {
        abilitiesOnUp[currentNodule == null ? 0 : currentNodule.Id].Activate(brain);
    }

    public void OnButtonDown(EntityBrain brain) {
        abilitiesOnDown[currentNodule == null ? 0 : currentNodule.Id].Activate(brain);
    }
}



using System.Collections.Generic;
using UnityEngine;

public class EntityBodyPart : EntityComponent {
    [SerializeField] KeyCode keyCode;
    [SerializeField] protected Nodule currentNodule;
    private EntityBrain brain;
    Dictionary<int, Ability> abilities = new();
    public bool Available => abilities[currentNodule == null ? 0 : currentNodule.Id].IsAvailable;

    protected Sprite sprite;

    public Sprite Sprite => sprite;
    public Nodule Nodule => currentNodule;


    protected override void ResetSetup() {
        for (int i = 0; i < transform.childCount; i++) {
            abilities.Add(i, transform.GetChild(i).GetComponent<Ability>());
        }
    }
    public void AssignBrain(EntityBrain newBrain) {
        brain = newBrain;
    }

    private void Update() {
        if (Input.GetKeyDown(keyCode)) {
            KeyEvenement(false);
        }

        if (Input.GetKeyUp(keyCode)) {
            KeyEvenement(true);
        }
    }
    public void AddNodules(Nodule nodule) {
        currentNodule = nodule;
    }

    public void RemoveNodules() {
        currentNodule = null;
    }

    public void KeyEvenement(bool isUp) {
        if(!abilities.ContainsKey(currentNodule == null ? 0 : currentNodule.Id)) {
            return;
        }

        abilities[currentNodule == null ? 0 : currentNodule.Id].Launch(brain, isUp);
    }
}
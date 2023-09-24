using System.Collections.Generic;
using UnityEngine;

public class EntityBodyPart : EntityComponent {
    public enum NoduleType {
        UNCOLOR = 0,
        RED = 1,
        BLUE = 2,
        GREEN = 3,
        YELLOW = 4,
        PURPLE = 5,
        WHITE = 6
    }

    [SerializeField] KeyCode keyCode;
    /*[SerializeField]*/
    protected Nodule currentNodule;
    [SerializeField] protected NoduleType noduleType = NoduleType.UNCOLOR;
    private EntityBrain brain;
    Dictionary<int, Ability> abilities = new();
    public bool Available => abilities[(int)noduleType].IsAvailable;

    protected Sprite sprite;

    public Sprite Sprite => sprite;
    public Nodule Nodule => currentNodule;


    protected override void ResetSetup() {
        for (int i = 0; i < transform.childCount; i++) {
            abilities.Add(i, transform.GetChild(i).GetComponent<Ability>());
        }
    }

    protected override void LoadSetup() {
        RootGet<EntityBrain>().OnCannotAct += CancelAbility;
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
        if (!abilities.ContainsKey((int)noduleType)) {
            return;
        }

        abilities[(int)noduleType].Launch(brain, isUp);
    }

    public void CancelAbility() {
        abilities[(int)noduleType].Cancel();
    }
}
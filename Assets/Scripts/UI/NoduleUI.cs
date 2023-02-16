using UnityEngine;
using UnityEngine.UI;

public class NoduleUI : ItemUI {
    [SerializeField] Nodule nodule;
    public Nodule Nodule { get { return nodule; } }
    protected override void ItemUpdate() {
        if (Input.GetMouseButtonDown(0) && mouseOver) {
            LastItemSelected = this;
        }
    }
    protected override void VisualUpdate() {
        if(nodule == null) { return; }
        image.sprite = nodule.Sprite;
    }

    public void AssignNodule(Nodule newNodule) {
        nodule = newNodule;
        VisualUpdate();
    }

    public void RemoveBodyPart() {
        nodule = null;
        VisualUpdate();
    }

    public override Nodule GetNodule() {
        return nodule;
    }

}


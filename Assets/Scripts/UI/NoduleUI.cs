using UnityEngine;
using static EntityBodyPart;

public class NoduleUI : ItemUI {
    [SerializeField] protected NoduleType noduleType = NoduleType.UNCOLOR;
    protected override void ItemUpdate() {
        if (Input.GetMouseButtonDown(0) && mouseOver) {
            LastItemSelected = this;
        }
    }
    protected override void VisualUpdate() {
        image.color = NoduleColor(GetNodule());
    }

    public void AssignNodule(NoduleType newNodule) {
        noduleType = newNodule;
        VisualUpdate();
    }

    public override NoduleType GetNodule() {
        return noduleType;
    }
}


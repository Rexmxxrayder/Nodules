using UnityEngine;
using static EntityBodyPart;

public class EntityBodyPartUI : ItemUI {
    [SerializeField] EntityBodyPart bodyPart;
    public EntityBodyPart EntityBodyPart { get { return bodyPart; } }

    protected override void ItemUpdate() {
        if (Input.GetMouseButtonDown(0) && mouseOver) {
            LastItemSelected = this;
        }
        if (Input.GetMouseButtonDown(1) && mouseOver) {
            bodyPart.RemoveNodules();
        }
        VisualUpdate();
    }
    protected override void VisualUpdate() {
       image.color = NoduleColor(GetNodule());
    }

    public void AssignBodyPart(EntityBodyPart newBodyPart) {
        bodyPart = newBodyPart;
        VisualUpdate();
    }

    public void RemoveBodyPart() {
        bodyPart = null;
        VisualUpdate();
    }

    public override NoduleType GetNodule() {
        return EntityBodyPart.Nodule;
    }
}

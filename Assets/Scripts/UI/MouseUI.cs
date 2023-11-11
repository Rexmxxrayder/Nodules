using UnityEngine;

public class MouseUI : ItemUI {
    RectTransform rectTransform;

    public override EntityBodyPart.NoduleType GetNodule() {
        if (LastItemSelected != null) {
            return LastItemSelected.GetNodule();
        } else {
            return EntityBodyPart.NoduleType.NONE;
        }
    }

    protected override void ItemStart() {
        rectTransform = GetComponent<RectTransform>();
    }
    protected override void ItemUpdate() {
        rectTransform.position = Input.mousePosition;
        if (Input.GetMouseButtonUp(0)) {
            if (HoverItemSelected != LastItemSelected) {
                if (HoverItemSelected is EntityBodyPartUI isBodyPart) {
                    isBodyPart.EntityBodyPart.AddNodules(LastItemSelected.GetNodule());
                    if (LastItemSelected is EntityBodyPartUI isBodyPartTwo)
                        isBodyPartTwo.EntityBodyPart.RemoveNodules();
                }
            }
            LastItemSelected = null;
        }
        VisualUpdate();
    }

    protected override void VisualUpdate() {
        if (LastItemSelected != null) {
            image.color = EntityBodyPart.NoduleColor(LastItemSelected.GetNodule());
        } else {
            image.color = EntityBodyPart.NoduleColor(EntityBodyPart.NoduleType.NONE);
        }
    }
}

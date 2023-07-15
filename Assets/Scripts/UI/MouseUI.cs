using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MouseUI : ItemUI {
    RectTransform rectTransform;

    protected override void ItemStart() {
        rectTransform = GetComponent<RectTransform>();
    }
    protected override void ItemUpdate() {
        rectTransform.position = Input.mousePosition;
        if (Input.GetMouseButtonUp(0)) {
            if (HoverItemSelected != LastItemSelected) {
                if (HoverItemSelected is EntityBodyPartUI isBodyPart && LastItemSelected.GetNodule() != null) {
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
        if (LastItemSelected != null && LastItemSelected.GetNodule() != null) {
            image.sprite = LastItemSelected.GetNodule().Sprite;
            image.color = Color.white;
        } else {
            image.sprite = null;
            image.color = new Color(0, 0, 0, 0);
        }
    }
}

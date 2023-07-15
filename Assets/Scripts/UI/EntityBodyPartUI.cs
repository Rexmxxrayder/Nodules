using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class EntityBodyPartUI : ItemUI {
    [SerializeField] Image nodulePartImage;
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
        if (bodyPart == null) {
            image.sprite = null;
            nodulePartImage.sprite = null;
        } else {
            image.sprite = bodyPart.Sprite;
            if (bodyPart.Nodule == null) {
                nodulePartImage.sprite = null;
            } else {
                nodulePartImage.sprite = bodyPart.Nodule.Sprite;
            }
        }
    }

    public void AssignBodyPart(EntityBodyPart newBodyPart) {
        bodyPart = newBodyPart;
        VisualUpdate();
    }

    public void RemoveBodyPart() {
        bodyPart = null;
        VisualUpdate();
    }

    public override Nodule GetNodule() {
        return bodyPart.Nodule;
    }
}

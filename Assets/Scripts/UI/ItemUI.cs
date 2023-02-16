using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public static ItemUI LastItemSelected;
    public static ItemUI HoverItemSelected;
    [SerializeField] protected Image image;
    protected bool mouseOver;


    private void Start() {
        VisualUpdate();
        ItemStart();
    }
    void Update() {
        UIUpdate();
        ItemUpdate();
    }
    private void UIUpdate() {}

    protected virtual void ItemStart() {}

    protected virtual void ItemUpdate() {}

    protected abstract void VisualUpdate();

    public virtual Nodule GetNodule() {
        return null;
    }
    public void OnPointerEnter(PointerEventData eventData) {
        mouseOver = true;
        HoverItemSelected = this;
    }

    public void OnPointerExit(PointerEventData eventData) {
        mouseOver = false;
        HoverItemSelected = null;
    }

    public Image Image { get { return image; } }
}

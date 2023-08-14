using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityLifeBar : EntityComponent {
    EntityHealth eh;
    [SerializeField]
    Slider slider;
    protected override void DefinitiveSetup() {
        eh = _root.RootGet<EntityHealth>();
        if (eh == null) {
            Destroy(gameObject);
        }
    }

    protected override void LoadSetup() {
        slider.minValue = 0;
        slider.maxValue = eh.MaxHealth;
        slider.value = eh.Health;
    }

    private void Update() {
        slider.maxValue = eh.MaxHealth;
        slider.value = eh.Health;
    }
}

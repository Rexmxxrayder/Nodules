using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityLifeBar : EntityComponent {
    EntityHealth eh;
    [SerializeField]
    Slider slider;
    protected override void AwakeSetup() {
        eh = _root.Get<EntityHealth>();
        if (eh == null) {
            Destroy(gameObject);
        } else {
            slider.minValue = 0;
            slider.maxValue = eh.MaxHealth;
            slider.value = eh.Health;
        }
    }

    private void Update() {
        slider.maxValue = eh.MaxHealth;
        slider.value = eh.Health;
    }
}

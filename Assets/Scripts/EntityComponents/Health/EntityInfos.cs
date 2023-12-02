using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityInfos : EntityComponent {
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text effectsText;
    private EntityHealth eh;
    private EntityEffectManager eem;

    protected override void LoadSetup() {
        eh = GetRootComponent<EntityHealth>();
        if (eh == null) {
            Destroy(gameObject);
        }

        eem = GetRootComponent<EntityEffectManager>();
        if (eem == null) {
            Destroy(gameObject);
        }

        slider.minValue = 0;
        slider.maxValue = eh.MaxHealth;
        slider.value = eh.Health;
    }

    private void Update() {
        slider.maxValue = eh.MaxHealth;
        slider.value = eh.Health;
        healthText.text = $" {slider.value} / {slider.maxValue}";
        string effects = "";
        foreach (string effect in eem.effectsVisualise) {
            effects += $"{effect}\n";
        }
        effectsText.text = effects;
        canvas.transform.LookAt(canvas.transform.position - Camera.main.transform.forward);
    }
}

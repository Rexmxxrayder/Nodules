using TMPro;
using UnityEngine;

public class EntityEffectsBar : EntityComponent {
    [SerializeField] private TMP_Text effectsText;
    [SerializeField] private Canvas canvas;
    private EntityEffectManager eem;

    protected override void DefinitiveSetup() {
        eem = _root.RootGet<EntityEffectManager>();
        if (eem == null) {
            Destroy(gameObject);
        }
    }

    protected override void LoadSetup() {
        effectsText.text = "";
    }

    private void Update() {
        string effects = "";
        foreach (string effect in eem.effectsVisualise) {
            effects += $"{effect}\n"; 
        }
        canvas.transform.LookAt(canvas.transform.position - Camera.main.transform.forward);
        effectsText.text = effects;
    }
}

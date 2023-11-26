using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class SlasherBrain : EntityBrain {
    [SerializeField] private SlasherData slasherData;
    private EntityBodyPart slash;
    private bool launch;

    protected override void ResetSetup() {
        base.ResetSetup();
        slash = GetComponentInChildren<EntityBodyPart>();
        slasherData.SetupData(this);
    }

    protected override void LoadSetup() {
        base.LoadSetup();
        selected = PlayerBrain.Transform;
    }

    private void Update() {
        visor = selected.transform.position;
        if (slash.Available) {
            StartCoroutine(LaunchAbility());
        }
    }

    private IEnumerator LaunchAbility() {
        if (launch) {
            yield break;
        } else {
            launch = true;
        }

        yield return new WaitForSeconds(Random.Range(0, 5f));
        slash.KeyEvenement(true);
        launch = false;
    }
}

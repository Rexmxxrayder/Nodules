using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class JumperBrain : EntityBrain {
    [SerializeField] private JumperData jumperData;
    private EntityBodyPart jump;
    private bool launch;

    protected override void ResetSetup() {
        base.ResetSetup();
        jump = GetComponentInChildren<EntityBodyPart>();
        jumperData.SetupData(this);
    }

    protected override void LoadSetup() {
        base.LoadSetup();
        selected = PlayerBrain.Transform;
    }

    private void Update() {
        visor = selected.transform.position;
        if (jump.Available) {
            StartCoroutine(LaunchAbility());
        }
    }

    private IEnumerator LaunchAbility() {
        if(launch) {
            yield break;
        } else {
            launch = true;
        }

        yield return new WaitForSeconds(Random.Range(0, 5f));
        jump.KeyEvenement(true);
        launch = false;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class GunnerBrain : EntityBrain {
    [SerializeField] private GunnerData GunnerData;
    private EntityBodyPart shoot;
    private bool launch;

    protected override void ResetSetup() {
        base.ResetSetup();
        shoot = GetComponentInChildren<EntityBodyPart>();
        GunnerData.SetupData(this);
    }

    protected override void LoadSetup() {
        base.LoadSetup();
        selected = PlayerBrain.Transform;
    }

    private void Update() {
        visor = selected.transform.position;
        if (shoot.Available && CanAct) {
            StartCoroutine(LaunchAbility());
        }
    }

    private IEnumerator LaunchAbility() {
        if (launch) {
            yield break;
        } else {
            launch = true;
        }

        yield return new WaitForSeconds(Random.Range(0, 1f));
        shoot.KeyEvenement(true);
        launch = false;
    }
}

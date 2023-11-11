using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBrain : EntityBrain {
    public static Transform Transform;
    [SerializeField] private EntityBodyPart move;

    protected override void DefinitiveSetup() {
        base.DefinitiveSetup();
        Transform = GetRootTransform();
        GetRoot().OnDeath += () => SceneManager.LoadScene(0);
    }
    private void Update() {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, out hit, Mathf.Infinity)) {
            visor = hit.point;
        }

        Debug.DrawLine(visor, transform.position, Color.red);
        if (Input.GetMouseButtonDown(1)) {
            move.KeyEvenement(true);
        }
    }

    protected override void DestroySetup() {
        base.DestroySetup();
        SceneManager.LoadScene(0);
    }
}

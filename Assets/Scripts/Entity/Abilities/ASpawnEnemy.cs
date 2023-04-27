using UnityEngine;
using Sloot;

public class ASpawnEnemy : Ability {
    public string ennemiType;
    public float distSpawn;
    public int NumberSpawn;

    protected override void LaunchAbility(EntityBrain brain) {
        GetComponentInParent<EntityBodyParts>().onMovement?.Invoke();
        Vector3 BodyPosition = gameObject.GetRootPosition();
        for (int i = 0; i < NumberSpawn; i++) {
            Vector3 direction = RotationSloot.GetDirectionOnAxis(360 / NumberSpawn * i, Vector3.up);
            Vector3 ennemiPosition = BodyPosition + direction * distSpawn;
            Spawn(ennemiPosition);
        }
    }

    public void Spawn(Vector3 position) {
        EntityBrain ennemi = BrainPools.Gino.GetInstance(ennemiType);
        ennemi.transform.position = position;
    }
}

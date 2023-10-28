using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicOrbs : Bullet {
    [SerializeField] private int maxBounce;
    [SerializeField] private float rebounce = 0.2f;
    private EntityPhysics ep;
    private int bounceRemaining;
    private float bounceProtect;


    protected override void ResetSetup() {
        base.ResetSetup();
        bounceRemaining = maxBounce;
        bounceProtect = Time.fixedTime - rebounce;
    }

    protected override void LoadSetup() {
        base.LoadSetup();
        RootGet<EntityMainCollider3D>().OnTriggerEnterDelegate += Bounce;
        ep = RootGet<EntityPhysics>();
    }

    private void Bounce(Collider collider) {
        if (!collider.CompareTag("Wall") || Time.fixedTime < bounceProtect + rebounce) {
            return;
        }

        if (bounceRemaining == 0) {
            Die();
        }

        bounceProtect = Time.fixedTime;
        bounceRemaining--;
        Vector3 collisionPoint = collider.ClosestPoint(transform.position);
        Vector3 collisionNormal = transform.position - collisionPoint;
        Vector3 newVelocity = Vector3.Reflect(ep.Velocity, collisionNormal.normalized);
        newVelocity.y = 0;      
        ep.Add(Force.Const(newVelocity, newVelocity.magnitude, 9999), EntityPhysics.PhysicPriority.PROJECTION);
    }

    protected override void DestroySetup() {
        base.DestroySetup();
        RootGet<EntityMainCollider3D>().OnTriggerEnterDelegate -= Bounce;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AFlareOfFire : Ability
{
    [Header("FirstDash")]
    [SerializeField] private float firstDashSpeed;
    [SerializeField] private float firstDashDist;
    [SerializeField] private int firstDashDamage;

    [Header("SecondDash")]
    [SerializeField] private float secondDashSpeed;
    [SerializeField] private float secondDashDist;
    [SerializeField] private int secondDashDamage;
    [SerializeField] private float maxWaitBeforeSecondDash;

    private bool isDashing = false;
    private bool secondDashing;
    private Force dashForce;
    private EntityMainCollider3D mainCollider;
    InvulnerabilityModifier modifier;
    protected override void LaunchAbilityUp(EntityBrain brain) {
        if(isDashing) {
            return;
        }

        secondDashing = !secondDashing;
        EntityPhysics ep = gameObject.RootGet<EntityPhysics>();
        Vector3 startPosition = gameObject.GetRootPosition();
        Vector3 goToPosition = brain.Visor;
        mainCollider = gameObject.RootGet<EntityMainCollider3D>();
        DashTo(ep, startPosition, goToPosition);
    }

    private void DashTo(EntityPhysics ep, Vector3 startPosition, Vector3 goToPosition) {
        isDashing = true;
        dashForce = Force.Const(goToPosition - startPosition, firstDashSpeed, firstDashDist / firstDashSpeed);
        dashForce.OnEnd += (_) => EndDash();
        
        mainCollider.GetRoot().GetComponentInChildren<Collider>().isTrigger = true;
        mainCollider.OnTriggerEnterDelegate += FireBodyDamage;
        modifier = new InvulnerabilityModifier();
        gameObject.RootGet<EntityHealthModfier>().AddModifier(modifier);
        ep.Add(dashForce, EntityPhysics.PhysicPriority.DASH);
    }

    private void EndDash() {
        gameObject.RootGet<EntityHealthModfier>().RemoveModifier(modifier);
        modifier = null;
        mainCollider.GetRoot().GetComponentInChildren<Collider>().isTrigger = false;
        mainCollider.OnTriggerEnterDelegate -= FireBodyDamage;
        if(!secondDashing) {
            StartCoroutine(SecondDashWait());
        } else {
            StartCooldown();
        }
        isDashing = false;
    }

    private void FireBodyDamage(Collider collider) {
        if (collider.gameObject.GetRoot().CompareTag("Enemy")) {
            collider.gameObject.RootGet<EntityHealth>().RemoveHealth(!secondDashing? firstDashDamage : secondDashDamage);
        }
    }

    private IEnumerator SecondDashWait() {
        yield return new WaitForSeconds(maxWaitBeforeSecondDash);
        if (!secondDashing) {
            StartCooldown();
            secondDashing = true;
        }
    }
}

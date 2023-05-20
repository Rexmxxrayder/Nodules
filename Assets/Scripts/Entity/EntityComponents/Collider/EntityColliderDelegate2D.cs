using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class EntityColliderDelegate2D : EntityCollider2D {
    protected Collider2D collider2d;
    public override bool IsActive {
        get { return isActive; }
        set {
            isActive = value;
            Collider.enabled = value;
        }
    }
    public Collider2D Collider => collider2d;

    protected override void AwakeSetup() {
        collider2d = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!isActive) { return; }
        _onCollisionEnter.Invoke(collision);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (!isActive) { return; }
        _onCollisionStay.Invoke(collision);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (!isActive) { return; }
        _onCollisionExit.Invoke(collision);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (!isActive) { return; }
        _onTriggerEnter.Invoke(collider);
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if (!isActive) { return; }
        _onTriggerStay.Invoke(collider);
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (!isActive) { return; }
        _onTriggerExit.Invoke(collider);
    }
}
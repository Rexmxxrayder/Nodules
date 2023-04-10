using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityColliderDelegate2D : EntityCollider2D {
    protected Collider2D collider2d;
    [SerializeField] bool _debug = false;

    public Collider2D GetCollider() {
        return collider2d;
    }

    protected override void AwakeSetup() {
        collider2d = GetComponent<Collider2D>();
        if(GetCollider() == null) {
            Debug.LogError("No Collider");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!_active) { return; }
        if (_debug) { DebugMe("Collision Enter " + collision.gameObject.name); }
        _onCollisionEnter.Invoke(collision);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (!_active) { return; }
        if (_debug) { DebugMe("Collision Stay " + collision.gameObject.name); }
        _onCollisionStay.Invoke(collision);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (!_active) { return; }
        if (_debug) { DebugMe("Collision Exit " + collision.gameObject.name); }
        _onCollisionExit.Invoke(collision);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (!_active) { return; }
        if (_debug) { DebugMe("Trigger Enter " + collider.gameObject.name); }
        _onTriggerEnter.Invoke(collider);
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if (!_active) { return; }
        if (_debug) { DebugMe("Trigger Stay " + collider.gameObject.name); }
        _onTriggerStay.Invoke(collider);
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (!_active) { return; }
        if (_debug) { DebugMe("Trigger Exit " + collider.gameObject.name); }
        _onTriggerExit.Invoke(collider);
    }

    private void DebugMe(string message) {
        Debug.Log(name + " debug: " + message);
    }

    public override void SetTrigger(bool newState) {
        collider2d.isTrigger= newState;
    }
}
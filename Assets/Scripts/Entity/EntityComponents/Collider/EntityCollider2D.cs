using Sloot;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EntityCollider2D : EntityComponent {
    protected bool _active = true;
    public bool Active { get => _active; set => _active = value; }

    #region Events
    [SerializeField] protected UnityEvent<Collision2D> _onCollisionEnter = new UnityEvent<Collision2D>();
    [SerializeField] protected UnityEvent<Collision2D> _onCollisionExit = new UnityEvent<Collision2D>();
    [SerializeField] protected UnityEvent<Collision2D> _onCollisionStay = new UnityEvent<Collision2D>();
    [SerializeField] protected UnityEvent<Collider2D> _onTriggerEnter = new UnityEvent<Collider2D>();
    [SerializeField] protected UnityEvent<Collider2D> _onTriggerExit = new UnityEvent<Collider2D>();
    [SerializeField] protected UnityEvent<Collider2D> _onTriggerStay = new UnityEvent<Collider2D>();

    public event UnityAction<Collision2D> OnCollisionEnter { add => _onCollisionEnter.AddListener(value); remove => _onCollisionEnter.RemoveListener(value); }
    public event UnityAction<Collision2D> OnCollisionExit { add => _onCollisionExit.AddListener(value); remove => _onCollisionExit.RemoveListener(value); }
    public event UnityAction<Collision2D> OnCollisionStay { add => _onCollisionStay.AddListener(value); remove => _onCollisionStay.RemoveListener(value); }
    public event UnityAction<Collider2D> OnTriggerEnter { add => _onTriggerEnter.AddListener(value); remove => _onTriggerEnter.RemoveListener(value); }
    public event UnityAction<Collider2D> OnTriggerExit { add => _onTriggerExit.AddListener(value); remove => _onTriggerExit.RemoveListener(value); }
    public event UnityAction<Collider2D> OnTriggerStay { add => _onTriggerStay.AddListener(value); remove => _onTriggerStay.RemoveListener(value); }
    #endregion

    protected void AssignTo(EntityCollider2D entityCollider2D) {
        entityCollider2D.OnCollisionEnter += (x) => _onCollisionEnter?.Invoke(x);
        entityCollider2D.OnCollisionExit += (x) => _onCollisionExit?.Invoke(x);
        entityCollider2D.OnCollisionStay += (x) => _onCollisionStay?.Invoke(x);
        entityCollider2D.OnTriggerEnter += (x) => _onTriggerEnter?.Invoke(x);
        entityCollider2D.OnTriggerExit += (x) => _onTriggerExit?.Invoke(x);
        entityCollider2D.OnTriggerStay += (x) => _onTriggerStay?.Invoke(x);
    }

    public void GetAssign(Action<Collider2D> method) {
        OnTriggerEnter += (x) => method(x);
        OnTriggerExit += (x) => method(x);
        OnTriggerStay += (x) => method(x);
    }

    public void GetAssign(Action<Collision2D> method) {
        OnCollisionEnter += (x) => method(x);
        OnCollisionExit += (x) => method(x);
        OnCollisionStay += (x) => method(x);
    }

    public abstract void SetTrigger(bool newState);

    public void ResetListeners() {
        _onCollisionEnter.RemoveAllListeners();
        _onCollisionExit.RemoveAllListeners();
        _onCollisionStay.RemoveAllListeners();      
        _onTriggerEnter.RemoveAllListeners();
        _onTriggerExit.RemoveAllListeners();
        _onTriggerStay.RemoveAllListeners();
    }
}

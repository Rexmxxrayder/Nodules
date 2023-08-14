using UnityEngine.Events;

public abstract class EntityCollider<T,U> : EntityComponent {
    protected bool isActive = true;
    public virtual bool IsActive {
        get { return isActive; }
        set {
            isActive = value;
        }
    }

    protected override void ResetSetup() {
        ResetListeners();
    }

    #region Events
    protected UnityEvent<T> _onCollisionEnterDelegate = new();
    protected UnityEvent<T> _onCollisionExitDelegate = new();
    protected UnityEvent<T> _onCollisionStayDelegate = new();
    protected UnityEvent<U> _onTriggerEnterDelegate = new();
    protected UnityEvent<U> _onTriggerExitDelegate = new();
    protected UnityEvent<U> _onTriggerStayDelegate = new();

    public event UnityAction<T> OnCollisionEnterDelegate { add => _onCollisionEnterDelegate.AddListener(value); remove => _onCollisionEnterDelegate.RemoveListener(value); }
    public event UnityAction<T> OnCollisionExitDelegate { add => _onCollisionExitDelegate.AddListener(value); remove => _onCollisionExitDelegate.RemoveListener(value); }
    public event UnityAction<T> OnCollisionStayDelegate { add => _onCollisionStayDelegate.AddListener(value); remove => _onCollisionStayDelegate.RemoveListener(value); }
    public event UnityAction<U> OnTriggerEnterDelegate { add => _onTriggerEnterDelegate.AddListener(value); remove => _onTriggerEnterDelegate.RemoveListener(value); }
    public event UnityAction<U> OnTriggerExitDelegate { add => _onTriggerExitDelegate.AddListener(value); remove => _onTriggerExitDelegate.RemoveListener(value); }
    public event UnityAction<U> OnTriggerStayDelegate { add => _onTriggerStayDelegate.AddListener(value); remove => _onTriggerStayDelegate.RemoveListener(value); }
    #endregion

    protected void AssignTo(EntityCollider<T,U> entityCollider) {
        entityCollider.OnCollisionEnterDelegate += (x) => _onCollisionEnterDelegate?.Invoke(x);
        entityCollider.OnCollisionExitDelegate += (x) => _onCollisionExitDelegate?.Invoke(x);
        entityCollider.OnCollisionStayDelegate += (x) => _onCollisionStayDelegate?.Invoke(x);
        entityCollider.OnTriggerEnterDelegate += (x) => _onTriggerEnterDelegate?.Invoke(x);
        entityCollider.OnTriggerExitDelegate += (x) => _onTriggerExitDelegate?.Invoke(x);
        entityCollider.OnTriggerStayDelegate += (x) => _onTriggerStayDelegate?.Invoke(x);
    }

    public void ResetListeners() {
        _onCollisionEnterDelegate.RemoveAllListeners();
        _onCollisionExitDelegate.RemoveAllListeners();
        _onCollisionStayDelegate.RemoveAllListeners();
        _onTriggerEnterDelegate.RemoveAllListeners();
        _onTriggerExitDelegate.RemoveAllListeners();
        _onTriggerStayDelegate.RemoveAllListeners();
    }
}

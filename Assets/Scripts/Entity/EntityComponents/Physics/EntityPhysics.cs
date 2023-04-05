using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sloot;

public class EntityPhysics : EntityComponent, IReset {
    public enum PhysicPriority {
        PLAYER_INPUT = 10, DASH = 20, PROJECTION = 30, BLOCK = 40, ENVIRONNEMENT = 50, SYSTEM = 60
    }

    [SerializeField] Rigidbody2D _rb;
    [SerializeField] List<Force> _forcesDisplay = new List<Force>();
    [SerializeField] bool _debug = false;
    [SerializeField, HideInInspector] UnityEvent<Vector2> _onMove = new UnityEvent<Vector2>();

    SortedDictionary<int, List<Force>> _forces = new SortedDictionary<int, List<Force>>();

    public Vector2 Velocity => _rb.velocity;
    public event UnityAction<Vector2> OnMove { add => _onMove.AddListener(value); remove => _onMove.RemoveListener(value); }

    private void FixedUpdate() {
        Vector2 velocity = ComputeForces();
        if (velocity != Vector2.zero) { _onMove.Invoke(velocity); }
        _rb.velocity = velocity;
    }

    #region Get/Set

    public void Add(Force force, int priority) {
        if (!_forces.ContainsKey(priority)) { _forces.Add(priority, new List<Force>()); }
        _forcesDisplay.Add(force);
        _forces[priority].Add(force);
    }

    public void Remove(Force force) {
        try {
            foreach (KeyValuePair<int, List<Force>> pair in _forces) {
                if (pair.Value.Contains(force)) {
                    Remove(force, pair.Key);
                    _forcesDisplay.Remove(force);
                }
            }
        } catch (System.Exception e) {
            if (_debug)
                Debug.LogError(e);
        }
    }

    public void Remove(Force force, int priority) {
        if (!_forces.ContainsKey(priority)) { return; }
        if (!_forces[priority].Contains(force)) { return; }

        _forces[priority].Remove(force);
        _forcesDisplay.Remove(force);

        if (_forces[priority].Count <= 0) { _forces.Remove(priority); }
    }

    #endregion

    public Vector2 ComputeForces() {
        Vector2 force = Vector2.zero;

        if (_forces.Count == 0) {
            return force;
        }

        float weight = 1f;

        int maxPriority = _forces.Keys.Last();
        int minPriority = _forces.Keys.First();

        try {
            for (int priority = maxPriority; priority >= minPriority; --priority) {
                if (!_forces.ContainsKey(priority)) { continue; }
                float maxWeightTaken = 0;
                for (int index = 0; index < _forces[priority].Count; ++index) {
                    if (_forces[priority][index] == null) { continue; }
                    if (_forces[priority][index].Ignored) { continue; }
                    _forces[priority][index].Update(Time.fixedDeltaTime);
                    float currentWeightTaken = Mathf.Min(weight, _forces[priority][index].Weight);
                    force += _forces[priority][index].Evaluate() * currentWeightTaken;
                    maxWeightTaken = Mathf.Max(currentWeightTaken, maxWeightTaken);
                    if (_forces[priority][index].HasEnded) {
                        Remove(_forces[priority][index], priority);
                        continue;
                    }
                }
                weight -= maxWeightTaken;
                weight = Mathf.Max(0, weight);
            }
        } catch (KeyNotFoundException k) {
            if (_debug) { Debug.LogWarning("Happen : " + k); }
        } catch (System.Exception e) {
            Debug.LogError(e);
        }

        return force;
    }

    public void Purge() {
        _rb.velocity = Vector2.zero;
        try {
            foreach (KeyValuePair<int, List<Force>> key in _forces) {
                for (int i = 0; i < key.Value.Count; i++) {
                    key.Value[i].End();
                }
            }
        } catch (KeyNotFoundException k) {
            if (_debug) { Debug.LogWarning("Happen : " + k); }
        } catch (System.Exception e) {
            Debug.LogError(e);
        }
    }

    protected override void ChildSetup() {
        if (_rb == null) {
            if (_root.GetComponent<Rigidbody2D>() == null) {
                _rb = _root.AddComponent<Rigidbody2D>();
            } else {
                _rb = _root.GetComponent<Rigidbody2D>();
            }
        }
        _rb.gravityScale = 0;
        _rb.freezeRotation = true;
    }

    public void InstanceReset() {
        Purge();
    }
}

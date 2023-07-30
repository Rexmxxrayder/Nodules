
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Force {
    [SerializeField] Vector2 _direction = Vector2.zero;
    [SerializeField, Range(0f, 1f)] float _weight = 1f;
    [SerializeField] List<AnimationCurve> _curves = new List<AnimationCurve>();

    [SerializeField] int _currentCurve = 0;
    [SerializeField] float _currentPercent = 0f;
    bool _ignored = false;
    bool _hasEnded = false;

    [SerializeField] UnityEvent<Force> _onStart = new UnityEvent<Force>();
    [SerializeField] UnityEvent<Force, int> _onNewCurve = new UnityEvent<Force, int>();
    [SerializeField] UnityEvent<Force> _onEnd = new UnityEvent<Force>();

    #region Properties

    public Vector2 Direction { get => _direction; set => _direction = value.normalized; }
    public float CurrentStrength { get => Evaluate().magnitude; }
    public float Weight { get => _weight; set => _weight = value; }
    public bool Ignored { get => _ignored; set => _ignored = value; }
    public bool HasEnded { get => _hasEnded; }
    public List<AnimationCurve> Curves { get => _curves; set => _curves = value; }

    #endregion

    #region Events

    public event UnityAction<Force> OnStart { add => _onStart.AddListener(value); remove => _onStart.RemoveListener(value); }
    public event UnityAction<Force, int> OnNewCurve { add => _onNewCurve.AddListener(value); remove => _onNewCurve.RemoveListener(value); }
    public event UnityAction<Force> OnEnd { add => _onEnd.AddListener(value); remove => _onEnd.RemoveListener(value); }

    #endregion

    #region Constructors

    public Force(List<AnimationCurve> curves, Vector2 direction, float weight = 1f) {
        _onStart = new UnityEvent<Force>();
        _onNewCurve = new UnityEvent<Force, int>();
        _onEnd = new UnityEvent<Force>();
        _direction = direction.normalized;
        _weight = Mathf.Clamp(weight, 0f, 1f);
        _curves = curves;
    }

    public Force(AnimationCurve curve, Vector2 direction, float weight = 1f) :
        this(new List<AnimationCurve> { curve }, direction, weight) {

    }

    public Force(Force force) :
        this(force._curves, force._direction, force._weight) {
    }

    public static Force Const(Vector2 direction, float strength, float duration = 1f, float weight = 1f) {
        return new Force(AnimationCurve.Constant(0f, duration, strength), direction, weight);
    }

    public static Force LinearUp(Vector2 direction, float strength, float duration = 1f, float weight = 1f) {
        return new Force(AnimationCurve.Linear(0f, 0f, duration, strength), direction, weight); ;
    }

    public static Force LinearDown(Vector2 direction, float strength, float duration = 1f, float weight = 1f) {
        return new Force(AnimationCurve.Linear(0f, strength, duration, 0f), direction, weight);
    }

    public static Force LinearTriangleUp(Vector2 direction, float strength, float duration = 1f, float weight = 1f) {
        return new Force(new List<AnimationCurve> {
            AnimationCurve.Linear(0f, 0f, duration / 2f, strength),
            AnimationCurve.Linear(0f, strength, duration / 2f, 0f)},
            direction, weight);
    }

    public static Force LinearTriangleDown(Vector2 direction, float strength, float duration = 1f, float weight = 1f) {
        return new Force(new List<AnimationCurve> {
            AnimationCurve.Linear(0f, strength, duration / 2f, 0f),
            AnimationCurve.Linear(0f, 0f, duration / 2f, strength) },
            direction, weight);
    }

    public static Force Empty() {
        return new Force(AnimationCurve.Constant(0f, 0f, 0f), Vector3.zero, 1f);
    }

    #endregion

    public void AddCurves(List<AnimationCurve> curves) {
        _curves.AddRange(curves);
    }

    public void AddCurve(AnimationCurve curve) {
        AddCurves(new List<AnimationCurve> { curve });
    }

    public void AddCurve(Force force) {
        AddCurves(force._curves);
    }

    public Vector2 Evaluate() {
        return Evaluate(_currentPercent);
    }

    public Vector2 Evaluate(float percent) {
        Mathf.Clamp(0f, 1f, percent);
        return _direction * _curves[_currentCurve].Evaluate(percent);
    }

    public void Update(float deltaTime) {
        if (_currentPercent == 0f && _currentCurve == 0f) {
            _onStart?.Invoke(this);
        }
        _currentPercent += deltaTime / _curves[_currentCurve].GetCurveDuration();
        if (_currentPercent >= 1f && !_hasEnded) {
            NextCurve(_currentPercent - 1f);
        }
    }

    public void Reset() {
        _currentPercent = 0f;
        _currentCurve = 0;
    }

    public void ResetCurve() {
        _currentPercent = 0f;
    }

    public void End() {
        _currentPercent = 1f;
        _currentCurve = _curves.Count - 1;
        _hasEnded = true;
        _onEnd?.Invoke(this);
    }

    public void NextCurve(float newPercent = 0f) {
        _currentPercent = newPercent;
        _currentCurve++;
        if (_currentCurve >= _curves.Count) {
            End();
        } else {
            _onNewCurve?.Invoke(this, _currentCurve);
        }
    }
}




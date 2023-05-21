using Sloot;
using UnityEngine;
using UnityEngine.Events;

public class EntityRangeFloat : EntityComponent {
    [SerializeField] protected float _currentValue;
    [SerializeField] protected float _maxValue;
    [SerializeField] protected float _minValue;

    public float CurrentValue => _currentValue;
    public float MaxValue => _maxValue;
    public float MinValue => _minValue;

    protected readonly UnityEvent<float> _onIncreasing = new(); 
    protected readonly UnityEvent<float> _onDecreasing = new();
    protected readonly UnityEvent<float> _onOverIncreased = new();
    protected readonly UnityEvent<float> _onOverDecreased = new();

    public float IncreaseOf(float value) {
        value = Mathf.Max(0, value);
        if (value == 0) { return _currentValue; }

        if (_currentValue + value > _maxValue) {
            _currentValue = _maxValue;
            _onOverIncreased?.Invoke(_currentValue + value - _maxValue);
        } else {
            _currentValue += value;

        }
        _onIncreasing?.Invoke(value);
        return _currentValue;
    }

    public float DecreaseOf(float value) {
        value = Mathf.Max(0, value);
        if (value == 0) { return _currentValue; }

        if (_currentValue - value < _minValue) {
            _currentValue = _minValue;
            _onOverDecreased?.Invoke(_minValue - (_currentValue - value));
        } else {
            _currentValue -= value;
        }
        _onDecreasing?.Invoke(value);
        return _currentValue;
    }

    public void EqualTo(float value) {
        if (_currentValue == value) { return; }
        if (_currentValue < value) {
            IncreaseOf(Mathf.Abs(value - _currentValue));
        } else {
            DecreaseOf(Mathf.Abs(value - _currentValue));
        }
    }

    public void NewMaxValue(float newMaxValue) {
        _maxValue = newMaxValue;
        if (_currentValue > _maxValue) {
            _currentValue = _maxValue;
        }
        if (_minValue > _maxValue) {
            _minValue = _maxValue;
        }
    }

    public void NewMinValue(float newMinValue) {
        _minValue = newMinValue;
        if (_currentValue < _minValue) {
            _currentValue = _minValue;
        }
        if (_maxValue < _minValue) {
            _maxValue = _minValue;
        }
    }

    public float GetPercentRange(float percent) {
        return (_maxValue - _minValue) * percent / 100;
    }

    protected override void AwakeSetup() {
        NewMaxValue(_maxValue);
        _currentValue = _minValue;
        RemoveAllListeners();
    }

    protected virtual void RemoveAllListeners() {
        _onIncreasing.RemoveAllListeners();
        _onDecreasing.RemoveAllListeners();
        _onOverIncreased.RemoveAllListeners();
        _onOverDecreased.RemoveAllListeners();
    }
}

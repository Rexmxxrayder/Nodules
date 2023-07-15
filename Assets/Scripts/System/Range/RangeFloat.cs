using Sloot;
using UnityEngine;
using UnityEngine.Events;

public class RangeFloat {
    [SerializeField] protected float _minValue = 0;
    [SerializeField] protected float _maxValue = 1f;
    [SerializeField] protected float _currentValue = 0;

    UnityEvent<float> _onIncreasing = new();
    UnityEvent<float> _onDecreasing = new();
    UnityEvent<float> _onOverIncreased = new();
    UnityEvent<float> _onOverDecreased = new();
    public float MinValue { get => _minValue; set => NewMinValue(value); }
    public float MaxValue { get => _maxValue; set => NewMaxValue(value); }
    public float CurrentValue { get => _currentValue; set => NewCurrentValue(value); }

    public event UnityAction<float> OnIncreasing { add => _onIncreasing.AddListener(value); remove => _onIncreasing.RemoveListener(value); }
    public event UnityAction<float> OnDecreasing { add => _onDecreasing.AddListener(value); remove => _onDecreasing.RemoveListener(value); }
    public event UnityAction<float> OnOverIncreased { add => _onOverIncreased.AddListener(value); remove => _onOverIncreased.RemoveListener(value); }
    public event UnityAction<float> OnOverDecreased { add => _onOverDecreased.AddListener(value); remove => _onOverDecreased.RemoveListener(value); }

    public RangeFloat(float minValue, float maxValue, float currentValue) { 
        MinValue = minValue;
        MaxValue = maxValue;
        CurrentValue = currentValue;
    }

    private void NewMinValue(float newMinValue) {
        _minValue = newMinValue;
        if (_currentValue < _minValue) {
            _currentValue = _minValue;
        }
        if (_maxValue < _minValue) {
            _maxValue = _minValue;
        }
    }

    private void NewMaxValue(float newMaxValue) {
        _maxValue = newMaxValue;
        if (_currentValue > _maxValue) {
            _currentValue = _maxValue;
        }
        if (_minValue > _maxValue) {
            _minValue = _maxValue;
        }
    }

    private void NewCurrentValue(float value) {
        if (_currentValue == value) { return; }
        if (_currentValue < value) {
            IncreaseOf(Mathf.Abs(value - _currentValue));
        } else {
            DecreaseOf(Mathf.Abs(value - _currentValue));
        }
    }

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


    public float GetPercentRange(float percent) {
        return (_maxValue - _minValue) * percent / 100;
    }

    public void ClearEvent() {
        _onIncreasing.RemoveAllListeners();
        _onDecreasing.RemoveAllListeners();
        _onOverIncreased.RemoveAllListeners();
        _onOverDecreased.RemoveAllListeners();
    }
}

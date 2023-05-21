using Sloot;
using UnityEngine;
using UnityEngine.Events;

public abstract class EntityRangeInt : EntityComponent {
    [SerializeField] protected int _currentValue;
    [SerializeField] protected int _maxValue;
    [SerializeField] protected int _minValue;

    public int CurrentValue => _currentValue;
    public int MaxValue => _maxValue;
    public int MinValue => _minValue;

    protected readonly UnityEvent<int> _onIncreasing = new();
    protected readonly UnityEvent<int> _onDecreasing = new();
    protected readonly UnityEvent<int> _onOverIncreased = new();
    protected readonly UnityEvent<int> _onOverDecreased = new();

    protected int IncreaseOf(int value) {
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

    protected int DecreaseOf(int value) {
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

    protected void EqualTo(int value) {
        if (_currentValue == value) { return; }
        if (_currentValue < value) {
            IncreaseOf(Mathf.Abs(value - _currentValue));
        } else {
            DecreaseOf(Mathf.Abs(value - _currentValue));
        }
    }

    protected void NewMaxValue(int newMaxValue) {
        _maxValue = newMaxValue;
        if (_currentValue > _maxValue) {
            _currentValue = _maxValue;
        }
        if (_minValue > _maxValue) {
            _minValue = _maxValue;
        }
    }

    protected void NewMinValue(int newMinValue) {
        _minValue = newMinValue;
        if (_currentValue < _minValue) {
            _currentValue = _minValue;
        }
        if (_maxValue < _minValue) {
            _maxValue = _minValue;
        }
    }

    protected int GetPercentRange(int percent) {
        return (_maxValue - _minValue) * percent / 100;
    }
    protected virtual void RemoveAllListeners() {
        _onIncreasing.RemoveAllListeners();
        _onDecreasing.RemoveAllListeners();
        _onOverIncreased.RemoveAllListeners();
        _onOverDecreased.RemoveAllListeners();
    }

    protected override void AwakeSetup() {
        NewMaxValue(_maxValue);
        _currentValue = _minValue;
        RemoveAllListeners();
    }

}

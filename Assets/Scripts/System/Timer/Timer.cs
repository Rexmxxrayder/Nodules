using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Sloot {
    public class Timer {
        public enum TimerState {
            RUNNING,
            RUNNINGOFFSET,
            PAUSED,
            WAITING
        }

        float _duration = 1f;
        float _currentDuration = 0f;
        bool _loop = true;

        TimerState state = TimerState.WAITING;

        Coroutine coroutine;

        public float Duration { get => _duration; set => _duration = value; }
        public float CurrentDuration => _currentDuration;
        public bool IsLooping => _loop;

        [SerializeField] UnityEvent _onActivate = new UnityEvent();
        [SerializeField] UnityEvent _onStart = new UnityEvent();
        [SerializeField] UnityEvent _onEndOffset = new UnityEvent();
        [SerializeField] UnityEvent _onPause = new UnityEvent();
        [SerializeField] UnityEvent _onContinue = new UnityEvent();
        [SerializeField] UnityEvent _onStop = new UnityEvent();
        [SerializeField] UnityEvent _onEnd = new UnityEvent();
        [SerializeField] UnityEvent _onReset = new UnityEvent();

        public event UnityAction OnActivate { add => _onActivate.AddListener(value); remove => _onActivate.RemoveListener(value); }
        public event UnityAction OnStart { add => _onStart.AddListener(value); remove => _onStart.RemoveListener(value); }
        public event UnityAction OnEndOffset { add => _onEndOffset.AddListener(value); remove => _onEndOffset.RemoveListener(value); }
        public event UnityAction OnPause { add => _onPause.AddListener(value); remove => _onPause.RemoveListener(value); }
        public event UnityAction OnContinue { add => _onContinue.AddListener(value); remove => _onContinue.RemoveListener(value); }
        public event UnityAction OnStop { add => _onStop.AddListener(value); remove => _onStop.RemoveListener(value); }
        public event UnityAction OnEnd { add => _onEnd.AddListener(value); remove => _onEnd.RemoveListener(value); }
        public event UnityAction OnReset { add => _onReset.AddListener(value); remove => _onReset.RemoveListener(value); }

        public Timer(float duration, UnityAction onActivateFunction = null, bool loop = true) {
            TimerManager.Activate(this);
            _duration = duration;
            _loop = loop;
            if (onActivateFunction != null) {
                OnActivate += onActivateFunction;
            }
        }

        public Timer Start(float offset = 0) {
            Stop();
            coroutine = TimerManager.instance.StartCoroutine(StartTimer(offset));
            return this;
        }

        public void Pause() {
            if (state != TimerState.RUNNING && state != TimerState.RUNNINGOFFSET) { return; }
            state = TimerState.PAUSED;
            _onPause?.Invoke();
        }

        public void Continue() {
            if (state != TimerState.PAUSED) { return; }
            state = TimerState.RUNNING;
            _onContinue?.Invoke();
        }



        public void Stop() {
            if (state == TimerState.WAITING) {
                return;
            }
            state = TimerState.WAITING;
            if (coroutine != null) {
                TimerManager.instance.StopCoroutine(coroutine);
            }
            _onStop?.Invoke();
        }

        public void Reset() {
            if (state == TimerState.WAITING) { return; }
            _currentDuration = 0;
            _onReset?.Invoke();
        }
        public TimerState GetState() {
            return state;
        }

        public void StopLoop() {
            _loop = false;
        }

        public void StartLoop() {
            _loop = true;
        }
        void End() {
            _onEnd?.Invoke();
            state= TimerState.WAITING;
        }

        IEnumerator StartTimer(float offset) {
            state = TimerState.RUNNINGOFFSET;
            _onStart?.Invoke();
            if (0 < offset) {
                float _offsetduration = 0;
                OnReset += () => _offsetduration = 0;
                while (_offsetduration < offset) {
                    yield return null;
                    if (state == TimerState.RUNNINGOFFSET)
                        _offsetduration += Time.deltaTime;
                }
                OnReset -= () => _offsetduration = 0;
            }
            state = TimerState.RUNNING;
            _onEndOffset?.Invoke();
            do {
                _currentDuration = 0f;
                while (_currentDuration < _duration) {
                    yield return null;
                    if (state == TimerState.RUNNING)
                        _currentDuration += Time.deltaTime;
                }
                _onActivate?.Invoke();
            } while (_loop);
            End();
        }
    }
}

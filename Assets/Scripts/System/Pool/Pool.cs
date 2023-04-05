using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sloot {
    public class Pool<T> where T : MonoBehaviour {
        List<T> _pool;
        List<T> _alive;
        T _original;
        int _count;
        int _maxCount;
        GameObject _poolStorage;

        public int Count { get { return _count; } }
        public float MaxCount { get { return _maxCount; } }

        public Pool(T original) {
            _pool = new List<T>();
            _alive = new List<T>();
            _original = original;
            _count = 0;
            _maxCount = int.MaxValue;
        }

        public Pool(T original, GameObject poolStorage = null, int maxCount = int.MaxValue) {
            _pool = new List<T>();
            _alive = new List<T>();
            _original = original;
            _count = 0;
            if (maxCount < 0) {
                _maxCount = 0;
            } else {
                _maxCount = maxCount;
            }
            if (poolStorage != null) {
                _poolStorage = poolStorage;
            }
        }

        public T GetInstance() {
            T newObject;
            if (_pool.Count == 0) {
                if (_maxCount <= _count) {
                    newObject = _alive[0];
                    _alive.Remove(newObject);
                } else {
                    newObject = UnityEngine.Object.Instantiate(_original);
                    newObject.gameObject.name = newObject.GetType().Name + " N°" + _count;
                    Reset(newObject);
                    _count++;
                    if (_poolStorage != null) {
                        newObject.gameObject.transform.parent = _poolStorage.transform;
                    }
                }
            } else {
                newObject = _pool[0];
                _pool.Remove(newObject);
            }
            _alive.Add(newObject);
            newObject.gameObject.SetActive(true);
            return newObject;
        }

        public void LetInstance(T instance) {
            if (_alive.Contains(instance)) {
                instance.gameObject.SetActive(false);
                Reset(instance);
                _alive.Remove(instance);
                _pool.Add(instance);
                if (_poolStorage != null) {
                    instance.gameObject.transform.parent = _poolStorage.transform;
                }
            }
        }

        public void ChangeMaxCount(int newMaxCount) {
            _maxCount = newMaxCount;
        }

        public bool Contains(T instance) {
            return _alive.Contains(instance) || _pool.Contains(instance);
        }

        void Reset(T instance) {
            instance.transform.position = Vector3.zero;
            instance.transform.rotation = Quaternion.identity;
            instance.StopAllCoroutines();
            (instance as IReset)?.InstanceReset();
            foreach (IReset component in instance.GetComponentsInChildren<IReset>()) { 
                component.InstanceReset();
            }
        }
    }
}

using System;
using UnityEngine;

[Serializable]
public class InterfaceReference<T> where T : class {
    [SerializeField] private GameObject _target;
    private T _value;

    public T Value {
        get {
            if (_value != null) {
                return _value;
            }
            if (_target == null) {
                return null;
            }
            _value = _target.GetComponent<T>();
            return _value;
        }
    }
}

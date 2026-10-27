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
                Debug.LogError("[InterfaceReference] " + typeof(T).Name + " 를 가진 오브젝트가 연결되지 않았습니다");
                return null;
            }
            _value = _target.GetComponent<T>();
            if (_value == null) {
                Debug.LogError("[InterfaceReference] " + _target.name + " 에 " + typeof(T).Name + " 컴포넌트가 없습니다");
            }
            return _value;
        }
    }
}

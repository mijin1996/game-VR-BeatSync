using UnityEngine;

public class SelectFeedback : MonoBehaviour {
    [SerializeField] private InterfaceReference<IListSelect> _selector;
    [SerializeField] private InterfaceReference<ISound> _sound;
    [SerializeField] private InterfaceReference<IEffect> _effect;
    [SerializeField] private Transform _title;

    void OnEnable() {
        if (_selector.Value != null) {
            _selector.Value.Changed += OnChanged;
        }
    }

    void OnDisable() {
        if (_selector.Value != null) {
            _selector.Value.Changed -= OnChanged;
        }
    }

    private void OnChanged() {
        _sound.Value.PlaySelect();
        _effect.Value.Pulse(_title);
    }
}

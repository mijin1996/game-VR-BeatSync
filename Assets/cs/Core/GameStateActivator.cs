using System;
using UnityEngine;

public class GameStateActivator : MonoBehaviour {
    [SerializeField] private InterfaceReference<IGameStateProvider> _stateSource;
    [SerializeField] private Entry[] _entries;

    void OnEnable() {
        IGameStateProvider source = _stateSource.Value;
        if (source == null) {
            return;
        }
        source.StateChanged += Apply;
        Apply(source.State);
    }

    void OnDisable() {
        if (_stateSource.Value != null) {
            _stateSource.Value.StateChanged -= Apply;
        }
    }

    private void Apply(GameState state) {
        foreach (Entry entry in _entries) {
            entry.Apply(state);
        }
    }

    [Serializable]
    private class Entry {
        [SerializeField] private GameObject _target;
        [SerializeField] private GameState _visibleIn;

        public void Apply(GameState state) {
            if (_target != null) {
                _target.SetActive((_visibleIn & state) != 0);
            }
        }
    }
}

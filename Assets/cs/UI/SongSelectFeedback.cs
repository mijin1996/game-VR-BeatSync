using UnityEngine;

public class SongSelectFeedback : MonoBehaviour {
    [SerializeField] private InterfaceReference<ISongSelect> _songSelector;
    [SerializeField] private InterfaceReference<ISound> _sound;
    [SerializeField] private InterfaceReference<IEffect> _effect;
    [SerializeField] private Transform _title;

    void OnEnable() {
        if (_songSelector.Value != null) {
            _songSelector.Value.SongSelected += OnSongSelected;
        }
    }

    void OnDisable() {
        if (_songSelector.Value != null) {
            _songSelector.Value.SongSelected -= OnSongSelected;
        }
    }

    private void OnSongSelected(SongData song) {
        _sound.Value.PlaySelect();
        _effect.Value.Pulse(_title);
    }
}

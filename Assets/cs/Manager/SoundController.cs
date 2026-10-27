using UnityEngine;

public class SoundController : MonoBehaviour {
    [SerializeField] private InterfaceReference<IGameStateProvider> _state;
    [SerializeField] private InterfaceReference<ISongSelect> _songSelector;
    [SerializeField] private InterfaceReference<ISound> _sound;

    void OnEnable() {
        if (_state.Value != null) {
            _state.Value.StateChanged += OnStateChanged;
        }
        if (_songSelector.Value != null) {
            _songSelector.Value.SongSelected += OnSongSelected;
        }
    }

    void OnDisable() {
        if (_state.Value != null) {
            _state.Value.StateChanged -= OnStateChanged;
        }
        if (_songSelector.Value != null) {
            _songSelector.Value.SongSelected -= OnSongSelected;
        }
    }

    private void OnStateChanged(GameState state) {
        if (state == GameState.Waiting) {
            _sound.Value.PlayWaitingBgm();
        } else {
            _sound.Value.StopBgm();
        }
    }

    private void OnSongSelected(SongData song) {
        // 대기 화면에서 노래를 고르면 그 노래를 미리듣기
        if (_state.Value.State == GameState.Waiting) {
            _sound.Value.PlayBgm(song.Clip);
        }
    }
}

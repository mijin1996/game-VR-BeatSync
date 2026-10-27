using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour {
    private const string BgmKey = "Volume.Bgm";
    private const string MusicKey = "Volume.Music";
    private const string SfxKey = "Volume.Sfx";

    [SerializeField] private InterfaceReference<ISound> _sound;
    [SerializeField] private InterfaceReference<IMusic> _music;
    [SerializeField] private InterfaceReference<IGameStateProvider> _state;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    void Awake() {
        if (_openButton != null) {
            _openButton.onClick.AddListener(Open);
        }
        if (_closeButton != null) {
            _closeButton.onClick.AddListener(Close);
        }
        if (_panel != null) {
            _panel.SetActive(false);
        }
    }

    void Start() {
        // 저장된 볼륨을 불러와 적용 (SoundManager / MusicPlayer 준비가 끝난 뒤)
        Bind(_bgmSlider, BgmKey, volume => _sound.Value.SetBgmVolume(volume));
        Bind(_musicSlider, MusicKey, volume => _music.Value.SetVolume(volume));
        Bind(_sfxSlider, SfxKey, volume => _sound.Value.SetSfxVolume(volume));
    }

    void OnEnable() {
        if (_state.Value != null) {
            _state.Value.StateChanged += OnStateChanged;
        }
    }

    void OnDisable() {
        if (_state.Value != null) {
            _state.Value.StateChanged -= OnStateChanged;
        }
    }

    public void Open() {
        if (_panel != null) {
            _panel.SetActive(true);
        }
    }

    public void Close() {
        if (_panel != null) {
            _panel.SetActive(false);
        }
        PlayerPrefs.Save();
    }

    private void OnStateChanged(GameState state) {
        // 게임이 시작되면 열려 있던 설정 패널은 닫음
        if (state != GameState.Waiting) {
            Close();
        }
    }

    private static void Bind(Slider slider, string key, Action<float> apply) {
        float volume = PlayerPrefs.GetFloat(key, 1f);
        apply(volume);
        if (slider == null) {
            return;
        }
        slider.SetValueWithoutNotify(volume);
        slider.onValueChanged.AddListener(value => {
            apply(value);
            PlayerPrefs.SetFloat(key, value);
        });
    }
}

using UnityEngine;

public class SoundManager : MonoBehaviour, ISound {
    [Header("배경음")]
    [SerializeField] private AudioSource _lobbyBgm;
    [SerializeField, Range(0f, 1f)] private float _bgmVolume = 1f;

    [Header("효과음")]
    [SerializeField] private AudioClip _clickSfx;
    [SerializeField] private AudioClip _selectSfx;
    [SerializeField] private AudioClip _cutSfx;
    [SerializeField] private AudioClip _goSfx;
    [SerializeField, Range(0f, 1f)] private float _sfxVolume = 1f;

    private AudioSource _bgmSource;
    private AudioSource _sfxSource;
    private float _lobbyBaseVolume = 1f;

    public float BgmVolume => _bgmVolume;
    public float SfxVolume => _sfxVolume;

    void Awake() {
        _bgmSource = CreateSource(true);
        _sfxSource = CreateSource(false);
        if (_lobbyBgm != null) {
            // 대기음은 인스펙터의 AudioSource 로 재생 (Volume 은 그 AudioSource 값 기준)
            _lobbyBgm.playOnAwake = false;
            _lobbyBgm.loop = true;
            _lobbyBaseVolume = _lobbyBgm.volume;
            _lobbyBgm.Stop();
        }
    }

    public void PlayWaitingBgm() {
        _bgmSource.Stop();
        if (_lobbyBgm != null && !_lobbyBgm.isPlaying) {
            _lobbyBgm.volume = _lobbyBaseVolume * _bgmVolume;
            _lobbyBgm.Play();
        }
    }

    public void PlayBgm(AudioClip clip) {
        StopLobbyBgm();
        if (clip == null) {
            StopBgm();
            return;
        }
        if (_bgmSource.isPlaying && _bgmSource.clip == clip) {
            return;
        }
        _bgmSource.clip = clip;
        _bgmSource.volume = _bgmVolume;
        _bgmSource.Play();
    }

    public void StopBgm() {
        _bgmSource.Stop();
        StopLobbyBgm();
    }

    public void PlayClick() {
        PlaySfx(_clickSfx);
    }

    public void PlaySelect() {
        PlaySfx(_selectSfx);
    }

    public void PlayCut() {
        PlaySfx(_cutSfx);
    }

    public void PlayGo() {
        PlaySfx(_goSfx);
    }

    public void SetBgmVolume(float volume) {
        _bgmVolume = Mathf.Clamp01(volume);
        if (_bgmSource != null) {
            _bgmSource.volume = _bgmVolume;
        }
        if (_lobbyBgm != null) {
            _lobbyBgm.volume = _lobbyBaseVolume * _bgmVolume;
        }
    }

    public void SetSfxVolume(float volume) {
        _sfxVolume = Mathf.Clamp01(volume);
    }

    private void StopLobbyBgm() {
        if (_lobbyBgm != null) {
            _lobbyBgm.Stop();
        }
    }

    private void PlaySfx(AudioClip clip) {
        if (clip != null) {
            _sfxSource.PlayOneShot(clip, _sfxVolume);
        }
    }

    private AudioSource CreateSource(bool loop) {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = loop;
        source.spatialBlend = 0f;
        return source;
    }
}

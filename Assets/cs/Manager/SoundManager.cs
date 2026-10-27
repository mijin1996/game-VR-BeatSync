using UnityEngine;

public class SoundManager : MonoBehaviour, ISound {
    [Header("배경음")]
    [SerializeField] private AudioClip _waitingBgm;
    [SerializeField, Range(0f, 1f)] private float _bgmVolume = 1f;

    [Header("효과음")]
    [SerializeField] private AudioClip _clickSfx;
    [SerializeField] private AudioClip _selectSfx;
    [SerializeField, Range(0f, 1f)] private float _sfxVolume = 1f;

    private AudioSource _bgmSource;
    private AudioSource _sfxSource;

    void Awake() {
        _bgmSource = CreateSource(true);
        _sfxSource = CreateSource(false);
    }

    public void PlayWaitingBgm() {
        PlayBgm(_waitingBgm);
    }

    public void PlayBgm(AudioClip clip) {
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
    }

    public void PlayClick() {
        PlaySfx(_clickSfx);
    }

    public void PlaySelect() {
        PlaySfx(_selectSfx);
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

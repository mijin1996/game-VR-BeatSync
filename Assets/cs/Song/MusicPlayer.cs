using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour, IMusicPlayer {
    [SerializeField] private float _scheduleDelay = 0.1f;

    private AudioSource _source;
    private double _songStartDsp;
    private bool _isSongMode;

    public float SongTime => _isSongMode ? (float)(AudioSettings.dspTime - _songStartDsp) : 0f;
    public bool IsSongFinished => _isSongMode && SongTime > 0f && !_source.isPlaying;

    void Awake() {
        _source = GetComponent<AudioSource>();
        _source.playOnAwake = false;
    }

    public void PlayLoop(AudioClip clip) {
        if (clip == null) {
            Stop();
            return;
        }
        if (!_isSongMode && _source.isPlaying && _source.clip == clip) {
            return;   // 이미 같은 곡 반복 재생 중
        }
        _isSongMode = false;
        _source.Stop();
        _source.clip = clip;
        _source.loop = true;
        _source.Play();
    }

    public void PlaySong(AudioClip clip) {
        _isSongMode = true;
        _source.Stop();
        _source.clip = clip;
        _source.loop = false;
        _songStartDsp = AudioSettings.dspTime + _scheduleDelay;
        _source.PlayScheduled(_songStartDsp);
    }

    public void Stop() {
        _isSongMode = false;
        _source.Stop();
    }
}

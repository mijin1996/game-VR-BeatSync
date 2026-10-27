using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour, IMusic {
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

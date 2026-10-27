using UnityEngine;

public interface IMusic {
    float SongTime { get; }
    bool IsSongFinished { get; }
    void PlaySong(AudioClip clip);
    void Stop();
    void SetVolume(float volume);
}

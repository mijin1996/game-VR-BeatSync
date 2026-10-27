using UnityEngine;

public interface IMusicPlayer {
    float SongTime { get; }
    bool IsSongFinished { get; }
    void PlayLoop(AudioClip clip);
    void PlaySong(AudioClip clip);
    void Stop();
}

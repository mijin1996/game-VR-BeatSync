using UnityEngine;

public interface ISound {
    float BgmVolume { get; }
    float SfxVolume { get; }
    void PlayWaitingBgm();
    void PlayBgm(AudioClip clip);
    void StopBgm();
    void PlayClick();
    void PlaySelect();
    void PlayCut();
    void SetBgmVolume(float volume);
    void SetSfxVolume(float volume);
}

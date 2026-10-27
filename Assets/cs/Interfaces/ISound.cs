using UnityEngine;

public interface ISound {
    void PlayWaitingBgm();
    void PlayBgm(AudioClip clip);
    void StopBgm();
    void PlayClick();
    void PlaySelect();
}

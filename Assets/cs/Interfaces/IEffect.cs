using UnityEngine;
using UnityEngine.UI;

public interface IEffect {
    void PlayClick(RectTransform target);
    void Blink(Graphic target);
    void Pulse(Transform target);
    void PlayHit(Vector3 position);
    void PlayGo(Transform at);
    void ShowGrade(HitGrade grade, Vector3 position);
}

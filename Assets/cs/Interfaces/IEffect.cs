using UnityEngine;
using UnityEngine.UI;

public interface IEffect {
    void PlayClick(RectTransform target);
    void Blink(Graphic target);
    void Pulse(Transform target);
}

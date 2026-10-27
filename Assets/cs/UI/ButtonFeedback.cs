using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonFeedback : MonoBehaviour {
    [SerializeField] private InterfaceReference<ISound> _sound;
    [SerializeField] private InterfaceReference<IEffect> _effect;

    void Awake() {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick() {
        _sound.Value.PlayClick();
        _effect.Value.PlayClick((RectTransform)transform);
    }
}

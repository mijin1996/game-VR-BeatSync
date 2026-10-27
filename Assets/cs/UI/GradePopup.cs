using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class GradePopup : MonoBehaviour {
    private TextMeshPro _text;
    private Color _color;
    private float _duration;
    private float _rise;
    private float _time;
    private Vector3 _startPos;

    public void Init(string word, Color color, float size, float duration, float rise) {
        _text = GetComponent<TextMeshPro>();
        _text.text = word;
        _text.color = color;
        _text.fontSize = size;
        _text.alignment = TextAlignmentOptions.Center;
        _text.textWrappingMode = TextWrappingModes.NoWrap;
        _color = color;
        _duration = Mathf.Max(0.01f, duration);
        _rise = rise;
        _startPos = transform.position;
        FaceCamera();
    }

    void Update() {
        _time += Time.deltaTime;
        float t = Mathf.Clamp01(_time / _duration);

        // 위로 살짝 떠오르고, 처음엔 톡 커졌다가, 뒤 절반 동안 투명해짐
        transform.position = _startPos + Vector3.up * (_rise * t);
        float scale = t < 0.15f ? Mathf.Lerp(0.6f, 1.1f, t / 0.15f) : Mathf.Lerp(1.1f, 1f, (t - 0.15f) / 0.85f);
        transform.localScale = Vector3.one * scale;
        Color color = _color;
        color.a = _color.a * (1f - Mathf.Clamp01((t - 0.5f) / 0.5f));
        _text.color = color;
        FaceCamera();

        if (_time >= _duration) {
            Destroy(gameObject);
        }
    }

    private void FaceCamera() {
        if (Camera.main != null) {
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        }
    }
}

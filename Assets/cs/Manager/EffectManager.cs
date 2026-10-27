using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour, IEffect {
    [Header("버튼 클릭")]
    [SerializeField] private Sprite _ringSprite;
    [SerializeField] private Color _ringColor = new Color(1f, 1f, 1f, 0.6f);
    [SerializeField] private float _ringScale = 1.4f;
    [SerializeField] private float _ringDuration = 0.35f;
    [SerializeField] private float _punchScale = 1.15f;
    [SerializeField] private float _punchDuration = 0.15f;

    [Header("깜빡임")]
    [SerializeField] private int _blinkCount = 2;
    [SerializeField] private float _blinkInterval = 0.08f;
    [SerializeField, Range(0f, 1f)] private float _blinkAlpha = 0.2f;

    [Header("커졌다 작아지기")]
    [SerializeField] private int _pulseCount = 2;
    [SerializeField] private float _pulseScale = 1.3f;
    [SerializeField] private float _pulseHalfDuration = 0.08f;

    private readonly Dictionary<Transform, Coroutine> _scaleRoutines = new Dictionary<Transform, Coroutine>();
    private readonly Dictionary<Transform, Vector3> _baseScales = new Dictionary<Transform, Vector3>();
    private readonly Dictionary<Graphic, Coroutine> _blinkRoutines = new Dictionary<Graphic, Coroutine>();
    private readonly Dictionary<Graphic, Color> _baseColors = new Dictionary<Graphic, Color>();

    public void PlayClick(RectTransform target) {
        if (target == null) {
            return;
        }
        SpawnRing(target);
        StartScale(target, _punchScale, 1, _punchDuration * 0.5f);
    }

    public void Pulse(Transform target) {
        StartScale(target, _pulseScale, _pulseCount, _pulseHalfDuration);
    }

    public void Blink(Graphic target) {
        if (target == null) {
            return;
        }
        Coroutine running;
        if (_blinkRoutines.TryGetValue(target, out running)) {
            StopCoroutine(running);
            target.color = _baseColors[target];
        } else {
            _baseColors[target] = target.color;
        }
        _blinkRoutines[target] = StartCoroutine(BlinkRoutine(target, _baseColors[target]));
    }

    private void StartScale(Transform target, float peak, int count, float half) {
        if (target == null) {
            return;
        }
        Coroutine running;
        if (_scaleRoutines.TryGetValue(target, out running)) {
            StopCoroutine(running);
            target.localScale = _baseScales[target];
        } else {
            _baseScales[target] = target.localScale;
        }
        _scaleRoutines[target] = StartCoroutine(ScaleRoutine(target, _baseScales[target], peak, count, half));
    }

    private IEnumerator ScaleRoutine(Transform target, Vector3 baseScale, float peak, int count, float half) {
        for (int i = 0; i < count; i++) {
            yield return ScaleStep(target, baseScale, baseScale * peak, half);
            yield return ScaleStep(target, baseScale * peak, baseScale, half);
        }
        if (target != null) {
            target.localScale = baseScale;
        }
        _scaleRoutines.Remove(target);
        _baseScales.Remove(target);
    }

    private static IEnumerator ScaleStep(Transform target, Vector3 from, Vector3 to, float duration) {
        float time = 0f;
        while (time < duration) {
            if (target == null) {
                yield break;
            }
            time += Time.deltaTime;
            target.localScale = Vector3.Lerp(from, to, Mathf.Clamp01(time / duration));
            yield return null;
        }
    }

    private IEnumerator BlinkRoutine(Graphic target, Color baseColor) {
        Color dim = baseColor;
        dim.a = baseColor.a * _blinkAlpha;
        for (int i = 0; i < _blinkCount; i++) {
            if (target == null) {
                break;
            }
            target.color = dim;
            yield return new WaitForSeconds(_blinkInterval);
            if (target == null) {
                break;
            }
            target.color = baseColor;
            yield return new WaitForSeconds(_blinkInterval);
        }
        if (target != null) {
            target.color = baseColor;
        }
        _blinkRoutines.Remove(target);
        _baseColors.Remove(target);
    }

    private void SpawnRing(RectTransform target) {
        Canvas canvas = target.GetComponentInParent<Canvas>(true);
        if (canvas == null) {
            return;
        }
        // 버튼이 바로 꺼져도(시작, RESTART, HOME) 보이도록 최상위 Canvas 에 만든다
        Transform root = canvas.rootCanvas.transform;
        GameObject ring = new GameObject("ClickRing", typeof(RectTransform), typeof(Image));
        ring.layer = target.gameObject.layer;

        RectTransform ringRect = (RectTransform)ring.transform;
        ringRect.SetParent(root, false);
        ringRect.position = target.TransformPoint(target.rect.center);
        ringRect.rotation = target.rotation;
        Vector3 ratio = new Vector3(target.lossyScale.x / root.lossyScale.x, target.lossyScale.y / root.lossyScale.y, 1f);
        ringRect.sizeDelta = new Vector2(target.rect.width * ratio.x, target.rect.height * ratio.y);
        ringRect.SetAsLastSibling();

        Image image = ring.GetComponent<Image>();
        image.sprite = _ringSprite;
        image.color = _ringColor;
        image.raycastTarget = false;

        StartCoroutine(RingRoutine(ringRect, image));
    }

    private IEnumerator RingRoutine(RectTransform ring, Image image) {
        float time = 0f;
        while (time < _ringDuration) {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / _ringDuration);
            ring.localScale = Vector3.one * Mathf.Lerp(1f, _ringScale, t);
            Color color = _ringColor;
            color.a = Mathf.Lerp(_ringColor.a, 0f, t);
            image.color = color;
            yield return null;
        }
        Destroy(ring.gameObject);
    }
}

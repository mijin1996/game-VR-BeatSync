using UnityEngine;
using UnityEngine.VFX;

public class RainbowTrailFollower : MonoBehaviour {
    [Header("따라갈 대상")]
    [SerializeField] private Transform saberTipTransform;          // 세이버 끝

    [Header("무지개 곡선")]
    [SerializeField] private Transform[] rainbowCurvePoints;       // 무지개 곡선의 점들 (A, B, C, D 순서)
    [SerializeField] private float pointFollowSpeed = 25f;         // 점이 앞 점을 따라가는 속도 (클수록 꼬리가 짧음)

    [Header("무지개 크기 (세이버에 맞게 작게)")]
    [SerializeField] private float glowSize = 0.12f;                               // 빛 번짐 크기 (에셋 원래 값 2)
    [SerializeField] private Vector2 particleSizeMinMax = new Vector2(0.01f, 0.025f); // 알갱이 크기 (에셋 원래 값 0.1~0.2)

    private const string GlowSizePropertyName = "Trailing Particle Glow Size";
    private const string ParticleSizePropertyName = "Trailing Particle Size";

    private VisualEffect rainbowVisualEffect;
    private bool wasSaberVisibleLastFrame;

    private void Awake() {
        rainbowVisualEffect = GetComponent<VisualEffect>();
        ApplyRainbowSize();
    }

    private void OnEnable() {
        MoveAllPointsToSaberTip();
        wasSaberVisibleLastFrame = IsSaberVisible();
        SetRainbowVisible(wasSaberVisibleLastFrame);
    }

    // Play 중에 Inspector에서 크기 값을 바꾸면 바로 반영
    private void OnValidate() {
        if (Application.isPlaying && rainbowVisualEffect != null) {
            ApplyRainbowSize();
        }
    }

    private void LateUpdate() {
        if (IsSetupMissing()) {
            return;
        }

        // 세이버가 숨겨지거나(대기화면) 다시 나타날 때 무지개도 같이 숨기거나 보여줌
        bool isSaberVisibleNow = IsSaberVisible();
        if (isSaberVisibleNow != wasSaberVisibleLastFrame) {
            if (isSaberVisibleNow) {
                MoveAllPointsToSaberTip();   // 다시 나타날 때 꼬리가 엉뚱한 곳에서 튀어나오지 않게
            }
            SetRainbowVisible(isSaberVisibleNow);
            wasSaberVisibleLastFrame = isSaberVisibleNow;
        }

        if (!isSaberVisibleNow) {
            return;
        }

        // 첫 번째 점(A)은 세이버 끝에 딱 붙임
        rainbowCurvePoints[0].position = saberTipTransform.position;

        // 이번 프레임에 앞 점 쪽으로 얼마나 다가갈지 비율 (0~1)
        float followRatioThisFrame = 1f - Mathf.Exp(-pointFollowSpeed * Time.deltaTime);

        // 나머지 점(B, C, D)은 바로 앞 점을 조금씩 늦게 따라감
        for (int pointIndex = 1; pointIndex < rainbowCurvePoints.Length; pointIndex++) {
            Transform currentPoint = rainbowCurvePoints[pointIndex];
            Transform pointInFront = rainbowCurvePoints[pointIndex - 1];

            currentPoint.position = Vector3.Lerp(
                currentPoint.position,
                pointInFront.position,
                followRatioThisFrame
            );
        }
    }

    // 리스폰이나 순간이동 시 호출하면 늘어진 꼬리를 정리합니다
    public void MoveAllPointsToSaberTip() {
        if (saberTipTransform == null || rainbowCurvePoints == null) {
            return;
        }

        foreach (Transform curvePoint in rainbowCurvePoints) {
            if (curvePoint != null) {
                curvePoint.position = saberTipTransform.position;
            }
        }
    }

    private bool IsSetupMissing() {
        return saberTipTransform == null
            || rainbowCurvePoints == null
            || rainbowCurvePoints.Length == 0;
    }

    private bool IsSaberVisible() {
        return saberTipTransform != null && saberTipTransform.gameObject.activeInHierarchy;
    }

    private void SetRainbowVisible(bool isVisible) {
        if (rainbowVisualEffect == null) {
            return;
        }

        rainbowVisualEffect.enabled = isVisible;
        if (isVisible) {
            rainbowVisualEffect.Reinit();   // 이전 파티클을 지우고 새로 시작
        }
    }

    private void ApplyRainbowSize() {
        if (rainbowVisualEffect == null) {
            return;
        }

        if (rainbowVisualEffect.HasFloat(GlowSizePropertyName)) {
            rainbowVisualEffect.SetFloat(GlowSizePropertyName, glowSize);
        }
        if (rainbowVisualEffect.HasVector2(ParticleSizePropertyName)) {
            rainbowVisualEffect.SetVector2(ParticleSizePropertyName, particleSizeMinMax);
        }
    }
}

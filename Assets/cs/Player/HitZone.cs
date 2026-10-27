using System;
using UnityEngine;

[Serializable]
public class HitZone {
    [SerializeField] private float _bladeLength = 2f;
    [SerializeField] private float _greatStart = 1f;
    [SerializeField] private float _marvelousStart = 1.2f;
    [SerializeField] private float _perfectStart = 1.5f;

    public float BladeLength => _bladeLength;

    public HitGrade GradeAt(float distanceFromHand) {
        // 손(0m) ~ 1.0m: MISS / ~1.2m: GREAT / ~1.5m: MARVELOUS / ~2.0m: PERFECT
        if (distanceFromHand < _greatStart) {
            return HitGrade.Miss;
        }
        if (distanceFromHand < _marvelousStart) {
            return HitGrade.Great;
        }
        if (distanceFromHand < _perfectStart) {
            return HitGrade.Marvelous;
        }
        return HitGrade.Perfect;
    }
}

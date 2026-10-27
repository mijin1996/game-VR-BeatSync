using TMPro;
using UnityEngine;

public class ScoreFeedback : MonoBehaviour {
    [SerializeField] private InterfaceReference<IScore> _score;
    [SerializeField] private InterfaceReference<IEffect> _effect;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _comboText;
    [SerializeField] private TMP_Text _missText;

    private int _lastScore;
    private int _lastCombo;
    private int _lastMiss;

    void OnEnable() {
        if (_score.Value == null) {
            return;
        }
        _score.Value.Changed += OnChanged;
        Remember();
    }

    void OnDisable() {
        if (_score.Value != null) {
            _score.Value.Changed -= OnChanged;
        }
    }

    private void OnChanged() {
        IScore score = _score.Value;
        // 값이 올라갈 때만 이펙트 (ResetScore 로 0 이 될 때는 안 함)
        if (score.Score > _lastScore) {
            _effect.Value.Blink(_scoreText);
        }
        if (score.Combo > _lastCombo) {
            _effect.Value.Pulse(_comboText != null ? _comboText.transform : null);
        }
        if (score.Miss > _lastMiss) {
            _effect.Value.Pulse(_missText != null ? _missText.transform : null);
        }
        Remember();
    }

    private void Remember() {
        _lastScore = _score.Value.Score;
        _lastCombo = _score.Value.Combo;
        _lastMiss = _score.Value.Miss;
    }
}

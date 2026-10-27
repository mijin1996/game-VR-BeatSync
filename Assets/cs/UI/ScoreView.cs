using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour {
    [SerializeField] private InterfaceReference<IScoreSystem> _score;
    [SerializeField] private TMP_Text _comboText;
    [SerializeField] private TMP_Text _missText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _levelText;

    void OnEnable() {
        if (_score.Value == null) {
            return;
        }
        _score.Value.Changed += Refresh;
        Refresh();
    }

    void OnDisable() {
        if (_score.Value != null) {
            _score.Value.Changed -= Refresh;
        }
    }

    private void Refresh() {
        IScoreSystem score = _score.Value;
        SetText(_comboText, "COMBO: " + score.Combo);
        SetText(_missText, "MISS: " + score.Miss);
        SetText(_scoreText, "SCORE: " + score.Score);
        SetText(_levelText, "LEVEL: " + score.Level);
    }

    private static void SetText(TMP_Text text, string value) {
        if (text != null) {
            text.text = value;
        }
    }
}

using TMPro;
using UnityEngine;

public class GameOverView : MonoBehaviour {
    [SerializeField] private InterfaceReference<IScore> _score;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _levelText;

    void OnEnable() {
        IScore score = _score.Value;
        if (score == null) {
            return;
        }
        SetText(_scoreText, "SCORE: " + score.Score);
        SetText(_levelText, "LEVEL: " + score.Level);
    }

    private static void SetText(TMP_Text text, string value) {
        if (text != null) {
            text.text = value;
        }
    }
}

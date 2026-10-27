using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour, IScore {
    [Header("점수 규칙 (GameRule.md)")]
    [SerializeField] private int _firstHitPoints = 5;
    [SerializeField] private int _comboHitPoints = 7;
    [SerializeField] private int _missPenalty = 5;
    [SerializeField] private int _firstLevelUpScore = 200;
    [SerializeField] private int _levelUpMultiplier = 2;
    [SerializeField] private int _gameOverScore = 0;

    private int _nextLevelUpScore;
    private bool _isGameOver;

    public int Score { get; private set; }
    public int Combo { get; private set; }
    public int Miss { get; private set; }
    public int Level { get; private set; } = 1;

    public event Action Changed;
    public event Action<int> LeveledUp;
    public event Action GameOver;

    void Awake() {
        ResetScore();
    }

    public void ResetScore() {
        Score = 0;
        Combo = 0;
        Miss = 0;
        Level = 1;
        _nextLevelUpScore = _firstLevelUpScore;
        _isGameOver = false;
        Changed?.Invoke();
    }

    public void Register(NoteResult result) {
        if (_isGameOver) {
            return;
        }

        if (result == NoteResult.Hit) {
            Combo++;
            // 첫 Hit(콤보 1) +5, 연속 Hit(콤보 2 이상) +7
            Score += Combo >= 2 ? _comboHitPoints : _firstHitPoints;
        } else {
            Combo = 0;
            Miss++;
            Score = Mathf.Max(0, Score - _missPenalty);   // 0점 아래로는 내려가지 않음
        }

        while (Score >= _nextLevelUpScore) {
            Level++;
            _nextLevelUpScore *= Mathf.Max(2, _levelUpMultiplier);
            LeveledUp?.Invoke(Level);
        }
        Changed?.Invoke();

        // Miss 로 점수가 0점이 되면 GAME OVER
        if (result == NoteResult.Miss && Score <= _gameOverScore) {
            _isGameOver = true;
            GameOver?.Invoke();
        }
    }
}

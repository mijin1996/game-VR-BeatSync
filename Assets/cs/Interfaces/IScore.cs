using System;

public interface IScore {
    int Score { get; }
    int Combo { get; }
    int Miss { get; }
    int Level { get; }
    int BestScore { get; }   // 이번 판에서 도달한 최고 점수
    event Action Changed;
    event Action<int> LeveledUp;
    event Action GameOver;
    void ResetScore();
    void Register(NoteResult result);
}

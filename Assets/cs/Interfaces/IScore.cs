using System;

public interface IScore {
    int Score { get; }
    int Combo { get; }
    int Miss { get; }
    int Level { get; }
    event Action Changed;
    event Action<int> LeveledUp;
    event Action GameOver;
    void ResetScore();
    void Register(NoteResult result);
}

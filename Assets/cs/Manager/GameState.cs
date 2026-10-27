using System;

[Flags]
public enum GameState {
    None = 0,
    Waiting = 1,
    Countdown = 2,
    Playing = 4,
    GameOver = 8,
    Result = 16
}

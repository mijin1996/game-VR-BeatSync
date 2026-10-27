using System;

public interface IGameStateProvider {
    GameState State { get; }
    event Action<GameState> StateChanged;
}

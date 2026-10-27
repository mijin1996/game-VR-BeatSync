using System;

public interface IDifficultySelect {
    DifficultyData Selected { get; }
    event Action<DifficultyData> DifficultySelected;
}

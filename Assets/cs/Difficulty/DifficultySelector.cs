using System;

public class DifficultySelector : ListSelector<DifficultyData>, IDifficultySelect {
    public event Action<DifficultyData> DifficultySelected;

    protected override string TitleOf(DifficultyData item) {
        return item.Title;
    }

    protected override void OnSelected(DifficultyData item) {
        DifficultySelected?.Invoke(item);
    }
}

using System;

public class SongSelector : ListSelector<SongData>, ISongSelect {
    public event Action<SongData> SongSelected;

    protected override string TitleOf(SongData item) {
        return item.Title;
    }

    protected override void OnSelected(SongData item) {
        SongSelected?.Invoke(item);
    }
}

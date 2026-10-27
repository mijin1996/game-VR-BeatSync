using System;

public interface ISongSelector {
    SongData Selected { get; }
    event Action<SongData> SongSelected;
}

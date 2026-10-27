using System;

public interface ISongSelect {
    SongData Selected { get; }
    event Action<SongData> SongSelected;
}

public readonly struct NoteData {
    public readonly float Time;
    public readonly int LineIndex;
    public readonly int LineLayer;
    public readonly NoteColor Color;
    public readonly CutDirection Direction;

    public NoteData(float time, int lineIndex, int lineLayer, NoteColor color, CutDirection direction) {
        Time = time;
        LineIndex = lineIndex;
        LineLayer = lineLayer;
        Color = color;
        Direction = direction;
    }
}

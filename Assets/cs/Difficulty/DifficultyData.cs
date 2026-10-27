using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "BeatSync/Difficulty Data")]
public class DifficultyData : ScriptableObject {
    [SerializeField] private string _title;
    [SerializeField] private BeatmapLevel _level = BeatmapLevel.Expert;
    [SerializeField] private float _noteSpeed = 5f;
    [SerializeField, Range(2, 4)] private int _laneCount = 4;

    public string Title => string.IsNullOrEmpty(_title) ? name : _title;
    public BeatmapLevel Level => _level;
    public float NoteSpeed => _noteSpeed;
    public int LaneCount => _laneCount;
}

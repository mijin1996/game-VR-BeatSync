using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SongData", menuName = "BeatSync/Song Data")]
public class SongData : ScriptableObject {
    [SerializeField] private string _title;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private TextAsset _normalBeatmap;
    [SerializeField] private TextAsset _hardBeatmap;
    [SerializeField, FormerlySerializedAs("_beatmap")] private TextAsset _expertBeatmap;
    [SerializeField] private float _bpm = 120f;

    public string Title => string.IsNullOrEmpty(_title) ? name : _title;
    public AudioClip Clip => _clip;
    public float Bpm => _bpm;

    public TextAsset BeatmapFor(BeatmapLevel level) {
        TextAsset beatmap = level == BeatmapLevel.Normal ? _normalBeatmap
                          : level == BeatmapLevel.Hard ? _hardBeatmap
                          : _expertBeatmap;
        // 해당 난이도 악보가 비어 있으면 Expert 악보로 대신함
        return beatmap != null ? beatmap : _expertBeatmap;
    }
}

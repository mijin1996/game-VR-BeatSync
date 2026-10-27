using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "BeatSync/Song Data")]
public class SongData : ScriptableObject {
    [SerializeField] private string _title;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private TextAsset _beatmap;
    [SerializeField] private float _bpm = 120f;

    public string Title => string.IsNullOrEmpty(_title) ? name : _title;
    public AudioClip Clip => _clip;
    public TextAsset Beatmap => _beatmap;
    public float Bpm => _bpm;
}

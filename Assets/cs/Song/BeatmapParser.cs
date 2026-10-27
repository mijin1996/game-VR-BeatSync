using System;
using System.Collections.Generic;
using UnityEngine;

public static class BeatmapParser {
    public static List<NoteData> Parse(TextAsset json, float bpm) {
        List<NoteData> result = new List<NoteData>();
        if (json == null) {
            Debug.LogError("[BeatmapParser] 비트맵(TextAsset)이 비어 있습니다");
            return result;
        }

        RawBeatmap raw = JsonUtility.FromJson<RawBeatmap>(json.text);
        float secondsPerBeat = 60f / bpm;
        foreach (RawNote note in raw.Notes) {
            if (note.IsColorNote) {   // 폭탄(3) 등은 아직 제외
                result.Add(note.ToNoteData(secondsPerBeat));
            }
        }
        result.Sort((a, b) => a.Time.CompareTo(b.Time));
        return result;
    }

    [Serializable]
    private class RawBeatmap {
        [SerializeField] private RawNote[] _notes;

        public RawNote[] Notes => _notes ?? Array.Empty<RawNote>();
    }

    [Serializable]
    private class RawNote {
        [SerializeField] private float _time;
        [SerializeField] private int _lineIndex;
        [SerializeField] private int _lineLayer;
        [SerializeField] private int _type;
        [SerializeField] private int _cutDirection;

        public bool IsColorNote => _type == (int)NoteColor.Red || _type == (int)NoteColor.Blue;

        public NoteData ToNoteData(float secondsPerBeat) {
            return new NoteData(_time * secondsPerBeat, _lineIndex, _lineLayer,
                                (NoteColor)_type, (CutDirection)_cutDirection);
        }
    }
}

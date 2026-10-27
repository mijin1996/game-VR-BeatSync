using System.Collections.Generic;
using UnityEngine;

public static class LaneMapper {
    public static List<NoteData> ToLanes(List<NoteData> notes, int laneCount) {
        if (laneCount >= 4) {
            return notes;
        }
        List<NoteData> result = new List<NoteData>();
        HashSet<string> used = new HashSet<string>();
        foreach (NoteData note in notes) {
            // 2줄: 왼쪽 칸(0, 1) → 1번 칸, 오른쪽 칸(2, 3) → 2번 칸
            int lane = note.LineIndex < 2 ? 1 : 2;
            // 같은 박자 · 같은 칸 · 같은 높이로 겹치는 노트는 하나만 남김
            string key = Mathf.RoundToInt(note.Time * 100f) + "_" + lane + "_" + note.LineLayer;
            if (!used.Add(key)) {
                continue;
            }
            result.Add(new NoteData(note.Time, lane, note.LineLayer, note.Color, note.Direction));
        }
        return result;
    }
}

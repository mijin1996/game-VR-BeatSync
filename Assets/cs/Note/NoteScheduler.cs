using System;
using System.Collections.Generic;

public class NoteScheduler {
    private readonly ISpawner _spawner;
    private readonly IReadOnlyList<NoteData> _notes;
    private readonly Action<NoteResult> _onJudged;
    private readonly float _travelTime;
    private readonly float _missDistance;
    private int _nextIndex;

    public bool IsDone => _nextIndex >= _notes.Count;

    public NoteScheduler(ISpawner spawner, IReadOnlyList<NoteData> notes,
                         float travelDistance, float missMargin, Action<NoteResult> onJudged) {
        _spawner = spawner;
        _notes = notes;
        _onJudged = onJudged;
        _travelTime = travelDistance / spawner.Speed;
        _missDistance = travelDistance + missMargin;
    }

    public void SkipUntil(float songTime) {
        while (!IsDone && SpawnTime(_notes[_nextIndex]) < songTime) {
            _nextIndex++;
        }
    }

    public void Tick(float songTime) {
        while (!IsDone) {
            NoteData note = _notes[_nextIndex];
            float spawnTime = SpawnTime(note);
            if (songTime < spawnTime) {
                break;
            }
            float advance = (songTime - spawnTime) * _spawner.Speed;   // 프레임 때문에 늦은 만큼 앞에서 생성
            IJudgeable spawned = _spawner.Spawn(note, _missDistance, advance);
            spawned.Judged += _onJudged;
            _nextIndex++;
        }
    }

    private float SpawnTime(NoteData note) {
        return note.Time - _travelTime;
    }
}

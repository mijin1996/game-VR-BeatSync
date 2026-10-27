using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NoteSpawner : MonoBehaviour, ISpawner {
    [SerializeField, FormerlySerializedAs("redNote")] private GameObject _redNote;
    [SerializeField, FormerlySerializedAs("blueNote")] private GameObject _blueNote;
    public float _noteSpeed = 10f;
    [SerializeField, FormerlySerializedAs("colSpacing")] private float _colSpacing = 0.6f;
    [SerializeField, FormerlySerializedAs("rowSpacing")] private float _rowSpacing = 0.5f;
    [SerializeField, FormerlySerializedAs("bottomY")] private float _bottomY = -0.39f;
    [SerializeField] private InterfaceReference<IEffect> _effect;

    private readonly List<Note> _aliveNotes = new List<Note>();

    public float Speed => _noteSpeed;

    public void SetSpeed(float speed) {
        _noteSpeed = speed;
    }

    public float DistanceTo(Vector3 point) {
        return Vector3.Dot(point - transform.position, transform.forward);
    }

    public IJudgeable Spawn(NoteData data, float missDistance, float advance) {
        GameObject prefab = data.Color == NoteColor.Red ? _redNote : _blueNote;

        // 이 오브젝트가 플레이어를 보고 있으므로, 플레이어 기준 오른쪽 = -transform.right
        Vector3 playerRight = -transform.right;
        Vector3 pos = transform.position
                    + playerRight * ((data.LineIndex - 1.5f) * _colSpacing)
                    + Vector3.up * (_bottomY + data.LineLayer * _rowSpacing)
                    + transform.forward * advance;

        GameObject obj = Instantiate(prefab, pos, transform.rotation);
        obj.transform.Rotate(0, 0, CutAngle(data.Direction), Space.Self);

        Note note = obj.GetComponent<Note>();
        note.Init(_noteSpeed, missDistance, advance);
        _aliveNotes.Add(note);
        note.Judged += result => {
            _aliveNotes.Remove(note);
            // 칼에 베인 노트(Hit) 자리에 이펙트
            if (result == NoteResult.Hit && _effect.Value != null) {
                _effect.Value.PlayHit(note.transform.position);
            }
        };
        note.Graded += grade => {
            // 판정 문구 (MARVELOUS / PERFECT / GREAT / MISS) 를 노트 자리에
            if (_effect.Value != null) {
                _effect.Value.ShowGrade(grade, note.transform.position);
            }
        };
        return note;
    }

    public void ClearAll() {
        foreach (Note note in _aliveNotes) {
            if (note != null) {
                Destroy(note.gameObject);
            }
        }
        _aliveNotes.Clear();
    }

    private static float CutAngle(CutDirection direction) {
        switch (direction) {
            case CutDirection.Up: return 180f;
            case CutDirection.Down: return 0f;
            case CutDirection.Left: return 90f;
            case CutDirection.Right: return -90f;
            case CutDirection.UpLeft: return 135f;
            case CutDirection.UpRight: return -135f;
            case CutDirection.DownLeft: return 45f;
            case CutDirection.DownRight: return -45f;
            default: return 0f;   // Any: 일단 아래로 베기
        }
    }
}

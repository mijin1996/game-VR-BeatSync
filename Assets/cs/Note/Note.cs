using System;
using UnityEngine;

public class Note : MonoBehaviour, ICuttable, IJudgeable {
    private float _speed = 2f;
    private float _missDistance = float.MaxValue;
    private float _traveled;
    private bool _isJudged;

    public event Action<NoteResult> Judged;

    public void Init(float speed, float missDistance, float startDistance) {
        _speed = speed;
        _missDistance = missDistance;
        _traveled = startDistance;
    }

    void Update() {
        //노트가 앞으로 이동하는 코드
        float step = Time.deltaTime * _speed;
        transform.position += transform.forward * step;
        _traveled += step;

        if (_traveled > _missDistance) {
            Judge(NoteResult.Miss);
        }
    }

    public void Cut() {
        Judge(NoteResult.Hit);
    }

    private void Judge(NoteResult result) {
        if (_isJudged) {
            return;
        }
        _isJudged = true;
        Judged?.Invoke(result);
        Destroy(gameObject);
    }
}

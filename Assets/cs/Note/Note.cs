using System;
using UnityEngine;

public class Note : MonoBehaviour, ICuttable, IJudgeable {
    private float _speed = 2f;
    private float _missDistance = float.MaxValue;
    private float _traveled;
    private bool _isJudged;

    public event Action<NoteResult> Judged;
    public event Action<HitGrade> Graded;

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
            Judge(HitGrade.Miss);
        }
    }

    public void Cut(HitGrade grade) {
        Judge(grade);
    }

    private void Judge(HitGrade grade) {
        if (_isJudged) {
            return;
        }
        _isJudged = true;
        Graded?.Invoke(grade);
        // 점수는 Hit / Miss 로만 계산 (MARVELOUS · PERFECT · GREAT 는 모두 Hit)
        Judged?.Invoke(grade == HitGrade.Miss ? NoteResult.Miss : NoteResult.Hit);
        Destroy(gameObject);
    }
}

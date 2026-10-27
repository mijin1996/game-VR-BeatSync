using UnityEngine;

public interface ISpawner {
    float Speed { get; }
    void SetSpeed(float speed);
    float DistanceTo(Vector3 point);
    IJudgeable Spawn(NoteData data, float missDistance, float advance);
    void ClearAll();
}

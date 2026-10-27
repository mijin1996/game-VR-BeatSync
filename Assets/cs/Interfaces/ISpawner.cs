using UnityEngine;

public interface ISpawner {
    float Speed { get; }
    float DistanceTo(Vector3 point);
    IJudgeable Spawn(NoteData data, float missDistance, float advance);
    void ClearAll();
}

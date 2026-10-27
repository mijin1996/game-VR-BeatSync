using System;

public interface IJudgeable {
    event Action<NoteResult> Judged;
}

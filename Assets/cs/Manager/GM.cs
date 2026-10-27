using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GM : MonoBehaviour, IGameStateProvider, IGameFlow {
    [Header("연결 (해당 컴포넌트가 붙은 오브젝트를 넣기)")]
    [SerializeField] private InterfaceReference<IMusic> _music;
    [SerializeField] private InterfaceReference<ISongSelect> _songSelector;
    [SerializeField] private InterfaceReference<IDifficultySelect> _difficultySelector;
    [SerializeField] private InterfaceReference<ISpawner> _spawner;
    [SerializeField] private InterfaceReference<IScore> _score;
    [SerializeField] private InterfaceReference<ICountdown> _countdown;
    [SerializeField, FormerlySerializedAs("player")] private Transform _player;

    [Header("타이밍")]
    [SerializeField] private float _timingOffset = 0f;
    [SerializeField] private float _missMargin = 1f;

    [Header("임시 (결과 패널 만들기 전까지)")]
    [SerializeField] private float _autoReturnDelay = 3f;

    private NoteScheduler _scheduler;
    private SongData _currentSong;

    public GameState State { get; private set; } = GameState.None;
    public event Action<GameState> StateChanged;

    private float SongTime => _music.Value.SongTime + _timingOffset;

    void OnEnable() {
        if (_score.Value != null) {
            _score.Value.GameOver += OnGameOver;
        }
    }

    void OnDisable() {
        if (_score.Value != null) {
            _score.Value.GameOver -= OnGameOver;
        }
    }

    void Start() {
        GoHome();
    }

    void Update() {
        if (State != GameState.Playing) {
            return;
        }

        _scheduler.Tick(SongTime);
        if (_scheduler.IsDone && _music.Value.IsSongFinished) {
            Finish(GameState.Result);
        }
    }

    public void StartGame() {
        if (State != GameState.Waiting) {
            return;
        }
        SongData song = _songSelector.Value.Selected;
        if (song == null) {
            return;
        }
        StartCoroutine(PlayRoutine(song));
    }

    public void Restart() {
        if (_currentSong == null) {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(PlayRoutine(_currentSong));
    }

    public void GoHome() {
        StopAllCoroutines();
        _spawner.Value.ClearAll();
        _music.Value.Stop();
        SetState(GameState.Waiting);
    }

    private IEnumerator PlayRoutine(SongData song) {
        _currentSong = song;
        _spawner.Value.ClearAll();
        _score.Value.ResetScore();

        // 난이도: 악보(Normal / Hard / Expert) + 노트 속도 + 줄 수
        DifficultyData difficulty = _difficultySelector.Value != null ? _difficultySelector.Value.Selected : null;
        BeatmapLevel level = difficulty != null ? difficulty.Level : BeatmapLevel.Expert;
        List<NoteData> notes = BeatmapParser.Parse(song.BeatmapFor(level), song.Bpm);
        if (difficulty != null) {
            _spawner.Value.SetSpeed(difficulty.NoteSpeed);
            notes = LaneMapper.ToLanes(notes, difficulty.LaneCount);
        }
        float travelDistance = _spawner.Value.DistanceTo(_player.position);
        _scheduler = new NoteScheduler(_spawner.Value, notes, travelDistance, _missMargin, _score.Value.Register);

        // 1) 노래 재생 → 2) 카운트다운 → 3) 노트 생성 (노트가 노래보다 먼저 나오지 않음)
        _music.Value.PlaySong(song.Clip);
        SetState(GameState.Countdown);
        yield return _countdown.Value.Run();

        _scheduler.SkipUntil(SongTime);   // 카운트다운 동안 지나간 노트는 건너뜀
        SetState(GameState.Playing);
    }

    private void OnGameOver() {
        if (State == GameState.Countdown || State == GameState.Playing) {
            Finish(GameState.GameOver);
        }
    }

    private void Finish(GameState endState) {
        StopAllCoroutines();
        _spawner.Value.ClearAll();
        if (endState == GameState.GameOver) {
            _music.Value.Stop();
        }
        SetState(endState);

        // 게임오버는 패널의 RESTART / HOME 버튼을 기다림. 결과 패널이 아직 없어서 노래 끝은 자동으로 대기로
        if (endState == GameState.Result && _autoReturnDelay > 0f) {
            StartCoroutine(ReturnHomeAfter(_autoReturnDelay));
        }
    }

    private IEnumerator ReturnHomeAfter(float delay) {
        yield return new WaitForSeconds(delay);
        GoHome();
    }

    private void SetState(GameState state) {
        State = state;
        StateChanged?.Invoke(state);
    }
}

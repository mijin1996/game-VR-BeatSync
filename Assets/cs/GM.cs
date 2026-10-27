using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

// Beat Saber(Beat Sage) v2 비트맵 JSON 구조
[System.Serializable]
public class BeatNote {
    public float _time;        // 박자(beat) 단위 시간
    public int _lineIndex;     // 가로 칸 0~3 (왼쪽 → 오른쪽)
    public int _lineLayer;     // 세로 줄 0~2 (아래 → 위)
    public int _type;          // 0 = 빨강(왼손), 1 = 파랑(오른손), 3 = 폭탄
    public int _cutDirection;  // 0위 1아래 2왼 3오 4왼위 5오위 6왼아래 7오아래 8아무방향
}

[System.Serializable]
public class BeatMap {
    public BeatNote[] _notes;
}

public class GM : MonoBehaviour {
    public static GM instance;

    [Header("곡 / 비트맵")]
    public AudioSource musicSource; // 노래 클립이 들어있는 AudioSource (Play On Awake 끔)
    public TextAsset beatmapJson;   // Beatmap/Expert.json 등
    public float bpm = 120f;        // Info.dat 의 _beatsPerMinute

    [Header("연결")]
    public Spawner spawner;
    public Transform player;        // Main Camera (노트가 도착할 위치)
    public TMP_Text countdownText;  // 3,2,1,GO 표시 (없어도 됨)
    public GameObject startButton;  // 게임 시작 버튼 (누르면 숨김, 끝나면 다시 보임)

    [Header("타이밍")]
    public float noteSpeed = 2f;      // Note.cs 이동 속도와 같게
    public float timingOffset = 0f;   // 소리보다 노트가 늦으면 +, 빠르면 -
    public bool autoStart = false;   // 체크하면 버튼 없이 바로 시작

    List<BeatNote> notes = new List<BeatNote>();
    int nextIndex = 0;
    double songStartDsp;   // 노래가 실제로 시작되는 오디오 시계 시각
    float travelTime;      // 노트가 스폰 지점 → 플레이어까지 걸리는 시간
    bool playing = false;  // 노트 생성 중
    bool started = false;  // 카운트다운 ~ 게임 끝까지 (중복 시작 방지)

    void Awake() {
        instance = this;
        if (musicSource == null) {
        }
    }

    void Start() {
        LoadBeatmap();
        if (autoStart) {
            StartGame();
        } else {
            ShowText("READY");   // 버튼 누를 때까지 대기
        }
    }

    void LoadBeatmap() {
        BeatMap map = JsonUtility.FromJson<BeatMap>(beatmapJson.text);
        notes.Clear();
        foreach (BeatNote n in map._notes) {
            if (n._type == 0 || n._type == 1) {   // 폭탄(3)은 일단 제외
                notes.Add(n);
            }
        }
        notes.Sort((a, b) => a._time.CompareTo(b._time));
    }

    // 시작 버튼 OnClick 에 연결
    public void StartGame() {
        if (started) {
            return;
        }
        started = true;
        if (startButton != null) {
            startButton.SetActive(false);
        }
        musicSource.Stop();
        StopAllCoroutines();
        StartCoroutine(CountdownAndPlay());
    }

    IEnumerator CountdownAndPlay() {
        playing = false;
        nextIndex = 0;

        string[] words = { "READY" };
        foreach (string w in words) {
            ShowText(w);
            yield return new WaitForSeconds(1f);
        }
        ShowText("GO!");

        // 노트가 플레이어까지 날아오는 시간 계산
        Vector3 toPlayer = player.position - spawner.transform.position;
        float distance = Vector3.Dot(toPlayer, spawner.transform.forward);
        travelTime = distance / noteSpeed;

        // GO 순간부터 노트 생성 시작.
        // 노래는 travelTime 뒤에 재생 → 첫 노트가 플레이어에게 도착하는 순간과 음악이 맞음
        songStartDsp = AudioSettings.dspTime + travelTime;
        musicSource.PlayScheduled(songStartDsp);
        playing = true;

        yield return new WaitForSeconds(1f);
        ShowText("");
    }

    void Update() {
        // 테스트용: 키보드 스페이스바로도 시작
        if (!started && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) {
            StartGame();
        }
        if (!playing) {
            return;
        }

        // 현재 노래 시간(초). 노래 시작 전에는 음수
        float songTime = (float)(AudioSettings.dspTime - songStartDsp) + timingOffset;

        while (nextIndex < notes.Count) {
            BeatNote n = notes[nextIndex];
            float hitTime = n._time * 60f / bpm;      // 박자 → 초
            float spawnTime = hitTime - travelTime;   // 이만큼 일찍 생성해야 제때 도착
            if (songTime < spawnTime) {
                break;
            }
            float late = songTime - spawnTime;        // 프레임 때문에 늦은 만큼 앞으로 보정
            spawner.SpawnNote(n._type, n._lineIndex, n._lineLayer, n._cutDirection, late * noteSpeed);
            nextIndex++;
        }

        // 모든 노트가 나오고 노래가 끝나면 종료
        if (nextIndex >= notes.Count && songTime > 0 && !musicSource.isPlaying) {
            playing = false;
            started = false;
            ShowText("CLEAR!");
            if (startButton != null) {
                startButton.SetActive(true);   // 다시 하기
            }
        }
    }

    void ShowText(string s) {
        if (countdownText != null) {
            countdownText.text = s;
        }
        if (s != "") {
            Debug.Log("[GM] " + s);
        }
    }
}

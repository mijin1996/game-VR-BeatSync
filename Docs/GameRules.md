# game-VR-BeatSync 게임 규칙 & 진행상황

---

## 1. 프로토콜 (게임 규칙)

# GameRule — 판정 / 점수 규칙

> 노트를 베거나 놓쳤을 때 COMBO, SCORE, MISS 가 어떻게 바뀌는지 정리한 문서입니다.
> 코드: `Assets/cs/Score/ScoreSystem.cs` (숫자는 GM 오브젝트의 ScoreSystem 인스펙터에서 조절)

## 1. 용어

| 용어 | 뜻 | 화면 표시 |
|---|---|---|
| **Hit** | 노트를 **한 번** 벤 것 (판정 결과) | 없음 |
| **Miss** | 노트를 **놓친** 것 (판정 결과) | `MISS: 놓친 총 개수` |
| **COMBO** | 끊기지 않고 **연속으로** 벤 개수 | `COMBO: 연속 개수` |
| **SCORE** | 판정 결과로 쌓이는 **점수** | `SCORE: 점수` |

## 2. 판정별 변화

| 상황 | COMBO | SCORE | MISS |
|---|---|---|---|
| **첫 Hit** (시작 직후, 또는 Miss 바로 다음에 벰 → COMBO 1) | 1 이 됨 | **+5** | 그대로 |
| **연속 Hit** (바로 앞 노트도 벴음 → COMBO 2 이상) | +1 | **+7** | 그대로 |
| **Miss** | **0 으로 초기화** | **-5** (0점 아래로는 안 내려감) | +1 |

## 3. 시작 / GAME OVER

| 기준 | 값 |
|---|---|
| 시작 SCORE | **0** |
| GAME OVER | **Miss 로 SCORE 가 0점이 되면** → 노래를 멈추고 GAME OVER 패널 (SCORE, LEVEL, RESTART, HOME) |

- 시작 직후 첫 노트를 놓치면 0점 → 바로 GAME OVER.
- Hit 로 점수를 쌓아 두면 그만큼 Miss 를 버틸 수 있다.

## 4. 진행 예시

| 순서 | 결과 | COMBO | SCORE 변화 | SCORE | MISS |
|---|---|---|---|---|---|
| 시작 | - | 0 | - | 0 | 0 |
| 1 | Hit | 1 | +5 (첫 Hit) | 5 | 0 |
| 2 | Hit | 2 | +7 (연속) | 12 | 0 |
| 3 | Hit | 3 | +7 (연속) | 19 | 0 |
| 4 | Miss | 0 | -5 | 14 | 1 |
| 5 | Hit | 1 | +5 (다시 첫 Hit) | 19 | 1 |
| 6 | Miss | 0 | -5 | 14 | 2 |
| 7 | Miss | 0 | -5 | 9 | 3 |
| 8 | Miss | 0 | -5 | 4 | 4 |
| 9 | Miss | 0 | -5 → 0 | **0** | 5 → **GAME OVER** |

## 5. 칼 위치 판정 (MARVELOUS / PERFECT / GREAT)

> 칼(Saber)의 길이는 **2m** (손 0m ~ 칼 끝 2m). 노트가 칼의 **어느 위치에 맞았는지**로 판정 문구를 정한다.
> 코드: `Saber.cs`(칼 전체 레이) + `HitZone.cs`(구간 판정) → `Note.cs` → `NoteSpawner` → `EffectManager.ShowGrade` → `GradePopup.cs`(문구 표시)

```
손(0m) ━━━━━━━━━ 1m ━━━━━━━━━ 1.2~1.5 ━━━━━━━━━ 2m(끝)
│◀─── MISS ───▶│◀─ GREAT ─▶│MARVELOUS│◀── PERFECT ──▶│
```

| 맞은 위치 (손에서 거리) | 판정 | 문구 |
|---|---|---|
| 0 ~ 1.0m (벨 수 없는 곳) | Miss | **MISS** (닿은 순간 바로) |
| 1.0 ~ 1.2m | Hit | **GREAT** |
| 1.2 ~ 1.5m | Hit | **MARVELOUS** |
| 1.5 ~ 2.0m (칼 끝 쪽) | Hit | **PERFECT** |
| 못 맞히고 지나감 | Miss | **MISS** |

- 문구는 노트가 있던 자리에 잠깐 떠올랐다가 사라진다.
- 구간 숫자(1.0 / 1.2 / 1.5 / 2.0m)는 **Saber-L / Saber-R → Hit Zone** 인스펙터에서 조절
- 문구 색 · 크기 · 시간은 **EffectManager → 판정 문구** 에서 조절
- 칼을 거의 움직이지 않고 대고만 있으면 판정하지 않음 (Saber 의 Min Swing Speed, 기본 0.5m/s)
- 결정할 것
  - [x] 판정마다 점수: 같음 (MARVELOUS / PERFECT / GREAT 모두 Hit — 첫 Hit +5, 연속 Hit +7)
  - [x] 벨 수 있는 곳에 맞았지만 화살표 반대로 벤 경우: 닿은 순간 바로 MISS

## 6. 레벨 (아직 정리 필요)
- 지금: SCORE 200 → 400 → 800 ... 에 닿을 때마다 LEVEL +1 (점수가 내려가도 레벨은 유지)
- 새 점수 규칙(+5 / +7)에서는 200점까지 약 28번 연속 Hit 가 필요 → 기준을 다시 정할 것


---

## 2. 진행상황

### 완료
- [x] 노트 프리팹 (빨강 / 파랑), Saber 로 노트 베기 (방향 판정 130도)
- [x] Beat Sage 비트맵 받기, `.json` 으로 변환 (`Beatmap(...)` 폴더)
- [x] GM: 비트맵 읽기 → 비트(BPM 120)에 맞춰 노트 생성
- [x] GM: 노래 예약 재생 (노트가 플레이어에 도착하는 순간 = 음악 타이밍)
- [x] GM: 시작 버튼 → 3,2,1,GO 카운트다운 (스페이스바 테스트 가능)
- [x] GM: AudioSource 하나만 사용 (`musicSource`)
- [x] Git 연결, `.gitignore` 설정

### 진행 중 / 문제
- [ ] 카운트다운 텍스트(cntText)가 꺼져 있어서 안 보임
      → 규칙(1-3)대로: 대기 때 꺼짐, 시작 버튼 누르면 켜짐, 카운트다운 끝나면 꺼짐
- [ ] 노트 생성 위치/방향: Spawner 오브젝트 위치·회전 확인 필요 (플레이어 정면에서 날아와야 함)
- [ ] VR 컨트롤러로 시작 버튼 누르기 (Canvas 거리/크기, Tracked Device Graphic Raycaster)
- [ ] 배경 360/180 영상 (Skybox Panoramic + Video Player + Render Texture)

### 할 일 (예정)
- [ ] 코드 구조 정리 (GM / BeatmapLoader / NoteScheduler / Spawner 분리)
- [ ] `BeatNote` 의 `_time` 등 public 변수명 정리 (`_` 는 private 전용 규칙)
- [ ] Combo / Miss 판정과 화면 표시
- [ ] 레벨 시스템 (규칙 결정 후)
- [ ] 결과 패널 (레벨, Combo, Miss + 다시 하기 / 노래 선택 버튼)
- [ ] 노래 선택 토글 목록 (노래 여러 개)
- [ ] 장애물(Obstacles) 생성
- [ ] (선택) 폭탄(Bombs)

---

## 3. 작업 규칙 (코드 작성 시)
- 요청하지 않은 수정은 하지 않는다. 설명만 요청하면 설명만 한다.
- 하이어라키/씬 수정이 필요하면 직접 하지 않고 **방법을 알려준다**.
- 중괄호는 K&R 스타일 (`{` 는 항상 같은 줄, 클래스·메서드 포함).
- `_이름` 은 **private 변수에만** 사용한다.
- 단일 책임 원칙(SRP) 을 기준으로 클래스를 나눈다.

# game-VR-BeatSync 게임 규칙 & 진행상황

> VR 비트세이버 스타일 리듬게임 (Unity 6.6 / XR Interaction Toolkit 3.x)
> 이 문서는 **게임의 방향(규칙)** 을 글로 명확히 하기 위한 기준 문서입니다.
> 기능을 추가·수정할 때는 이 문서를 먼저 확인하고, 바뀐 내용은 이 문서에도 반영합니다.
> 판정 / 점수 / GAME OVER 규칙은 **[GameRule.md](GameRule.md)** 에 따로 정리합니다.

---

## 1. 프로토콜 (게임 규칙)

### 1-1. 핵심 목표
- 노트는 **노래의 비트(비트맵)에 맞춰** 나온다. (랜덤 생성 X)
- 비트맵은 **Beat Sage** 에서 받은 파일을 사용한다.
  - `.dat` → `.json` 으로 확장자만 바꿔서 Unity TextAsset 으로 사용 (내용은 원본 그대로)
  - Beat Sage 옵션: Standard + 난이도 선택. (90 Degrees / Bombs / Obstacles / Dot Blocks 는 해당 기능을 만들기 전까지 끔)
  - 비트맵은 가로 4칸(`_lineIndex` 0~3) × 세로 3줄(`_lineLayer` 0~2). 칸 간격은 NoteSpawner 의 Col / Row Spacing
- 나중에 **장애물(Obstacles)** 도 비트맵에 맞춰 나오게 한다.

### 1-2. 게임 흐름 (모든 노래 공통 패턴)

```
[대기] 대기음 재생 / 시작 버튼 / 노래 선택 (< > 버튼, 넘길 때마다 그 노래 미리듣기)
       카운트다운 텍스트 꺼짐 / 레이 보임 / Saber 꺼짐
        ↓  시작 버튼 (테스트: 스페이스바)
[카운트다운] 시작 버튼·노래 선택 꺼짐 / 노래 재생 → READY → GO! / Saber 켜짐
        ↓  카운트다운 끝
[플레이] 비트에 맞춰 노트 생성 / 화면에 COMBO · SCORE · MISS 표시
        ├─ Miss 로 SCORE 가 0점 → [GAME OVER 패널] SCORE, LEVEL 표시
        │        ├─ [RESTART] → 같은 노래로 카운트다운부터 다시
        │        └─ [HOME]    → 대기 화면으로
        └─ 노래 끝 → [결과] (결과 패널은 아직 없음 → 3초 뒤 자동으로 대기)
```

### 1-3. 상태별 규칙

| 상태 | 시작 버튼 · 노래 선택 | 카운트다운 텍스트 | COMBO · SCORE · MISS | Saber | 레이 | 노트 | 노래 | GAME OVER 패널 |
|---|---|---|---|---|---|---|---|---|
| 대기 | **보임** | 꺼짐 | 꺼짐 | 꺼짐 | **보임** | 없음 | 대기음 / 미리듣기 | 꺼짐 |
| 카운트다운 | 꺼짐 | **켜짐** (READY, GO!) | 보임 | **켜짐** | 꺼짐 | 없음 | 재생 | 꺼짐 |
| 플레이 | 꺼짐 | 꺼짐 | **보임** | **켜짐** | 꺼짐 | **비트에 맞춰 생성** | 재생 | 꺼짐 |
| GAME OVER | 꺼짐 | 꺼짐 | 꺼짐 | 꺼짐 | **보임** | 정리 | 정지 | **보임** |
| 결과 | 꺼짐 | 꺼짐 | 꺼짐 | 꺼짐 | **보임** | 정리 | - | (아직 없음) |

- 상태별 켜고 끄기는 GM 오브젝트의 `GameStateActivator` 목록에서 설정한다.
- 노래 AudioSource 는 **Play On Awake 끔**, GM 이 MusicPlayer 를 통해 직접 재생한다.
- 노래 / 비트맵이 바뀌어도 흐름은 **항상 같다**.

### 1-4. 플레이 중 표시
- **COMBO**: 연속으로 벤 노트 수 (Miss 면 0)
- **SCORE**: 점수 (첫 Hit +5, 연속 Hit +7, Miss -5) → [GameRule.md](GameRule.md)
- **MISS**: 놓친 노트 수 (못 베고 플레이어를 지나간 노트)
- **레벨**: 규칙 정리 필요 (아래 "결정할 것")

### 1-5. GAME OVER 패널 / 결과 패널
- **GAME OVER 패널**: SCORE, LEVEL 표시 + **RESTART**(같은 노래 다시) / **HOME**(대기 화면)
- **결과 패널** (노래 끝, 아직 없음): 레벨, Combo, Miss + 다시 하기 / 노래 선택

### 1-6. 노래 선택
- 노래 목록은 **< > 버튼** 으로 넘기며 선택한다. 넘길 때마다 그 노래가 미리듣기로 재생된다. (마지막 곡에서 > 는 첫 곡으로)
- 노래 목록은 SongSelector 의 **Songs 배열** 에 SongData 를 넣어 관리한다. (노래마다 오브젝트를 만들지 않음)
- 노래 하나 = **AudioClip + 비트맵(json, Normal / Hard / Expert) + BPM** 묶음 (`Assets/Songs`, Create → BeatSync → Song Data)
- 현재 3곡: Sad Piano, Club Change Up, Upbeat Funk (모두 Normal / Hard / Expert 악보, BPM 120)

### 1-6-1. 난이도 / 설정
- 난이도는 노래 선택 아래 **< Normal >** 줄에서 고른다 (Normal → Hard → Expert). 노래 선택과 같은 방식: 선택음 + 이름 Pulse
- 난이도마다 **Beat Sage 가 만든 그 난이도의 악보**를 쓴다 → 노트 수가 달라도 박자는 노래와 맞음 (노트를 임의로 빼지 않음)

| 난이도 | 악보 | 노트 줄 | 노트 속도 |
|---|---|---|---|
| Normal | Normal.json | **2줄** (악보 0·1칸 → 왼쪽, 2·3칸 → 오른쪽, 겹치는 노트는 하나만) | 5 |
| Hard | Hard.json | 2줄 | 6 |
| Expert | Expert.json | 2줄 | 8 |

- 값은 `Assets/Difficulty/Normal.asset`, `Hard.asset`, `Expert.asset` 인스펙터에서 조절 (게임 시작 때 NoteSpawner 속도를 덮어씀)
- 곡(SongData)마다 Normal / Hard / Expert 악보 칸이 있음. 비어 있으면 Expert 악보로 대신함
- **SETTINGS** 버튼(대기 화면) → 설정 패널: BGM(대기음·미리듣기) / MUSIC(게임 노래) / SFX(효과음) 볼륨, CLOSE. 값은 저장되어 다시 켜도 유지

### 1-7. 결정할 것 (아직 정해지지 않음)
- [ ] **레벨**이 무엇인지 / 기준 점수 (지금 200 → 400 → 800 은 새 점수 규칙에서 너무 높음)
- [ ] **레벨이 오를 때** 패널이 뜨면 게임이 멈추는지(일시정지 후 재개), 노래 끝에 한 번만 뜨는지
- [ ] "재개" 버튼이 필요한지 (일시정지 → 이어하기)
- [ ] Miss 판정 기준 (지금: 노트가 플레이어를 1m 지나가면 Miss — GM 의 Miss Margin)
- [ ] 장애물을 피하지 못했을 때 벌점 (Miss 증가? Combo 끊김? 점수 감소?)
- [ ] 폭탄(Bombs) 사용 여부
- [ ] 빨강=왼손 / 파랑=오른손 칼만 벨 수 있게 할지 (지금은 어느 칼로 베도 잘림)

---

## 2. 진행상황

### 완료 (Unity 에서 테스트 확인)
- [x] 노트 프리팹 (빨강 / 파랑), Saber 로 노트 베기 (방향 판정 130도)
- [x] Beat Sage 비트맵 받기, `.json` 으로 변환
- [x] Git 연결, `.gitignore` 설정
- [x] 흐름: 대기(대기음) → 시작 → 노래 재생 → READY, GO → 노트 생성 → 게임오버 / 결과 → 대기 (`GM`)
- [x] 상태별 오브젝트 켜고 끄기: 시작 버튼, 노래 목록, 카운트다운, COMBO/SCORE/MISS, Saber-L/R, 레이, GAME OVER 패널 (`GameStateActivator`)
- [x] 노래 선택: < > 버튼 + Songs 배열, 넘길 때마다 미리듣기 (`SongData`, `SongSelector`)
- [x] 세 번째 곡 Upbeat Funk 추가 (`Song_UpbeatFunk`, Expert.dat → Expert.json)
- [x] 대기음 / 미리듣기 / 게임 노래 재생 (`MusicPlayer`)
- [x] Hit / Miss 판정 (`Note`, `Saber` → `ICuttable`)
- [x] 점수: 첫 Hit +5, 연속 Hit +7, Miss -5, 0점이면 GAME OVER (`ScoreSystem`, [GameRule.md](GameRule.md))
- [x] COMBO / SCORE / MISS 텍스트 표시 (`ScoreView`)
- [x] 카운트다운 READY → GO! (`CountdownView`, 오브젝트 `coundDown` 재연결·크기 수정)
- [x] GAME OVER 패널: SCORE / LEVEL + RESTART / HOME (`GameOverView`)
- [x] 버튼 연결 분리: START / RESTART / HOME (+ 테스트용 스페이스바) → `GameButtons`, GM 은 `IGameFlow` 로 요청만 받음
- [x] 이름 정리: Spawner → `NoteSpawner` (노트 속도 = NoteSpawner 의 Note Speed)
- [x] 코드 스타일 K&R 통일, 클래스 위 / 필드 옆 주석 정리
- [x] 난이도: `DifficultyData`(Normal / Hard / Expert 에셋, 난이도별 Beat Sage 악보 `BeatmapLevel`), `DifficultySelector`, `LaneMapper`(4줄 → 2줄). 노래·난이도 < > 공통 부모 `ListSelector`, 공통 선택 피드백 `SelectFeedback`
- [x] 설정 패널: `SettingsView` (볼륨 3개 + 저장), `ISound` / `IMusic` 에 볼륨 조절 추가
- [x] 폴더 정리: `Manager` 폴더 (GM, GameState, GameStateActivator, InterfaceReference, SoundManager, SoundController, EffectManager), 안 쓰는 `SongToggle.cs` 삭제
- [x] 사운드: `SoundManager` : `ISound` (대기음·미리듣기 BGM + 효과음), `SoundController` (상태에 따라 대기음 / 미리듣기 / 정지). `MusicPlayer` 는 게임 노래(타이밍)만 담당
- [x] 이펙트: `EffectManager` : `IEffect` (버튼 클릭 링 + 눌림, 깜빡임, 커졌다 작아지기 2번)
- [x] 피드백 연결: `ButtonFeedback` (시작·< >·RESTART·HOME 클릭음 + 이펙트), `SongSelectFeedback` (노래 선택음 + 제목 이펙트), `ScoreFeedback` (SCORE 오르면 깜빡, COMBO·MISS 늘면 커졌다 작아지기 2번)

### 진행 중 / 문제
- [ ] 노트가 한꺼번에 몰려 보임 → NoteSpawner 의 Note Speed 조절 (지금 2, 6~8 추천) 또는 비트맵 난이도를 Normal / Hard 로
- [ ] 빨간 노트 방향 표시 점 안 보임 → `redNote` 프리팹의 `RedNoteUpPoint` Scale 이 0 (파랑은 0.2)
- [ ] VR 컨트롤러로 < > / 버튼 누르기 헤드셋 테스트 (레이 거리 15m 로 늘림)
- [ ] 배경 360/180 영상 (Skybox Panoramic + Video Player + Render Texture)

### 할 일 (예정)
- [ ] 레벨 규칙 확정 + 레벨업 패널
- [ ] 결과 패널 (노래 끝)
- [ ] 장애물(Obstacles) 생성
- [ ] (선택) 폭탄(Bombs) — `ICuttable` 로 추가 가능
- [ ] 대기음 파일 정하기 → SoundManager 의 Waiting Bgm 칸 (지금 비어 있어서 대기 화면은 무음)
- [ ] (선택) 노트 벨 때 / Miss 때 효과음

### 참고 (설정값)
- VR 레이 거리(Curve Interaction Caster 의 Cast Distance) 15m — Canvas 가 카메라 10m 앞(LazyFollow)이라 양옆 버튼까지 닿게 하려고 늘림
- `Assets/VRTemplateAssets/Fonts/Inter/.gitattributes` 때문에 GitHub Desktop 이 Git LFS 초기화를 물어봄 → **Not now** (이 프로젝트는 LFS 안 씀)

---

## 3. 작업 규칙 (코드 작성 시)
- 요청하지 않은 수정은 하지 않는다. 설명만 요청하면 설명만 한다.
- 하이어라키/씬 수정이 필요하면 직접 하지 않고 **방법을 알려준다**.
- 중괄호는 K&R 스타일 (`{` 는 항상 같은 줄, 클래스·메서드 포함).
- `_이름` 은 **private 변수에만** 사용한다.
- 단일 책임 원칙(SRP) 을 기준으로 클래스를 나눈다.

# 🎵 VR BeatSync (game-VR-BeatSync)

> 한 줄 소개: (직접 작성)
> 예) 노래 박자에 맞춰 날아오는 노트를 VR 칼로 베는 비트세이버 스타일 리듬 게임

---

## 📜 개발 과정 히스토리

### Phase 1 — 기초 뼈대 (교수님 수업 · `VR-BeatSync-초기스크립트/`)
수업에서 짠 초기 스크립트 3개(`Note`, `Saber`, `Spawner`)로 **"노트가 날아오고 칼로 벤다"**는 핵심 루프를 만들었다.

| 사용한 개념 | 적용된 곳 |
|---|---|
| `transform.forward` 이동 | 노트가 앞으로 날아옴 |
| Raycast + `LayerMask` | 칼 앞쪽으로 레이를 쏴 노트만 감지 |
| 벡터 각도(`Vector3.Angle`) | 칼을 휘두른 방향과 노트 방향이 130도 넘게 차이 나면 자르기 |
| 타이머 박자 생성 (`timer -= beat`) | 한 박자마다 랜덤 노트·위치·회전으로 생성 |

### Phase 2 — 기능 확장 · 리팩토링 (ME)

**① 객체지향 설계 · 파일 구조화**
- 인터페이스 13개로 역할을 나눔: `ISpawner`, `IScore`, `IMusic`, `IJudgeable`, `ICuttable`, `IGameFlow`, `ISound`, `IEffect` …
- 폴더 정리: `Manager` / `Note` / `Player` / `Score` / `Song` / `Difficulty` / `UI` / `Interfaces`

**② 리듬 시스템 (랜덤 → 노래 박자)**
- 노래 + 악보(비트맵)를 `BeatmapParser`가 BPM 기준으로 읽고, `NoteScheduler`가 시간에 맞춰 노트 생성
- 노래 재생 → 카운트다운(GO!) → 노트 생성 순서 보장

**③ 판정 · 점수**
- 칼에 맞은 위치로 **MARVELOUS / PERFECT / GREAT** 판정, 방향이 틀리면 MISS, 칼을 대고만 있으면 판정 안 함
- `ScoreSystem`: 첫 Hit +5, 연속 Hit +7, Miss -4 / 0점이 되거나 연속 3 Miss면 GAME OVER (자세한 규칙: `Docs/GameRules.md`)

**④ 곡 · 난이도 선택**
- 곡 선택(`<` `>`), 난이도 Normal / Hard / Expert (악보 + 노트 속도 + 줄 수)

**⑤ 연출 · UI**
- 레인보우 칼 트레일, 노트 화살표, 노트 맞는 소리, 180도 배경 영상
- 게임오버 패널(RESTART / HOME), 설정 패드, 버튼 클릭 소리·이펙트, 판정 팝업

#### 💭 고민한 점 · 배운 점
- (직접 작성)
- (직접 작성)

---

## 🛠 기술 스택
- **Language:** C#
- **Engine:** Unity 6 (6000.6.3f1)
- **그 외:** XR Interaction Toolkit, URP

---

## 📁 문서
- [`Docs/Proposal/Proposal.md`](Docs/Proposal/Proposal.md) — 기획서 세트(컨셉 · 시스템 · 전투 · 레벨 · 밸런스) · 시뮬레이션
- [`Docs/GameRules.md`](Docs/GameRules.md) — 판정·점수 규칙 & 진행상황

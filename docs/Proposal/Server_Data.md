# 서버 · 데이터 연동 — VR BeatSync

> 문서 세트: [목차](Proposal.md) · 기획: [컨셉](Concept.md) · [시스템](System.md) · [전투·개체](Combat_Entity.md) · [레벨 디자인](LevelDesign.md) · [밸런스](Balance.md) · 사업·운영: [사업 검토](Business.md) · [요구사항·로드맵](Requirements.md) · [BM·경제](Monetization.md) · **서버·데이터** · [리소스](Resources.md) · [로그·지표](Analytics.md) · [QA·출시](QA_Release.md) · [라이브 운영](LiveOps.md) · [사업화·법무](Business_Legal.md) · [needFix](needFix.md)

> **문서 정보** · ID `D08-SERVER` · v1.0 · 상태 `Draft` · Owner ME (기획 · 개발) · 최종 수정 2026-11-21 · 관련 `D03-SYSTEM`, `D07-BM`

**변경 이력**

| 버전 | 날짜 | 변경 내용 | 사유 | 결정자 | 영향 범위 |
|---|---|---|---|---|---|
| v1.0 | 2026-11-21 | 최초 작성 | 사내 문서 기준 v3 적용 | ME | 문서만 |

---

## 1. 이 문서의 역할
**어떤 데이터를 어디에 저장하고, 무엇을 서버가 판정할지** 정하는 문서예요.

## 2. 지금 저장하는 것 (코드 기준)
**아무것도 저장하지 않아요.** 곡 · 난이도 데이터는 ScriptableObject(`SongsData` · `DifficultyData`)와 비트맵 JSON으로 빌드에 들어 있어요.

## 3. 저장 위치 (제안)
| 데이터 | 저장 위치 | 이유 |
|---|---|---|
| 곡별 · 난이도별 최고 점수 · 랭크 | 기기 | 혼자 보는 기록 |
| 해금 상태 (다음 난이도) | 기기 | |
| 박자 보정값 (`_timingOffset`) | 기기 | 기기마다 다름 |
| 플레이 설정 (앉아서 모드 · 키 높이 · 칼 길이) | 기기 | |
| 곡 팩 구매 | 스토어 구매 기록 | 재설치 시 복원 |
| 랭킹 (보류) | 서버 또는 스토어 리더보드 | 위조 방지 |

**저장 형식 (제안)** — 데이터 원본 JSON 규칙과 맞춰요.
```json
{
  "version": 1,
  "timing_offset_ms": -35,
  "seated_mode": false,
  "records": [
    { "song_id": "SONG_001", "difficulty": "Hard", "best_score": 1820, "best_rank": "A", "cleared": true }
  ]
}
```
> `Application.persistentDataPath`에 파일 하나로 저장해요. 키가 여러 개인 `PlayerPrefs`보다 곡이 늘어날 때 관리가 쉬워요. 불러오기 실패 시 기본값으로 시작하고 원본은 `.bak`으로 남겨요.

## 4. 곡 데이터 연동
| 항목 | 내용 |
|---|---|
| 지금 | Beat Sage가 만든 `.dat`를 `.json`으로 이름만 바꿔 사용 (`Sounds/BeatSaga/`) |
| 곡 ID | `SONG_001`처럼 ID를 정하고 폴더 · ScriptableObject · 기록에서 같은 ID 사용 → [F-05](needFix.md#8-사내-문서-기준-v3-적용-후-정할-것) |
| 곡 팩 | 빌드에 넣는 방식(업데이트) vs 내려받는 방식(Addressables) 결정 |

# 용어집 — VR BeatSync

> 문서 세트: [목차](common_Index.md) · 공통 관리: **용어집** · [결정 로그](common_Decisions.md) · [needFix](common_needFix.md)

> **문서 정보** · 문서 번호 `D00-TERM` · 버전 v1.0 · 상태 `Review` · 담당 기획자 · 최종 수정 2026-11-21 · 관련 문서 [목차](common_Index.md)

**변경 이력**

| 버전 | 날짜 | 변경 내용 | 사유 | 결정자 | 영향 범위 |
|---|---|---|---|---|---|
| v1.0 | 2026-11-21 | 최초 작성 | 기업용 기획 가이드의 공통 관리 문서 | 기획자 | 문서만 |

---

## 1. 목적
문서 · 화면 · 코드 · 로그에서 같은 것을 같은 이름으로 부르기 위한 목록이에요. 새 용어는 여기에 먼저 적고 문서에 써요.

## 2. 용어
| 용어 | 뜻 | 코드 · 데이터 이름 | 관련 문서 |
|---|---|---|---|
| 노트 | 박자에 맞춰 날아오는 베어야 할 블록 (화살표 방향 있음) | `Note` | [전투·개체](planning_Combat_Entity.md) |
| 칼 | 양손 컨트롤러에 붙은 2m 칼 | `Saber` · `HitZone` | [전투·개체](planning_Combat_Entity.md) §2 |
| 판정 | 칼의 어느 구간에 맞았는지로 정하는 PERFECT · MARVELOUS · GREAT · MISS | `HitZone` | [전투·개체](planning_Combat_Entity.md) §2 |
| 방향 판정 | 휘두른 방향이 노트 화살표와 맞는지 (130도 초과) | — | [전투·개체](planning_Combat_Entity.md) §2 |
| 비트맵 | 곡마다 노트가 나오는 시간 · 위치를 적은 데이터 | — | [레벨 디자인](planning_LevelDesign.md) |
| 스포너 | 비트맵을 읽어 노트를 내보내는 장치 | `Spawner` | [시스템](planning_System.md) |
| 박자 지연 보정 | 기기마다 다른 소리 · 화면 지연을 맞추는 값 | — | [QA·출시](qa_QA_Release.md) §4 |

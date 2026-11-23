# 라이브 운영 — VR BeatSync

> 문서 세트: [목차](common_Index.md) · 기획: [컨셉](planner_Concept.md) · [시스템](planner_System.md) · [전투·개체](planner_Combat_Entity.md) · [레벨 디자인](designer_LevelDesign.md) · [밸런스](planner_Balance.md) · 사업·운영: [사업 검토](owner_Business.md) · [요구사항·로드맵](planner_Requirements.md) · [Business Model](planner_Monetization.md) · [서버·데이터](developer_Server_Data.md) · [리소스](designer_Resources.md) · [로그·지표](planner_Analytics.md) · [QA·출시](tester_Release.md) · **라이브 운영** · [사업화·법무](owner_Legal.md) · [needFix](common_needFix.md)

> **문서 정보**

| 문서 번호 | 버전 | 상태 | 책임자 | 협업 | 최종 수정 | 관련 문서 |
|---|---|---|---|---|---|---|
| `D12-LIVE` | v1.1 | `Draft` | 운영 매니저 | 기획자 | 2026-11-23 | [로그·지표](planner_Analytics.md) |

**변경 이력**

| 버전 | 날짜 | 변경 내용 | 사유 | 결정자 | 영향 범위 |
|---|---|---|---|---|---|

---

## 1. 목적
출시 후 **게임을 어떻게 유지하고 키울지** 정하는 문서임. 유료 VR 리듬 게임 · 1인 운영 기준임.

## 2. 업데이트 주기 (제안)
| 종류 | 주기 | 내용 |
|---|---|---|
| 핫픽스 | 필요할 때 | 크래시 · 박자 오류 |
| 패치 | 4주 | 악보 조정 · 판정 · 편의 옵션 |
| 콘텐츠 | 지표 · 매출 보고 | 무료 곡 1곡 (재방문) → 곡 팩 DLC |

## 3. 지표 리뷰 (매주)
| 지표 | 이상 기준 | 대응 |
|---|---|---|
| 첫 곡 완주율 | 60% 미만 | 첫 곡 Normal 악보 · 체력 게이지 완화 |
| 곡별 완주율 차이 | 같은 난이도에서 20%p 이상 | 해당 곡 악보 조정 |
| 환불률 | 10% 초과 | 리뷰 · 환불 사유 확인 (멀미 · 판정 · 분량) |
| `miss_handle` 비율 | 높아짐 | 칼끝 판정 범위 조정 |

## 4. 곡 추가 절차
| 단계 | 할 일 | 확인 |
|---|---|---|
| 1 | 곡 확보 | [리소스](designer_Resources.md) §2 라이선스 표에 추가 · 원문 저장 |
| 2 | 악보 3난이도 | 초당 노트 수 기준 ([밸런스](planner_Balance.md)) |
| 3 | 곡 ID · 데이터 | `SONG_###`, ScriptableObject · 비트맵 JSON |
| 4 | 테스트 | 실제 헤드셋 [TC_008](tester_Release.md) · [TC_009](tester_Release.md) |
| 5 | 크레딧 | 작곡가 표기 |

## 5. 패치노트 양식
```
[v1.0.2] 2026-00-00
- 고쳤어요: 헤드셋을 벗었다 쓰면 박자가 밀리던 문제
- 바꿨어요: SONG_002 Hard 후반 노트 12개 줄임
```

## 6. CS 처리
| 문의 | 처리 | 기한 |
|---|---|---|
| 박자가 안 맞음 | 박자 보정 화면 안내 ([REQ_107](planner_Requirements.md)) | 24시간 |
| 어지러움 | 앉아서 모드 · 휴식 안내, 기기 · 프레임 정보 받기 | 24시간 |
| 환불 | 스토어 환불 정책 안내 | 24시간 |

## 7. 장애 대응
| 등급 | 예 | 대응 |
|---|---|---|
| 1 | 실행 안 됨 · 박자 전체 어긋남 | 즉시 이전 빌드 · 공지 |
| 2 | 특정 곡 · 기기 문제 | 확인 후 패치 |
| 3 | 화면 깨짐 | 다음 패치 |

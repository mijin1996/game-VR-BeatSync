# QA · 출시 체크리스트 — VR BeatSync

> 문서 세트: [목차](common_Index.md) · 기획: [컨셉](planning_Concept.md) · [시스템](planning_System.md) · [전투·개체](planning_Combat_Entity.md) · [레벨 디자인](planning_LevelDesign.md) · [밸런스](planning_Balance.md) · 사업·운영: [사업 검토](owner_Business.md) · [요구사항·로드맵](planning_Requirements.md) · [Business Model](planning_Monetization.md) · [서버·데이터](dev_Server_Data.md) · [리소스](design_Resources.md) · [로그·지표](planning_Analytics.md) · **QA·출시** · [라이브 운영](ops_LiveOps.md) · [사업화·법무](owner_Business_Legal.md) · [needFix](common_needFix.md)

> **문서 정보** · 문서 번호 `D11-QA` · 버전 v1.0 · 상태 `Draft` · 담당 QA · 최종 수정 2026-11-21 · 관련 문서 [시스템](planning_System.md) · [사업화·법무](owner_Business_Legal.md)

**변경 이력**

| 버전 | 날짜 | 변경 내용 | 사유 | 결정자 | 영향 범위 |
|---|---|---|---|---|---|
| v1.0 | 2026-11-21 | 최초 작성 | — | QA | — |

---

## 1. 목적
**출시해도 되는 상태인지** 판정하는 문서예요. VR은 **편의 · 안전 · 성능**이 스토어 심사와 리뷰를 좌우해요.

## 2. 버그 등급
| 등급 | 기준 | 출시 조건 |
|---|---|---|
| 치명 | 진행 불가 · 크래시 · **박자 어긋남** · 프레임 급락(멀미 유발) | 0건 |
| 높음 | 판정 오류 · 규칙이 의도와 다름 | 0건 목표 |
| 보통 · 낮음 | 불편 · 보기 문제 | 목록 관리 |

## 3. 테스트 케이스
| TC ID | 확인 방법 | 기대 결과 | 관련 | 등급 |
|---|---|---|---|---|
| `TC_001` | 첫 노트 일부러 놓치기 | GAME OVER 안 됨 | [S-01](common_needFix.md) | 치명 |
| `TC_002` | 곡 끝까지 플레이 | CLEAR 결과 화면 | [S-02](common_needFix.md) | 치명 |
| `TC_003` | 곡 시작 직후 노트 수 세기 | 비트맵 첫 노트부터 나옴 | [S-03](common_needFix.md) | 높음 |
| `TC_004` | 칼끝으로 벴는데 손잡이가 먼저 닿는 각도 | PERFECT 판정 | [S-04](common_needFix.md) | 높음 |
| `TC_005` | Any 노트를 옆으로 베기 | Hit | [S-05](common_needFix.md) | 높음 |
| `TC_006` | 3줄 비트맵 | 3줄로 나옴 | [S-06](common_needFix.md) | 보통 |
| `TC_007` | 플레이 중 헤드셋 벗기 → 쓰기 | 멈춤 → 카운트다운 → 박자 맞게 이어짐 | [S-07](common_needFix.md) | 치명 |
| `TC_008` | Expert 가장 빠른 구간 | 뜻밖의 Miss 없음 | [S-19](common_needFix.md) | 높음 |
| `TC_009` | 박자 보정 후 같은 곡 | 체감 박자 일치 | [S-16](common_needFix.md) | 치명 |
| `TC_010` | 30분 연속 플레이 | 프레임 · 발열 · 메모리 이상 없음 | — | 치명 |

## 4. VR 편의 · 안전 기준 (정할 것 → [F-09](common_needFix.md#8-정할-것))
| 항목 | 기준 (제안) | 이유 |
|---|---|---|
| 프레임 | 헤드셋 기본 주사율 유지 (예: 72 / 90Hz), 떨어지는 구간 0 | 프레임 급락 = 멀미 |
| 카메라 | 플레이어 시점을 게임이 **강제로 움직이지 않음** | 멀미 방지 |
| 시작 안내 | 주변 공간 확보 · 휴식 안내 화면 | 팔을 크게 휘두르는 게임 |
| 앉아서 모드 · 키 보정 | 노트 높이를 사용자 키 · 앉은 자세에 맞춤 | 접근성 · 심사 |
| 경계(가디언) | 플레이 영역 밖으로 나가면 시스템 경계 표시를 막지 않음 | 안전 |
| 번쩍임 | 화면 전체가 빠르게 번쩍이는 연출 없음 (또는 끄기 옵션) | 광과민성 |
| 글자 | 가까이 · 크게, 시야 가장자리에 중요한 글자 두지 않기 | VR 가독성 |
| 휴식 | 곡 사이 결과 화면에서 쉬어 갈 수 있음 | 피로 |

## 5. 스토어 제출 준비
| 항목 | 상태 |
|---|---|
| 1차 스토어 결정 (Meta Horizon Store · Android XR 등) | 대기 ([F-01](common_needFix.md)) |
| 스토어 기술 기준 (성능 · 입력 · 안전) 목록을 TC로 옮기기 | 대기 |
| 연령 등급 · 편의 등급(Comfort rating) | 대기 |
| 트레일러 영상 · 스크린샷 · 아이콘 | 대기 |
| 개인정보처리방침 (분석 도구 쓰면) | 대기 |
| 곡 크레딧 · 라이선스 화면 | 대기 ([REQ_114](planning_Requirements.md)) |

## 6. 출시 판정
- [ ] 치명 버그 0
- [ ] VR 편의 · 안전 기준 전부 통과
- [ ] [리소스](design_Resources.md) §2 곡 · 악보 라이선스 전부 확인
- [ ] [사업화 · 법무](owner_Business_Legal.md) 전 항목 완료

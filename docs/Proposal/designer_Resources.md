# 리소스 (아트 · 사운드 · 곡) 명세 — VR BeatSync

> 문서 세트: [목차](common_Index.md) · 기획: [컨셉](planner_Concept.md) · [시스템](planner_System.md) · [전투·개체](planner_Combat_Entity.md) · [레벨 디자인](designer_LevelDesign.md) · [밸런스](planner_Balance.md) · 사업·운영: [사업 검토](owner_Business.md) · [요구사항·로드맵](planner_Requirements.md) · [Business Model](planner_Monetization.md) · [서버·데이터](developer_Server_Data.md) · **리소스** · [로그·지표](planner_Analytics.md) · [QA·출시](tester_Release.md) · [라이브 운영](operator_LiveOps.md) · [사업화·법무](owner_Legal.md) · [needFix](common_needFix.md)

> **문서 정보**

| 문서 번호 | 버전 | 상태 | 책임자 | 협업 | 최종 수정 | 관련 문서 |
|---|---|---|---|---|---|---|
| `D09-RES` | v1.1 | `Draft` | 아트 디자이너 | 사운드 디자이너 | 2026-11-23 | [전투·개체](planner_Combat_Entity.md) · [사업화·법무](owner_Legal.md) |

**변경 이력**

| 버전 | 날짜 | 변경 내용 | 사유 | 결정자 | 영향 범위 |
|---|---|---|---|---|---|

---

## 1. 목적
게임에 들어가는 **모든 곡 · 악보 · 모델 · 소리 · 폰트의 목록 · 라이선스**를 관리함. 리듬 게임은 **곡이 곧 상품**이라 곡 라이선스가 가장 중요함.
> **규칙:** 라이선스가 `확인됨`이 아닌 리소스가 있으면 출시하지 않음.

## 2. 곡 · 악보 (가장 먼저 확인)
| 곡 ID (제안) | 곡 (파일 이름 기준) | 출처 (추정) | 악보 | 곡 라이선스 | 악보 라이선스 |
|---|---|---|---|---|---|
| `SONG_001` | Club Change Up (`phill_dillow-club-change-up-beat-585`) | 무료 음원 사이트 (Pixabay 이름 형식) | Beat Sage 자동 생성 | ⚠️ 다운로드 페이지 · 라이선스 원문 저장 | ⚠️ Beat Sage 이용 조건 확인 |
| `SONG_002` | Upbeat Funk (`music_for_videos-upbeat-funk-commercial-112624`) | 위와 같음 | 위와 같음 | ⚠️ | ⚠️ |
| `SONG_003` | Echoes of Lumen (`trap beat heavy bass`) | 미확인 | 위와 같음 | ⚠️ 출처 확인 | ⚠️ |
| `SONG_004` | Sad Dramatic Piano (`ludosoundx-...-262415`) | 무료 음원 사이트 (Pixabay 이름 형식) | 위와 같음 | ⚠️ | ⚠️ |

**확인할 것**
| 질문 | 왜 |
|---|---|
| 곡 라이선스가 **게임에 넣어 판매**하는 것을 허용하나? | 무료 음원도 "영상 배경음은 되지만 단독 재생 · 판매 상품 포함은 안 됨"인 경우가 있음 |
| 작곡가 표기가 필요한가? | 크레딧 화면([REQ_114](planner_Requirements.md))에 넣기 |
| Beat Sage로 만든 악보를 상업 게임에 써도 되나? | AI 생성 도구의 이용 조건 · 생성물 권리를 확인 필요. 안 되면 악보를 직접 만들거나 수정 |
| 악보 파일 형식(`.dat`)이 다른 게임 형식인가? | 형식 자체는 데이터 구조라 문제가 적지만, **다른 게임의 이름 · 로고를 게임 안에 쓰지 않기** |

→ [F-03](common_needFix.md#8-정할-것)

## 3. 그 밖의 리소스
| 리소스 ID | 폴더 | 쓰는 곳 | 출처 | 라이선스 |
|---|---|---|---|---|
| `MDL_NOTE_RED` · `MDL_NOTE_BLUE` | `fab/` (`redNote` · `blueNote` 프리팹) | 노트 | Fab 마켓 (폴더 이름 기준) | ⚠️ Fab 라이선스 종류(Standard 등) · 구매 기록 |
| `FX_RAINBOW` | `LuminaryLabs/StylizedRainbow` | 무대 연출 | 에셋 팩 | ⚠️ |
| `FX_CFXR_*` | `JMO Assets/Cartoon FX Remaster` | 베기 이펙트 | Unity Asset Store (JMO) | ⚠️ 다운로드 기록 |
| `VID_BG` | `Video/video 1.mp4` (meta만 있고 파일 없음) | 배경 영상? | 미확인 | ⚠️ 쓰는지 확인 |
| `XR_TEMPLATE_*` | `VRTemplateAssets` · `Samples/XR Interaction Toolkit` · `XRI` | 손 · 메뉴 | Unity 템플릿 · 샘플 | ✅ Unity 패키지 (Unity Companion License 범위 확인) |
| `SFX_NOTECUT` | `Sounds/NoteCut.mp3` | 베기 소리 | 미확인 | ⚠️ |
| `SFX_UI_*` | `Sounds/ButtonClick.wav` · `CountdownGo.wav` · `SongSelect.wav` | UI | 미확인 | ⚠️ |
| `FONT_INTER` | `VRTemplateAssets/Fonts/Inter` | UI 글자 | Inter | ✅ SIL OFL 1.1 |

## 4. 새로 필요한 리소스
| 리소스 ID | 종류 | 관련 | 상태 |
|---|---|---|---|
| `SONG_005+` | 상업 이용 가능한 곡 (최소 5곡) | [REQ_112](planner_Requirements.md) | 대기 |
| `UI_RESULT` | 결과 화면 (CLEAR · 랭크) | [S-02](common_needFix.md) | 대기 |
| `UI_HP_GAUGE` | 체력 게이지 | [S-17](common_needFix.md) | 대기 |
| `UI_CALIB` | 박자 보정 화면 | [S-16](common_needFix.md) | 대기 |
| `UI_SAFETY` | 시작 안전 안내 화면 (주변 공간 · 휴식) | [QA](tester_Release.md) §4 | 대기 |
| `FX_COMBO_*` | 콤보 연출 | [S-12](common_needFix.md) | 대기 |
| `STORE_*` | VR 스토어 이미지 · 트레일러 영상 | [QA](tester_Release.md) §5 | 대기 |

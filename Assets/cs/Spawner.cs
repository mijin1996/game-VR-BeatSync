using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] note;        // 예전 랜덤 모드용
    public Transform[] spawnPoint;   // 예전 랜덤 모드용

    [Header("GM 비트맵 모드")]
    public GameObject redNote;       // _type 0 (왼손)
    public GameObject blueNote;      // _type 1 (오른손)
    public float colSpacing = 0.6f;  // 가로 칸 간격 (4칸)
    public float rowSpacing = 0.5f;  // 세로 줄 간격 (3줄)
    public float bottomY = -0.39f;   // 맨 아랫줄 높이 (스포너 기준)

    [Header("랜덤 모드 (GM 없을 때 연습용)")]
    public bool randomMode = false;
    public float beat = 1;
    float timer = 0f;

    void Update() {
        if (!randomMode) {
            return;
        }
        timer += Time.deltaTime;
        if (timer > beat) {
            int n = Random.Range(0, note.Length);
            int sp = Random.Range(0, spawnPoint.Length);

            GameObject obj = Instantiate(note[n], spawnPoint[sp]);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));

            timer -= beat;
        }
    }

    // GM이 비트맵 타이밍에 맞춰 호출
    public void SpawnNote(int type, int lineIndex, int lineLayer, int cutDirection, float advance) {
        GameObject prefab = (type == 0) ? redNote : blueNote;

        // 스포너가 플레이어를 보고 있으므로, 플레이어 기준 오른쪽 = -transform.right
        Vector3 playerRight = -transform.right;
        Vector3 pos = transform.position
                    + playerRight * ((lineIndex - 1.5f) * colSpacing)
                    + Vector3.up * (bottomY + lineLayer * rowSpacing)
                    + transform.forward * advance;

        GameObject obj = Instantiate(prefab, pos, transform.rotation);

        // Saber.cs: 칼 이동방향이 노트 up 과 반대(130도 이상)일 때 잘림
        // → 노트 up 을 "베는 방향의 반대"로 돌려 놓는다
        obj.transform.Rotate(0, 0, CutAngle(cutDirection), Space.Self);
    }

    float CutAngle(int dir) {
        switch (dir) {
            case 0: return 180f;   // 위로 베기
            case 1: return 0f;     // 아래로 베기
            case 2: return 90f;    // 왼쪽으로
            case 3: return -90f;   // 오른쪽으로
            case 4: return 135f;   // 왼쪽 위
            case 5: return -135f;  // 오른쪽 위
            case 6: return 45f;    // 왼쪽 아래
            case 7: return -45f;   // 오른쪽 아래
            default: return 0f;    // 8: 아무 방향 (일단 아래로 베기)
        }
    }
}

using UnityEngine;

public class Note : MonoBehaviour {
    public float speed = 2f;   // GM의 noteSpeed와 같게

    void Start() {
        Destroy(gameObject, 15f);   // 못 벤 노트는 15초 뒤 삭제
    }

    void Update() {
        //노트가 앞으로 이동하는 코드
        transform.position += Time.deltaTime * transform.forward * speed;
    }
}

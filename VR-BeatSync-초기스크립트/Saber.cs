using UnityEngine;

public class Saber : MonoBehaviour{
    public LayerMask layer;
    Vector3 prevPosi; // previousposition = 이전 위치


    void Update(){
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 1, layer)){
            Vector3 swingDir = transform.position - prevPosi;

            // 칼의 이동 방향과 노트의 위쪽 방향 사이의 각도가 130도보다 크면 노트 제거
            if(Vector3.Angle(swingDir, hit.transform.up) > 130  ){
                Destroy(hit.transform.gameObject);
            }
        }
        prevPosi = transform.position;
    }
}

/*
[📌📝MEMO]

LayerMask :: 종류별로 맞히기 위함 = tag와 비슷
forward : z방향
up: y방향



⭐👍 Magic Numbers로 작성된 것을 변수로 바꾸면 가독성이 좋아짐  & Inspector에서 값을 바로 조절 가능
1: rayLength : 칼 앞쪽 감지 거리
130: cutAngle : 이 각도보다 크면 자름
*/

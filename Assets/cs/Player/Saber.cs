using UnityEngine;

public class Saber : MonoBehaviour{
    public LayerMask layer;
    Vector3 prevPos;


    void Update(){
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 1, layer)){
            Vector3 v = transform.position - prevPos;

            // 칼의 이동 방향과 노트의 위쪽 방향 사이의 각도가 130도보다 크면 노트 제거
            if(Vector3.Angle(v, hit.transform.up) > 130  ){
                // 벨 수 있는 것(ICuttable)이면 Cut() 으로 판정, 아니면 예전처럼 제거
                ICuttable cuttable = hit.transform.GetComponentInParent<ICuttable>();
                if(cuttable != null){
                    cuttable.Cut();
                }else{
                    Destroy(hit.transform.gameObject);
                }
            }
        }
        prevPos = transform.position;
    }
}

/*
[📌📝MEMO]

LayerMask :: 종류별로 맞히기 위함 = tag와 비슷
forward : z방향
up: y방향


*/

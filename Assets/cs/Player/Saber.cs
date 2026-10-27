using UnityEngine;

public class Saber : MonoBehaviour{
    public LayerMask layer;
    [SerializeField] private HitZone _hitZone = new HitZone();
    [SerializeField] private float _minSwingSpeed = 0.5f;
    Vector3 prevPos;


    void Update(){
        RaycastHit hit;
        // 칼 손잡이(0m)부터 칼 끝까지 전체 길이로 레이를 쏨 (칼 오브젝트 위치 = 칼 가운데)
        Vector3 handPos = transform.position - transform.forward * (_hitZone.BladeLength * 0.5f);

        if(Physics.Raycast(handPos, transform.forward, out hit, _hitZone.BladeLength, layer)){
            Vector3 v = transform.position - prevPos;

            // 칼을 대고만 있을 때(거의 안 움직임)는 판정하지 않음
            if(v.magnitude / Mathf.Max(Time.deltaTime, 0.0001f) >= _minSwingSpeed){
                // 칼의 이동 방향과 노트의 위쪽 방향 사이의 각도가 130도보다 크면 올바른 방향
                bool rightDirection = Vector3.Angle(v, hit.transform.up) > 130;
                // 맞은 위치(손에서 거리)로 판정, 방향이 틀리면 MISS
                HitGrade grade = rightDirection ? _hitZone.GradeAt(hit.distance) : HitGrade.Miss;

                ICuttable cuttable = hit.transform.GetComponentInParent<ICuttable>();
                if(cuttable != null){
                    cuttable.Cut(grade);
                }else if(grade != HitGrade.Miss){
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

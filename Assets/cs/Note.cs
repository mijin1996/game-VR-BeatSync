using UnityEngine;

public class Note : MonoBehaviour{

    void Update(){
        //노트가 앞으로 이동하는 코드
        transform.position += Time.deltaTime * transform.forward * 2;
    }
}

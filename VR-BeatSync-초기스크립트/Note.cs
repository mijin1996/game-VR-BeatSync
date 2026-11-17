using UnityEngine;

public class Note : MonoBehaviour{
    //노트가 앞으로 이동하는 코드
    void Update(){        
        transform.position += Time.deltaTime * transform.forward * 2;
    }
}

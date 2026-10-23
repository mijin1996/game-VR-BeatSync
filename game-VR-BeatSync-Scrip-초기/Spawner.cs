using UnityEngine;

public class Spawner : MonoBehaviour{
    public GameObject[] note; 
    public Transform[] spawnPoint;

    public float beat = 1;
    float timer = 0f;

    void Update(){
        timer += Time.deltaTime;
        if (timer > beat){
            int noteIndex = Random.Range(0, 4); // 몇 번째 노트를 생성할지
            int spawnPointIndex = Random.Range(0, 4); // 몇 번째 스폰 포인트에서 생성할지

            GameObject noteObj = Instantiate(note[noteIndex], spawnPoint[spawnPointIndex]);
            
            noteObj.transform.localPosition = Vector3.zero;
            noteObj.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));

            // 0일 때가 아니라 beat(1)를 넘을 때 note나와
            timer -= beat;
        }

    }
}


/*
timer -= beat이 없으면 첫 박자 이후로 매 프레임마다 노트가 생겨요.
타이머를 한 박자만큼 되돌려서 다음 박자를 정확히 기다리게 하는 코드


*/

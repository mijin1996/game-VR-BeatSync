using UnityEngine;

public class Spawner : MonoBehaviour{
    public GameObject[] note; 
    public Transform[] spawnPoint;

    public float beat = 1;
    float timer=0f;

    void Update(){
        timer += Time.deltaTime;
        if (timer > beat){
            int n = Random.Range(0, 4);
            int sp = Random.Range(0, 4);

            GameObject obj = Instantiate(note[n], spawnPoint[sp]);
            
            obj.transform.localPosition = Vector3.zero;
            obj.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));

            timer -= beat;
        }

    }
}

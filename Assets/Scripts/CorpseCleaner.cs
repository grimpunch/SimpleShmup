using UnityEngine;
using System.Collections;

public class CorpseCleaner : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InvokeRepeating("Clean", 1f,4f);
	}
	

    void Clean (){
        foreach(EnemyHitHandler go in gameObject.GetComponentsInChildren<EnemyHitHandler>(true)){
            Debug.Log(go.gameObject.name);
            Debug.Log(go.gameObject.activeInHierarchy);
            if(go.gameObject.activeInHierarchy == false){
                Destroy(go.gameObject);
            }
        }
        return;
    }
}

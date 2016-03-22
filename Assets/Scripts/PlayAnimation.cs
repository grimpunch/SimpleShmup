using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {
    public float delay = 0.0f;
	// Use this for initialization
	void OnEnable () {
        Invoke("ActivateNow", delay);
	}

    void ActivateNow(){
        if(!GetComponent<ParticleSystem>().isPlaying){
            GetComponent<ParticleSystem>().Play();
        }
        Destroy(this);
    }
}

using UnityEngine;
using System.Collections;

public class ResetGlitchCam : MonoBehaviour {
    public float delay = 0.0f;
	// Use this for initialization
	void OnEnable () {
        Invoke("ActivateNow", delay);

	}

    void ActivateNow(){
        Camera.main.GetComponent<Kino.AnalogGlitch>().horizontalShake = 0.0f;
        Destroy(this);
    }
	
}

using UnityEngine;
using System.Collections;

public class GibOnMessage : MonoBehaviour {

    public string particleSystemPool = null;
    public GameObject powerUp;

    public void Gib() {
		if (particleSystemPool.Contains("Pool")) {
			GameObject particleSystem = GameObject.Find(particleSystemPool).GetComponent<ObjectPoolScript>().GetPooledObject();
			particleSystem.transform.position = transform.position;
			particleSystem.SetActive(true);
        }
        if (powerUp) { GameObject GO = (GameObject)Instantiate(powerUp, transform.position, Quaternion.identity); }
        gameObject.SetActive(false);
    }
}

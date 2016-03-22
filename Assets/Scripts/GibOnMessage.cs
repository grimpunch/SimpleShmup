using UnityEngine;
using System.Collections;

public class GibOnMessage : MonoBehaviour
{

	public string particleSystemPool = null;
	public string powerUpPool = null;
	public GameObject powerUp;
    public GameObject specialParticle = null;
	public void Gib()
	{
		if (!gameObject)
			return;
        if (specialParticle != null) {
            GameObject GO = (GameObject)Instantiate(specialParticle, transform.position, Quaternion.identity);
        }

        if (particleSystemPool.Contains("Pool")) {
			GameObject particleSystem = GameObject.Find(particleSystemPool).GetComponent<ObjectPoolScript>().GetPooledObject();
			particleSystem.transform.position = transform.position;
			particleSystem.SetActive(true);
		}
		if (powerUpPool.Contains("Pool")) {
			for (int i = 0; i < 10; i++) {
				GameObject pooledPowerUp = GameObject.Find(powerUpPool).GetComponent<ObjectPoolScript>().GetPooledObject();
				pooledPowerUp.transform.position = transform.position;
				pooledPowerUp.SetActive(true);
			}
		}
		if (powerUp) {
			GameObject GO = (GameObject)Instantiate(powerUp, transform.position, Quaternion.identity);
		}
		gameObject.SetActive(false);
	}
}

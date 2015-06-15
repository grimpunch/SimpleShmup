using UnityEngine;
using System.Collections;

public class GibOnMessage : MonoBehaviour {

    public GameObject particleSystemPrefab;
    public GameObject powerUp;

    public void Gib() {
        if (particleSystemPrefab != null) {
            GameObject particleSystem = (GameObject)Instantiate(particleSystemPrefab, new Vector3(transform.position.x, transform.position.y,-1F), transform.rotation);
        }
        if (powerUp) { GameObject GO = (GameObject)Instantiate(powerUp, transform.position, Quaternion.identity); }
        gameObject.SetActive(false);
    }
}

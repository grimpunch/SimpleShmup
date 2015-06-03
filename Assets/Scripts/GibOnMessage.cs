using UnityEngine;
using System.Collections;

public class GibOnMessage : MonoBehaviour {

    public GameObject particleSystemPrefab;
    public GameObject powerUp;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void Gib() {
        if (particleSystemPrefab != null) {
            GameObject particleSystem = (GameObject)Instantiate(particleSystemPrefab, new Vector3(transform.position.x, transform.position.y,-1F), transform.rotation);
            particleSystem.name = gameObject.name + "DeathParticleSystemInstance";
        }
        if (powerUp) { GameObject GO = (GameObject)Instantiate(powerUp, transform.position, Quaternion.identity); }
        if (gameObject.name == "ShmupShip") { Destroy(gameObject); return; }
        gameObject.SetActive(false);

    }
}

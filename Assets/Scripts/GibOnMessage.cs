using UnityEngine;
using System.Collections;

public class GibOnMessage : MonoBehaviour {

    public GameObject particleSystemPrefab;

    // Use this for initialization
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
        Destroy(gameObject);
    }
}

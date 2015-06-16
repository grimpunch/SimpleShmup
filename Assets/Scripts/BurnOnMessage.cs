using UnityEngine;
using System.Collections;

public class BurnOnMessage : MonoBehaviour {

    public GameObject particleSystemPrefab;
    private GameObject activeParticleSystem;
    private bool burning = false;
    private float burnTimeRemaining;
    public float burnTime = 3.0f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Utils.Paused) return;
        if (burnTimeRemaining <= 0) {
            StopBurn();
        } else { burnTimeRemaining -= Time.deltaTime; }
    }

    public void Burn() {
        if (particleSystemPrefab != null && !burning) {
            activeParticleSystem = (GameObject)Instantiate(particleSystemPrefab, new Vector3(transform.position.x, transform.position.y, -1F), transform.rotation);
            activeParticleSystem.transform.parent = transform;
            burning = true;
        }
        if (burning) { burnTimeRemaining = burnTime; }
    }
    public void StopBurn() {
        if (burning) {
            Destroy(activeParticleSystem);
            burning = false;
        }
    }

}

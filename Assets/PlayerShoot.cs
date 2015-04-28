using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    public GameObject shotPrefab;
    private bool readyToFire;
    //public Vector3 shotOffset = new Vector3(0, 0.5f, 0);
    int shotLayer;

    public float fireDelay = 0.25f;
    float cooldownTimer = 0;



    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetButton("Fire1") && cooldownTimer <= 0) {
            // SHOOT!
            cooldownTimer = fireDelay;

            GameObject shotGO = (GameObject)Instantiate(shotPrefab, transform.position, transform.rotation);
            shotGO.name = "PlayerShotInstance";
        }
    }
}

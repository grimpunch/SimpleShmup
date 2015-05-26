using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    public GameObject shotPrefab;
    public GameObject turretLeft;
    public GameObject turretRight;
    private bool fireLeft = true;
    public int upgradeLevel = 0;
    private bool readyToFire;
    private Vector3 shotPosition;
    private Quaternion shotRotation;
    //public Vector3 shotOffset = new Vector3(0, 0.5f, 0);
    int shotLayer;

    public float fireDelay = 0.25f;
    float cooldownTimer = 0;

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Utils.Paused) return;
        cooldownTimer -= Time.deltaTime;

        if (Input.GetButton("Fire1") && cooldownTimer <= 0) {
            // SHOOT!
            cooldownTimer = fireDelay;
            GameObject shotCenter = (GameObject)Instantiate(shotPrefab, transform.position, transform.rotation);
            shotCenter.name = "PlayerShotInstance";
            if (GetComponent<AudioSource>() != null) {
                if (!GetComponent<AudioSource>().isPlaying) {
                    GetComponent<AudioSource>().Play();
                }
            }

            if (upgradeLevel == 1) {
                if (fireLeft) {
                    shotPosition = turretLeft.transform.position;
                    shotRotation = turretLeft.transform.rotation;
                    fireLeft = false;
                } else {
                    shotPosition = turretRight.transform.position;
                    shotRotation = turretRight.transform.rotation;
                    fireLeft = true;
                }

                GameObject shotSide = (GameObject)Instantiate(shotPrefab, shotPosition, shotRotation);
                shotSide.name = "PlayerShotInstance";
            }
            if (upgradeLevel == 2) {
                    shotPosition = turretLeft.transform.position;
                    shotRotation = turretLeft.transform.rotation;
                    GameObject shotLeft = (GameObject)Instantiate(shotPrefab, shotPosition, shotRotation);
                    shotLeft.name = "PlayerShotInstance";
                    shotPosition = turretRight.transform.position;
                    shotRotation = turretRight.transform.rotation;
                    GameObject shotRight = (GameObject)Instantiate(shotPrefab, shotPosition, shotRotation);
                    shotRight.name = "PlayerShotInstance";
            }
            if (upgradeLevel > 2) { upgradeLevel = 2; }
        }
        if (!Input.GetButton("Fire1")) { GetComponent<AudioSource>().Stop(); }
    }
}

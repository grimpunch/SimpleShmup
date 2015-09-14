using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    public GameObject turretLeft;
    public GameObject turretRight;
    private bool fireLeft = true;
    public int upgradeLevel = 0;
    private bool readyToFire;
    private Vector3 shotPosition;
    private Quaternion shotRotation;
    private ObjectPoolScript playerShotObjectPool;
    //public Vector3 shotOffset = new Vector3(0, 0.5f, 0);
    int shotLayer;

    public float fireDelay = 0.25f;
    float cooldownTimer = 0;

    // Use this for initialization
    void Start() {
        playerShotObjectPool = GameObject.Find("PlayerShotObjectPool").GetComponent<ObjectPoolScript>();
    }

    // Update is called once per frame
    void Update() {
        if (Utils.Paused) {
            if (GetComponent<AudioSource>() != null) {
                gameObject.GetComponent<AudioSource>().Pause();
                return;
            }
        }
        if (Utils.Paused) return;
        cooldownTimer -= Time.deltaTime;
        
        if (Application.isPlaying && !Utils.Paused) {
            gameObject.GetComponent<AudioSource>().UnPause();
        }

        if (FireButtonDown() && cooldownTimer <= 0) {
            // SHOOT!
            cooldownTimer = fireDelay;
            GameObject shotCenter = playerShotObjectPool.GetPooledObject();
            if (shotCenter == null) return;
            shotCenter.SetActive(true);
            shotCenter.transform.position = transform.position;
            shotCenter.transform.rotation = transform.rotation;
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

                GameObject shotSide = playerShotObjectPool.GetPooledObject();
                if (shotSide == null) return;
                shotSide.SetActive(true);
                shotSide.transform.position = shotPosition;
                if (FocusFireButtonDown()) shotSide.transform.rotation = Quaternion.identity;
                else { shotSide.transform.rotation = shotRotation; } 
            }
            if (upgradeLevel == 2) {
                    shotPosition = turretLeft.transform.position;
                    shotRotation = turretLeft.transform.rotation;
                    GameObject shotLeft = playerShotObjectPool.GetPooledObject();
                    if (shotLeft == null) return;
                    shotLeft.SetActive(true);
                    shotLeft.transform.position = shotPosition;
                    if (FocusFireButtonDown()) shotLeft.transform.rotation = Quaternion.identity;
                    else { shotLeft.transform.rotation = shotRotation; }
                    shotPosition = turretRight.transform.position;
                    shotRotation = turretRight.transform.rotation;
                    GameObject shotRight = playerShotObjectPool.GetPooledObject();
                    if (shotRight == null) return;
                    shotRight.SetActive(true);
                    shotRight.transform.position = shotPosition;
                    if (FocusFireButtonDown()) shotRight.transform.rotation = Quaternion.identity;
                    else { shotRight.transform.rotation = shotRotation; } 
            }
            if (upgradeLevel > 2) { upgradeLevel = 2; }
        }
        if (!FireButtonDown()) { GetComponent<AudioSource>().Stop(); }
    }

    private static bool FireButtonDown() {
        return Input.GetButton("Fire1");
    }
    
    private static bool FocusFireButtonDown() {
        return Input.GetButton("Fire2");
    }
}

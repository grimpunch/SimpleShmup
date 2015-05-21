using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {
    public GameObject shotPrefab;
    public float shotDelay = 0.2f;
    private float timeToShot = 0.0f;
    public float burstDelay = 6.0f;
    private float waitUntilBurst = 0.0f;
    public int burstAmount = 10;
    private int burstShots;
    public bool canShoot = true;
    void Start() {

    }

    void FixedUpdate() {
        if (!canShoot) { timeToShot += Time.fixedDeltaTime; return; }
        if (burstShots > burstAmount) {
            if (waitUntilBurst < burstDelay) {
                waitUntilBurst += Time.fixedDeltaTime;
            } 
            else { 
                burstShots = 0; 
                waitUntilBurst = 0.0F; 
            }
        } else {
            if (burstShots < burstAmount) {
                if (timeToShot > shotDelay) {
                    Shoot();
                    timeToShot = 0.0F;
                }
                timeToShot += Time.fixedDeltaTime;
            }
        }
    }


    void Shoot() {
        if (GetComponent<AudioSource>() != null) {
            if (!GetComponent<AudioSource>().isPlaying) {
                GetComponent<AudioSource>().Play();
            }
        }
        GameObject shotGO = (GameObject)Instantiate(shotPrefab, transform.position, transform.rotation);
        shotGO.name = gameObject.name + "ShotInstance";
        burstShots++;
    }
}

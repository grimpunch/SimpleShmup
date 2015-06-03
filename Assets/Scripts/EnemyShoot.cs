using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {
    public string shotPool;
    private ObjectPoolScript enemyShotObjectPoolScript;
    public float shotDelay = 0.2f;
    private float timeToShot = 0.0f;
    public float burstDelay = 6.0f;
    private float waitUntilBurst = 0.0f;
    public int burstAmount = 10;
    private int burstShots;
    public bool canShoot = true;
    void Start() {
        enemyShotObjectPoolScript = GameObject.Find(shotPool).GetComponent<ObjectPoolScript>();
    }

    void FixedUpdate() {
        if (Utils.Paused) return;
        if (!canShoot) { timeToShot += Time.fixedDeltaTime; return; }
        if (burstShots >= burstAmount) {
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
        GameObject shotGO = enemyShotObjectPoolScript.GetPooledObject();
        shotGO.transform.position = transform.position;
        shotGO.transform.rotation = transform.rotation;
        shotGO.SetActive(true);
        burstShots++;
    }
}

using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {
    public GameObject shotPrefab;
    public float shotDelay = 0.2f;

    void Start() {
        InvokeRepeating("Shoot", shotDelay,shotDelay);
    }


    void Shoot() {
        if (GetComponent<AudioSource>() != null) {
            if (!GetComponent<AudioSource>().isPlaying) {
                GetComponent<AudioSource>().Play();
            }
        }
        GameObject shotGO = (GameObject)Instantiate(shotPrefab, transform.position, transform.rotation);
        shotGO.name = gameObject.name+"ShotInstance";
    }
}

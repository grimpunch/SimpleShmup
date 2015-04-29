using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {
    public GameObject bullet;
    public float shotDelay = 0.2f;

    void Start() {
        Invoke("Shoot", shotDelay);
    }

    void Shoot() {
        if (GetComponent<AudioSource>() != null) {
            if (!GetComponent<AudioSource>().isPlaying) {
                GetComponent<AudioSource>().Play();
            }
        }
        Instantiate(bullet, transform.position, transform.rotation);
        Invoke("Shoot", shotDelay);
    }
}

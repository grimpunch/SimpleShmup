using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {
    public GameObject bullet;
    public float shotDelay = 0.2f;

    void Start() {
        Invoke("Shoot", shotDelay);
    }

    void Shoot() {
        if (audio != null) {
            if (!audio.isPlaying) {
                audio.Play();
            }
        }
        Instantiate(bullet, transform.position, transform.rotation);
        Invoke("Shoot", shotDelay);
    }
}

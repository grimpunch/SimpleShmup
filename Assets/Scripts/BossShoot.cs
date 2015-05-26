using UnityEngine;
using System.Collections;

public class BossShoot : MonoBehaviour {
    public GameObject shotPrefab;
    public float shotDelay = 0.2f;
    public bool canShoot = false;

    void Start() {
        
    }

    void FixedUpdate() {
        if (Utils.Paused) return;
        canShoot = gameObject.GetComponent<Animator>().GetBool("CanFire");
        if (!canShoot) {
            return;
        }
        if (!IsInvoking()) {
            Invoke("Shoot", shotDelay);
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
    }
}

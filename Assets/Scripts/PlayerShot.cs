using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour {

    private GameObject shot;
    private ScreenBoundsHandler screenBounds;
    public float shotSpeed = 1.0F;

    // Use this for initialization
    void Start() {
        shot = gameObject;
        screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Utils.Paused) return;
        if (shot.transform.position.y < screenBounds.ScreenTop - 0.1F) {
            this.transform.position += transform.up * (shotSpeed * Time.fixedDeltaTime);
        } else {
            shot.SetActive(false);
        }
    }
}

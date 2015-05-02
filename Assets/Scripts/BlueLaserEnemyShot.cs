using UnityEngine;
using System.Collections;

public class BlueLaserEnemyShot : MonoBehaviour {

    private GameObject shot;
    private ScreenBoundsHandler screenBounds;
    public float shotSpeed = 0.5F;

    // Use this for initialization
    void Start() {
        shot = gameObject;
        screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (shot.transform.position.y > screenBounds.ScreenBottom - 2.0F) {
            this.transform.position += transform.up * (shotSpeed * Time.fixedDeltaTime);
        } else {
            Destroy(this.gameObject);
        }
    }
}

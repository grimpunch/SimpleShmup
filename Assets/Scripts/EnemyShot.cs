using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour {

    private GameObject shot;
    private ScreenBoundsHandler screenBounds;
    public float shotSpeed = 0.5F;
    private const int PLAYERLASERLAYER = 14;


    // Use this for initialization
    void Start() {
        shot = gameObject;
        screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
    }

    void OnTriggerEnter2D(Collider2D col2d) {
        if (col2d.gameObject.layer == PLAYERLASERLAYER) {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Utils.Paused) return;
        if (shot.transform.position.x > screenBounds.ScreenRight + 0.2F || shot.transform.position.x < screenBounds.ScreenLeft - 0.2F) {
            Debug.Log("Destroying offscreen left right bullet");
            Destroy(this.gameObject);
        }
        if (shot.transform.position.y > screenBounds.ScreenBottom - 0.1F || shot.transform.position.y < screenBounds.ScreenTop + 0.1F) {
            this.transform.position += transform.up * (shotSpeed * Time.fixedDeltaTime);
        } else {
            Destroy(this.gameObject);
        }
    }
}

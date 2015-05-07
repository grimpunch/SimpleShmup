using UnityEngine;
using System.Collections;

public class EnemyHitHandler : MonoBehaviour {

    public int shipHealth = 5;
    private const int PLAYERSHOTLAYER = 10;
    private const int PLAYERLASERLAYER = 14;
    private ScreenBoundsHandler screenBounds;

    // Use this for initialization
    void Start() {
        screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
    }

    void OnTriggerEnter2D(Collider2D col2d) {
        if (!screenBounds) { return; }
        if (transform.position.y > screenBounds.ScreenTop) { return; }
        if (col2d.gameObject.layer == PLAYERSHOTLAYER) {
            shipHealth -= 1;
            col2d.gameObject.SendMessage("Gib");
        } else if (col2d.gameObject.layer == PLAYERLASERLAYER) {
            shipHealth -= 5;
            }
    }


    // Update is called once per frame
    void Update() {
        if (shipHealth <= 0) {
            gameObject.SendMessage("Gib");
        }
    }
}

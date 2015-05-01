using UnityEngine;
using System.Collections;

public class DestroyWhenOffScreen : MonoBehaviour {

    public ScreenBoundsHandler screenBounds;

    // Use this for initialization
    void Start() {
        screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y < screenBounds.ScreenBottom - 2.0F) { /* Ship has gone off bottom of screen */
            Destroy(gameObject);
        }
    }
}

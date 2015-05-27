using UnityEngine;
using System.Collections;

public class ScrollLevelForward : MonoBehaviour {

    public float scrollSpeed = 2.0F;
    private float movingSpeed;
    public bool stopped;
    public float FadeSpeed = 1.0f;
    public BackgroundScroll[] backgrounds;
    // Use this for initialization
    void Start() {
        movingSpeed = scrollSpeed;
    }

    // Update is called once per frame
    void Update() {
        if (Utils.Paused) return;
        foreach (BackgroundScroll background in backgrounds) {
            background.stopped = stopped;
            background.verticalScrollSpeed = scrollSpeed * 4;
        }
        if (!stopped && scrollSpeed <= movingSpeed) {
            scrollSpeed += Mathf.Lerp(0f, movingSpeed, FadeSpeed * Time.deltaTime);
        }
        if (!stopped && scrollSpeed >= movingSpeed - 0.1f) scrollSpeed = movingSpeed;
        //Slow to a stop
        if (stopped && scrollSpeed >= 0f) { 
            scrollSpeed -= Mathf.Lerp(scrollSpeed, 0f, FadeSpeed * Time.deltaTime);
            if (scrollSpeed <= 0.1f) scrollSpeed = 0f;
        }
        transform.position += transform.up * (scrollSpeed * Time.fixedDeltaTime);
    }
}

using UnityEngine;
using System.Collections;

public class ScrollLevelForward : MonoBehaviour {

    public float scrollSpeed = 2.0F;
    public bool stopped;
    public float FadeSpeed = 1.0f;
    public BackgroundScroll[] backgrounds;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Utils.Paused) return;
        foreach (BackgroundScroll background in backgrounds) {
            background.stopped = stopped;
        }
        if (stopped) return;
        transform.position += transform.up * (scrollSpeed * Time.fixedDeltaTime);
    }
}

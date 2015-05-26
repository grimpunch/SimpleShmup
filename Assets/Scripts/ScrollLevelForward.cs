using UnityEngine;
using System.Collections;

public class ScrollLevelForward : MonoBehaviour {

    public float scrollSpeed = 2.0F;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Utils.Paused) return;
        transform.position += transform.up * (scrollSpeed * Time.fixedDeltaTime);
    }
}

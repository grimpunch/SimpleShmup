using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {
    public float verticalScrollSpeed = 1.0f;
    public float horizontalScrollMultiplier = 1.0f;
    public bool stopped = false;
    private GameObject player;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        if (Utils.Paused) return;
        Vector2 newTextureOffset = GetComponent<Renderer>().material.mainTextureOffset;
        if (!stopped) newTextureOffset.y += verticalScrollSpeed * Time.deltaTime; 
        if (player != null) {
            newTextureOffset.x = player.transform.position.x * horizontalScrollMultiplier;
        } else { 
            player = GameObject.FindGameObjectWithTag("Player");
            newTextureOffset.x = Mathf.Lerp(newTextureOffset.x , 0.0f, 2.0F * Time.deltaTime);
        }

        GetComponent<Renderer>().material.mainTextureOffset = newTextureOffset;
    }
}

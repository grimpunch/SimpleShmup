using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {
    public float verticalScrollSpeed = 1.0f;
    public float horizontalScrollMultiplier = 1.0f;

    private GameObject player;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        Vector2 newTextureOffset = GetComponent<Renderer>().material.mainTextureOffset;
        newTextureOffset.y += verticalScrollSpeed * Time.deltaTime; 
        if (player != null) {
            newTextureOffset.x = player.transform.position.x * horizontalScrollMultiplier;
        } else { player = GameObject.FindGameObjectWithTag("Player");}

        GetComponent<Renderer>().material.mainTextureOffset = newTextureOffset;
    }
}

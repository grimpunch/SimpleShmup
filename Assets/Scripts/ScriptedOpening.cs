using UnityEngine;
using System.Collections;

public class ScriptedOpening : MonoBehaviour {

    public GameObject explosion;
    public PlayerMovement playerMoveScript;
    public ScrollLevelForward scrollScript;
    public float timeToExplode = 3.0f;
    public float timeToStartScroll = 4.0f;
    public float timeToAllowMove = 8f;
    private float timeAlive;
    // Use this for initialization
    void Start() {
    
    }

    void Update() {
        if (Utils.Paused) return;

        timeAlive += Time.deltaTime;
        if (timeAlive > timeToExplode) EnableExplode();
        if (timeAlive > timeToStartScroll) EnableScroll();
        if (timeAlive > timeToAllowMove) EnableMove();
        if (timeAlive > 20f) DestroyThis();
    }
    void EnableExplode() { explosion.SetActive(true); }

    void EnableScroll() { scrollScript.stopped = false; }

    void EnableMove() { playerMoveScript.enabled = true; }

    void DestroyThis() { Destroy(this.gameObject); }
}

﻿using UnityEngine;
using System.Collections;

public class ScriptedOpening : MonoBehaviour {

    public GameObject[] explosions;
    public PlayerMovement playerMoveScript;
    public ScrollLevelForward scrollScript;
    public float timeToExplode = 3.0f;
    public float timeToStartScroll = 4.0f;
    public float timeToAllowMove = 8f;
    private float timeAlive;
    // Use this for initialization
    void Start() {
        playerMoveScript.enabled = false;
        scrollScript.stopped = true;
    }

    void Update() {
        if (Utils.Paused) return;

        timeAlive += Time.deltaTime;
        if (timeAlive > timeToExplode) EnableExplode();
        if (timeAlive > timeToStartScroll) EnableScroll();
        if (timeAlive > timeToAllowMove && playerMoveScript != null) EnableMove();
        if (timeAlive > (timeToAllowMove + timeToStartScroll + timeToExplode+2f)) DestroyThis();
    }
    void EnableExplode() { 
        foreach(GameObject explosion in explosions)
        {
            explosion.SetActive(true);
        } 
    }

    void EnableScroll() { scrollScript.stopped = false; }

    void EnableMove() { playerMoveScript.enabled = true; }

    void DestroyThis() { Destroy(this.gameObject); }
}

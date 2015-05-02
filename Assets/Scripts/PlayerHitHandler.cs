﻿using UnityEngine;
using System.Collections;

public class PlayerHitHandler : MonoBehaviour {

    public int shipHealth = 1;
    private const int ENEMYLAYER = 9;
    private const int ENEMYSHOTLAYER = 11;
    private const int COLLIDABLELAYER = 13;

    // Use this for initialization
    void Start() {

    }

    void OnTriggerEnter2D(Collider2D col2d) {
        Debug.Log("Colliding with " + col2d.name);
        if (col2d.gameObject.layer == ENEMYLAYER || col2d.gameObject.layer == ENEMYSHOTLAYER || col2d.gameObject.layer == COLLIDABLELAYER) {
            shipHealth -= 1;
            if (col2d.gameObject.layer == ENEMYSHOTLAYER) {
                Destroy(col2d.gameObject);
            }
        }
    }


    // Update is called once per frame
    void Update() {
        if (shipHealth <= 0) {
            gameObject.SendMessage("Gib");
        }
    }
}
﻿using UnityEngine;
using System.Collections;

public class PlayerShot : MonoBehaviour {

    private GameObject shot;
    private ScreenBoundsHandler screenBounds;
    public float shotSpeed = 1.0F;

	// Use this for initialization
	void Start () {
        shot = gameObject;
        screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
	}
	
	// Update is called once per frame
	void Update () {
        if (shot.transform.position.y < screenBounds.ScreenTop+2.0F) {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (shotSpeed * Time.fixedDeltaTime), 0.0f);
        } else {
            Destroy(this.gameObject);
        }
	}
}

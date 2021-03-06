﻿using UnityEngine;
using System.Collections;

public class EnemyHitHandler : MonoBehaviour
{

	public int shipHealth = 5;
    public int startHealth;
	private const int PLAYERLAYER = 8;
	private const int PLAYERSHOTLAYER = 10;
	private const int PLAYERLASERLAYER = 14;
	private const int CAPTURESHOTLAYER = 15;
	private ScreenBoundsHandler screenBounds;
	private ScoreHandler scoreHandler;
	public int scoreValue = 100;
	private Color spriteColor;
	public float flashTime = 0.5f;
	private float toUnFlash;
	private bool flashing = false;
	private SpriteRenderer sprite;
	public bool captureable = false;

	// Use this for initialization
	void Start()
	{
		screenBounds = GameObject.Find("ScreenBoundsHandler").GetComponent<ScreenBoundsHandler>();
		scoreHandler = GameObject.Find("Score").GetComponent<ScoreHandler>();
		sprite = gameObject.GetComponent<SpriteRenderer>();
		spriteColor = sprite.color;
        startHealth = shipHealth;
	}

	void OnTriggerEnter2D(Collider2D col2d)
	{
		if (!screenBounds) {
			return;
		}
		//if (transform.position.y > screenBounds.ScreenTop) { return; }
		if (col2d.gameObject.layer == PLAYERSHOTLAYER) {
			shipHealth -= 10;
			Flash();
			col2d.gameObject.SendMessage("Gib");
		}

		if (col2d.gameObject.layer == PLAYERLAYER) {
			shipHealth -= 1;
		}
		//if (transform.position.y > screenBounds.ScreenTop) { return; }
		if (col2d.gameObject.layer == PLAYERLASERLAYER) {
			Flash();
		}
	}

	void OnTriggerStay2D(Collider2D col2d)
	{
		if (!screenBounds) {
			return;
		}

		if (col2d.gameObject.layer == PLAYERSHOTLAYER) {
			if (!col2d) {
				return;
			}
			shipHealth -= 10;
			Flash();
			col2d.gameObject.SendMessage("Gib", SendMessageOptions.DontRequireReceiver);
		}

		if (col2d.gameObject.layer == PLAYERLASERLAYER && captureable &&
		    gameObject.GetComponent<EnemyHitHandler>().enabled &&
		    col2d.transform.parent.GetComponentInChildren<LaserChargeHandler>().GetFireButtonUp()) {
			captureable = false;
			gameObject.GetComponent<Collider2D>().enabled = false;
			Flash();
			if (col2d.transform.parent.GetComponentInChildren<CaptureShipHandler>().capturedEnemies < col2d.transform.parent.GetComponentInChildren<CaptureShipHandler>().formationPoints.Count) {
				col2d.transform.parent.GetComponentInChildren<CaptureShipHandler>().Capture(gameObject.transform.position, gameObject.name);
				col2d.transform.parent.GetComponentInChildren<LaserChargeHandler>().Discharge(); 
				Destroy(gameObject);
			} else {
				shipHealth -= 999;
			}

			return;
		}
	}

	public void Flash()
	{
		if (!flashing) {
			flashing = true;
			toUnFlash = flashTime;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Utils.Paused)
			return;
		if (shipHealth <= 0) {
			if (flashing)
				flashing = false;
			scoreHandler.AddScore(scoreValue);
			gameObject.SendMessage("Gib");
		}
		if (flashing) {
			if (toUnFlash > 0f) {
				sprite.color = Color.red;
				toUnFlash -= Time.deltaTime;
			} else { 
				toUnFlash = 0f;
				flashing = false;
				sprite.color = spriteColor;
			}
		}
	}
}

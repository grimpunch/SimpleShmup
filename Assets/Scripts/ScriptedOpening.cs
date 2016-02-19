using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptedOpening : MonoBehaviour
{

	public GameObject[] explosions;
	public List<PlayerMovement> playerMoveScripts;
	public ScrollLevelForward scrollScript;
	public float timeToExplode = 3.0f;
	public float timeToStartScroll = 4.0f;
	public float timeToAllowMove = 8f;
	private float timeAlive;
	// Use this for initialization
	void Start()
	{
		foreach (PlayerMovement playerMoveScript in playerMoveScripts) {
			playerMoveScript.enabled = false;	
		}
		scrollScript.stopped = true;
	}

	void Update()
	{
		if (Utils.Paused)
			return;

		timeAlive += Time.deltaTime;
		if (timeAlive > timeToExplode)
			EnableExplode();
		if (timeAlive > timeToStartScroll)
			EnableScroll();
		if (timeAlive > timeToAllowMove && playerMoveScripts.Count > 0)
			EnableMove();
		if (timeAlive > (timeToAllowMove + timeToStartScroll + timeToExplode + 2f))
			DestroyThis();
	}

	void EnableExplode()
	{ 
		foreach (GameObject explosion in explosions) {
			explosion.SetActive(true);
		} 
	}

	void EnableScroll()
	{
		scrollScript.stopped = false;
	}

	void EnableMove()
	{
		foreach (PlayerMovement playerMoveScript in playerMoveScripts) {
			playerMoveScript.enabled = true;
		}
	}

	void DestroyThis()
	{
		Destroy(this.gameObject);
	}
}
